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
	
	public class UnityAsyncSocket   
	{   
		public delegate void OnConnectCallback ();
		public delegate void OnSendCallback();
		public delegate void OnReceiveCallback (object value);

		private Socket _socket = null;  

		
		public UnityAsyncSocket()   
		{   
		
		}  

		public void connect(string LocalIP, int LocalPort, OnConnectCallback onCallback)   

		{  
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   
			try   
			{   

				IPAddress ip = IPAddress.Parse(LocalIP);   
				IPEndPoint ipe = new IPEndPoint(ip, LocalPort);   
				DebugManager.getInstance().trace("ocket.BeginConnect");
				_socket.BeginConnect(ipe, new AsyncCallback(delegate(IAsyncResult ar)
				{
					try
					{
						Socket socket = (Socket)ar.AsyncState;
						socket.EndConnect(ar);
						DebugManager.getInstance().trace("ocket.EndConnect");
						if(onCallback != null) 
						{
							onCallback();
						}
					}
					catch (SocketException ex)
					{ 
						DebugManager.getInstance().error(ex.ToString());
					}
				}), _socket);
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
		public AsyncCallback getSendAsyncCallback(OnSendCallback onSendCallback)
		{
			DebugManager.getInstance ().trace ("send");
			return new AsyncCallback(delegate(IAsyncResult ar)
			                         {
				try
				{
					Socket socket = (Socket)ar.AsyncState;
					socket.EndSend(ar);
					DebugManager.getInstance ().trace ("EndSend");
					if(onSendCallback != null) 
					{
						onSendCallback();
					}
				}
				catch (SocketException ex)
				{ 
					DebugManager.getInstance().error(ex.ToString());
				}
			});
		}
		
		
		public void SendFloat(float data, OnSendCallback onCallback){  
			byte[] longth = TypeConvert.getBytesFromFloat(data, true);  
			_socket.BeginSend (longth, 0, longth.Length, SocketFlags.None, getSendAsyncCallback (onCallback), _socket);

		}  
		
		public void SendShort(short data, OnSendCallback onCallback) 
		{   
			byte[] longth=TypeConvert.getBytesFromShort(data,true);   
			_socket.BeginSend (longth, 0, longth.Length, SocketFlags.None, getSendAsyncCallback (onCallback), _socket);
		}   
		
		public void SendLong(long data, OnSendCallback onCallback)   
		{   
			byte[] longth=TypeConvert.getBytesFromLong(data,true);   
			_socket.BeginSend (longth, 0, longth.Length, SocketFlags.None, getSendAsyncCallback (onCallback), _socket);  
		}   
		
		public void SendInt(int data, OnSendCallback onCallback)   
		{   
			byte[] longth=TypeConvert.getBytesFromInt(data,true);   
			_socket.BeginSend (longth, 0, longth.Length, SocketFlags.None, getSendAsyncCallback (onCallback), _socket);
			
		}   
		
		public void SendString(string data, OnSendCallback onCallback)   
		{   
			byte[] longth=Encoding.UTF8.GetBytes(data);  
			SendInt(longth.Length, delegate() {
				_socket.BeginSend (longth, 0, longth.Length, SocketFlags.None, getSendAsyncCallback (onCallback), _socket);
			});  
		}   
		
		public void ReceiveFloat(OnReceiveCallback onCallback)  
		{  
			byte[] recvBytes = new byte[4];  
			_socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(delegate(IAsyncResult ar)
			{
				Socket socket = (Socket) ar.AsyncState;
				
				int read = socket.EndReceive(ar);
				
				float data = TypeConvert.getFloatFromBytes(recvBytes, true);  

				onCallback(data);
			}), _socket);//从服务器端接受返回信息   
		}   
		
		public void ReceiveShort(OnReceiveCallback onCallback)   
		{   
			byte[] recvBytes = new byte[2];   

			_socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(delegate(IAsyncResult ar)
			                                                                       {
				Socket socket = (Socket) ar.AsyncState;
				
				int read = socket.EndReceive(ar);
				
				short data=TypeConvert.getShortFromBytes(recvBytes,true);  
				
				onCallback(data);
			}), _socket);//从服务器端接受返回信息   
		}   
		
		public void ReceiveInt(OnReceiveCallback onCallback)      
		{   
			byte[] recvBytes = new byte[4];   

			DebugManager.getInstance().trace("ocket.ReceiveInt");
			_socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(delegate(IAsyncResult ar)
			                                                                       {
				Socket socket = (Socket) ar.AsyncState;
				
				int read = socket.EndReceive(ar);
				DebugManager.getInstance().trace("ocket.EndReceiveInt");
				int data=TypeConvert.getIntFromBytes(recvBytes,true);   

				onCallback(data);
			}), _socket);//从服务器端接受返回信息   
		}   
		
		public void ReceiveLong(OnReceiveCallback onCallback)       
		{   
			byte[] recvBytes = new byte[8];   

			_socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(delegate(IAsyncResult ar)
			                                                                       {
				Socket socket = (Socket) ar.AsyncState;
				
				int read = socket.EndReceive(ar);
				
				long data=TypeConvert.getLongFromBytes(recvBytes,true);   
				
				onCallback(data);
			}), _socket);//从服务器端接受返回信息   
		}   
		
		public void ReceiveString(OnReceiveCallback onCallback)      
		{   
			int length = 0;
			ReceiveInt(delegate(object value) {

				length = (int)value;
				byte[] recvBytes = new byte[length]; 

				DebugManager.getInstance().trace("ocket.BeginReceive");
				_socket.BeginReceive(recvBytes, 0, recvBytes.Length, SocketFlags.None, new AsyncCallback(delegate(IAsyncResult ar)
				                                                                       {
					Socket socket = (Socket) ar.AsyncState;
					
					int read = socket.EndReceive(ar);
					DebugManager.getInstance().trace("ocket.EndReceive");
					
					String data = Encoding.UTF8.GetString(recvBytes);   
					
					onCallback(data);
				}), _socket);//从服务器端接受返回信息  
			});  

		}   
	}  
}   