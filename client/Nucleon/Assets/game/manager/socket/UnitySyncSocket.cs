namespace game.manager.socket
{ 	
	// 描   述：封装c# socket数据传输协议  
	using game.manager.debug;
	using System;   
	using System.Net.Sockets;   
	using System.Net;   
	using System.Collections;   
	using System.Text;  
	using System.Threading;  
	
	public class UnitySyncSocket   
	{   
		private Socket _socket = null;  
		
		
		public UnitySyncSocket()   
		{   
			
		}   
		public void connect(string LocalIP, int LocalPort)   
		{  
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   
			try   
			{   
				
				IPAddress ip = IPAddress.Parse(LocalIP);   
				IPEndPoint ipe = new IPEndPoint(ip, LocalPort);   
				_socket.Connect(ipe);  
			}   
			catch (Exception e)   
			{  
				DebugManager.getInstance().error(e.ToString());  
			}   
		}  
		
		public void close(){  
			_socket.Shutdown(SocketShutdown.Both);
			_socket.Close();
			_socket=null;  
		}   
		
		
		public void SendFloat(float data){  
			byte[] longth = TypeConvert.getBytesFromFloat(data, true);  
			_socket.Send(longth);  
		}  
		
		public void SendShort(short data)   
		{   
			byte[] longth=TypeConvert.getBytesFromShort(data,true);   
			_socket.Send(longth);  
		}   
		
		public void SendLong(long data)   
		{   
			byte[] longth=TypeConvert.getBytesFromLong(data,true);   
			_socket.Send(longth);   
		}   
		
		public void SendInt(int data)   
		{   
			byte[] longth=TypeConvert.getBytesFromInt(data,true);   
			_socket.Send(longth);   
			
		}   
		
		public void SendString(string data)   
		{   
			byte[] longth=Encoding.UTF8.GetBytes(data);  
			SendInt(longth.Length);  
			_socket.Send(longth);   
			
		}   
		
		public float ReceiveFloat()  
		{  
			byte[] recvBytes = new byte[4];  
			_socket.Receive(recvBytes, 4, 0);//从服务器端接受返回信息   
			float data = TypeConvert.getFloatFromBytes(recvBytes, true);  
			return data;  
		}   
		
		public short ReceiveShort()   
		{   
			byte[] recvBytes = new byte[2];   
			_socket.Receive(recvBytes,2,0);//从服务器端接受返回信息   
			short data=TypeConvert.getShortFromBytes(recvBytes,true);   
			return data;   
		}   
		
		public int ReceiveInt()   
		{   
			byte[] recvBytes = new byte[4];   
			_socket.Receive(recvBytes,4,0);//从服务器端接受返回信息   
			int data=TypeConvert.getIntFromBytes(recvBytes,true);   
			return data;   
		}   
		
		public long ReceiveLong()   
		{   
			byte[] recvBytes = new byte[8];   
			_socket.Receive(recvBytes,8,0);//从服务器端接受返回信息   
			long data=TypeConvert.getLongFromBytes(recvBytes,true);   
			return data;   
		}   
		
		public String ReceiveString()   
		{   
			int length = ReceiveInt();  
			byte[] recvBytes = new byte[length];   
			_socket.Receive(recvBytes,length,0);//从服务器端接受返回信息   
			String data = Encoding.UTF8.GetString(recvBytes);   
			return data;    
		}   
		
		
	}  
}   