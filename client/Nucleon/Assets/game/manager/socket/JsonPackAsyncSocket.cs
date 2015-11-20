
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace game.manager.socket
{
	public class JsonPackAsyncSocket
	{
		private UnityAsyncSocket _unitySocket;

		public UnityAsyncSocket.OnReceiveCallback onReceiveCallback;

		public JsonPackAsyncSocket ()
		{
		}

		public void close()
		{
			_unitySocket.close ();
			_unitySocket = null;

			
			
		}

		public void connect(string localIP, int localPort, UnityAsyncSocket.OnConnectCallback onCallback)   
		{
			_unitySocket = new UnityAsyncSocket();
			_unitySocket.connect (localIP, localPort, delegate {

				if(onCallback != null)
				{
					onCallback();
				}
				_unitySocket.ReceiveString(new UnityAsyncSocket.OnReceiveCallback(receiveHandler));
		});

		}

		private void receiveHandler(object value)
		{
			JsonData jsonData = JsonMapper.ToObject ((string)value);
			if (onReceiveCallback != null)
				onReceiveCallback (jsonData);
		}

		public void send(object data, UnityAsyncSocket.OnSendCallback onCallback = null)
		{
			string dataStr = JsonMapper.ToJson (data);
			_unitySocket.SendString (dataStr, onCallback);
		}
	}
}

