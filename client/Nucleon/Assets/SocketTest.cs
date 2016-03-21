using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net; 
using System;
using System.IO; 
using System.Text;


using System.Collections.Generic;
using LitJson;
using game.manager.socket;
using System.Threading;  

public class SocketTest : MonoBehaviour {


	// Use this for initialization
	void Start () {

		

		//Debug.Log (game.manager.asset.AssetConstant.viewAssets ["load"] [0]);

		Dictionary<string, object> data = new Dictionary<string, object> ();
		data ["a"] = "b";
		data ["aa"] = "1";
		string dataStr = JsonMapper.ToJson (data);
		Debug.Log (dataStr);




		//socketConnect ("192.168.0.101", 12340);

		//string output = "客户端请求连接~~~";  
		//byte[] concent = Encoding.UTF8.GetBytes(output);          
		//socketSend(concent);  
	

		jsonSocket = new JsonPackAsyncSocket ();
		jsonSocket.connect("192.168.0.101", 12340, connectHandler);
		jsonSocket.onReceiveCallback = receiveHandler;


	}  

	public void connectHandler()
	{
		Dictionary<string, object> data = new Dictionary<string, object> ();
		data ["a"] = "b";
		data ["aa"] = "1";
		//jsonSocket.send (data);

	}
	public void receiveHandler(object value)
	{
		Debug.Log (value);
	}

	private JsonPackAsyncSocket jsonSocket;
	public void OnDestroy ()
	{
		jsonSocket.close ();
	}


	void Update () {
	
	}
}

