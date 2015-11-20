package socket

//package main

import (
	"bytes"
	"encoding/binary"
	"encoding/json"
	"errors"
	"log"
	"net"
	"sync"
	"time"
)

const (
	ConnectionMax = 100 //CS max connect
)

//conn Pool info
var (
	poolLock sync.RWMutex
	poolCli  [ConnectionMax]*CliInfo
)

type PackHead struct {
	packSize int32
	packType string
}

//Cli info
type CliInfo struct {
	AssignID   int            //cs assign ID
	Conn       net.Conn       // The TCP/IP connectin to the player.
	ConnTime   time.Time      //连接时间
	VerifyKey  string         //连接验证KEY
	ConnVerify bool           //是否验证
	ServerType int32          //服务器类型（1DB服务器，2WEB服务器）
	NodeStat   int32          //服务器当前状态（0、宕机；1、正常；2、数据导入中；3、准备中;4、数据迁出中
	Address    string         //服务地址
	Port       int            //服务端口
	BackupSer  map[string]int //备份服务器列表map(ip:port)
	sync.RWMutex
}

// Client disconnect
func (cli *CliInfo) disconnect(clientID int) {
	poolLock.Lock()
	defer poolLock.Unlock()
	cli.Conn.Close()
	log.Printf("Client: %s quit\n", cli.VerifyKey)
	if cli.ServerType == 1 {
		//掉线处理

		poolCli[clientID] = nil

	} else {

	}

}

//listen handle
func (cli *CliInfo) listenHandle(clientID int) {
	headBuff := make([]byte, 4) // set read stream size
	defer cli.Conn.Close()

	//send verify Key：
	b := []byte(cli.VerifyKey)
	cli.Conn.Write(b)
	// fmt.Println("cli-IP:", cli.Conn.RemoteAddr().String())

	//await 10 second verify
	cli.Conn.SetDeadline(time.Now().Add(time.Duration(10) * time.Second))

	forControl := true
	for forControl {
		var headNum int
		for headNum < cap(headBuff) {
			readHeadNum, readHeadErr := cli.Conn.Read(headBuff[headNum:])
			if readHeadErr != nil {
				log.Println("errHead:", readHeadErr)
				forControl = false
				break
			}
			headNum += readHeadNum
		}
		if headNum == cap(headBuff) {
			//pack head Handle
			var packHead, formatHeadErr = formatPackHead(headBuff)
			if formatHeadErr != nil {
				log.Println("errFormatHead:", formatHeadErr)
				forControl = false
				break
			}
			bodyBuff := make([]byte, packHead.packSize)
			var bodyNum int
			for bodyNum < cap(bodyBuff) {
				readBodyNum, readBodyErr := cli.Conn.Read(bodyBuff[bodyNum:])
				if readBodyErr != nil {
					log.Println("errBody:", readBodyErr)
					forControl = false
					break
				}
				bodyNum += readBodyNum
			}
			if bodyNum == int(packHead.packSize) {
				//pack body Handle
				//cli.packBodyAnalysis(clientID, packHead, bodyBuff)
				formatPackBody(clientID, packHead, bodyBuff)
				// fmt.Printf("packType:%d;packOther:%d;packLen:%d\n", packHead.PackType, packHead.PackOther, packHead.PackLen)
			}

		}
	}
	//掉线处理
	cli.ServerType = 1
	cli.disconnect(clientID)
}

func formatPackHead(headBuff []byte) (*PackHead, error) {
	var bBuf *bytes.Buffer = bytes.NewBuffer(headBuff)
	var packSize int32
	var err error = binary.Read(bBuf, binary.BigEndian, &packSize)

	var data PackHead
	data.packSize = packSize
	return &data, err
}
func formatPackBody(clientID int, packHead *PackHead, bodyBuff []byte) (interface{}, error) {
	var data map[string]interface{}
	var err error = json.Unmarshal(bodyBuff, &data)

	var a string = data["a"].(string)
	log.Println(a)
	return data, err
}

//Check or assign new conn
func NewConnection_CS(conn net.Conn) (ok bool, index int, info *CliInfo) {
	poolLock.Lock()
	defer poolLock.Unlock()

	//Assign ID for client
	var i int
	for i = 0; i < ConnectionMax; i++ {
		if poolCli[i] == nil {
			break
		}
	}

	//Too many connections
	if i > ConnectionMax {
		log.Printf("Too many connections! Active Denial %s\n", conn.RemoteAddr().String())
		conn.Close()
		return false, 0, nil
	}

	//Create client base info
	Cli := new(CliInfo)
	Cli.Conn = conn
	Cli.ConnTime = time.Now()
	Cli.VerifyKey = "VerifyKey"
	Cli.BackupSer = make(map[string]int)

	//Update Pool info
	poolCli[i] = Cli
	log.Println("Cli ID assign ok:", i)
	return true, i, Cli
}

//start listens
func StartListen(addr string) error {
	var listener, err = net.Listen("tcp", addr)
	if err != nil {
		return err
	}

	// if Errors accept arrive 100 .listener stop.
	var failures int
	for failures = 0; failures < 100; {
		var conn, listenErr = listener.Accept()
		if listenErr != nil {
			log.Printf("number:%d,failed listening:%v\n", failures, listenErr)
			failures++
		}
		var ok, index, Cli = NewConnection_CS(conn)
		if ok {
			log.Println(index, "cenfee")
			// A new connection is established. Spawn a new gorouting to handle that Client.
			go Cli.listenHandle(index)
		}
	}
	return errors.New("Too many listener.Accept() errors,listener stop")
}

/**
三、原理

一个新的连接建立。产生一个新的gorouting来处理客户端。

    一个客户端进来首先分配一个唯一ID，并初始化该客户端的基本信息(见：NewConnection_CS方法)，产生一个新的gorouting来处理客户端。

    如果服务器达到设定的连接上限，将抛弃该客户端。

    客户端连接(分配ID)正常后，将等待10秒来给客户端进行验证超期将断开该客户端连接(见：listenHandle中的cli.Conn.SetDeadline)。

    验证成功后,开接与用户数据进行分析处理：接收原理为：前4字节为包类型，4-12字节为包长，首先接收够12字节后进行包头解析(如不够12字节将进行等待直到够12字节)，解析出4-12字节来得到整个包体的长度进行读取(如不够将等待直到够包体长度)

    整包读取完后，根据0-4字节判断包的类型进行包的处理。

四、服务器连接出错达到100条后将终止运行
**/

/**
struct send_info
{
char info_from[20]; //发送者ID

int info_length; //发送的消息主体的长度
char info_content[1024]; //消息主体
};
**/
