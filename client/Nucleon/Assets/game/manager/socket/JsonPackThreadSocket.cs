
using game.manager.debug;
using System.Collections.Generic;
using System;
using System.Threading;
using LitJson;

namespace game.manager.socket
{
	public class JsonPackThreadSocket
	{
		private UnitySyncSocket _unitySocket;
		private Thread _thread = null;  

		public JsonPackThreadSocket ()
		{

		}

		public void close()
		{
			_unitySocket.close ();
			_unitySocket = null;

			_thread.Abort ();
			_thread = null;


		}
		public void connect(string localIP, int localPort)   
		{
			_unitySocket = new UnitySyncSocket();
			_unitySocket.connect (localIP, localPort);

			_thread = new Thread (new ThreadStart (threadRun));
			_thread.IsBackground = true;
			_thread.Start ();
		}

		private void threadRun()
		{
			while (true)  
			{  
				try  
				{  
					string receiveStr = _unitySocket.ReceiveString ();
					DebugManager.getInstance ().trace (receiveStr);
				}  
				catch (Exception e)  
				{  
					close ();
					DebugManager.getInstance () .error(e.ToString()); 
					break;
				}  
			}  
		}

		public void send(Dictionary<string ,object> data)
		{
			string dataStr = JsonMapper.ToJson (data);
			_unitySocket.SendString (dataStr);
		}
	}
}

