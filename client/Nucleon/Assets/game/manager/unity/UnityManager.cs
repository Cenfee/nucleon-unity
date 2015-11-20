using UnityEngine;
using System.Collections;


namespace game.manager.unity
{

public class UnityManager 
{
		private static UnityManager _instance;
		public static UnityManager getInstance()
		{
			if (_instance == null) {
				_instance = new UnityManager ();
			}
			return _instance;
		}

		private GameObject _aliveGameObject;
		public GameObject getAliveGameObject()
		{
			if (_aliveGameObject == null) 
			{
				_aliveGameObject = new GameObject("game.manager.unity.AliveGameObject");	
				_aliveGameObject.AddComponent<AliveGameObject>();
			}

			return _aliveGameObject;
		}
		public AliveGameObject getAliveBehaviour()
		{
			return _aliveGameObject.GetComponent<AliveGameObject> ();
		}


		public delegate void UpdateDelegate( object sender);	
		public event UpdateDelegate updateEvent; 
		public void sendUpdateEvent()
		{
			if(updateEvent != null)
				updateEvent (this);
		}
		public void addUpdateFunction(UpdateDelegate value)
		{
			getAliveGameObject ();
			updateEvent += value;
		}
		public void removeUpdateFunction(UpdateDelegate value)
		{
			updateEvent -= value;
		}

		public delegate void LastUpdateDelegate( object sender);	
		public event LastUpdateDelegate lastUpdateEvent; 
		public void sendLastUpdateEvent()
		{
			if(lastUpdateEvent != null)
				lastUpdateEvent (this);
		}
		public void addLastUpdateFunction(UpdateDelegate value)
		{
			getAliveGameObject ();
			updateEvent += value;
		}
		public void removeLastUpdateFunction(UpdateDelegate value)
		{
			updateEvent -= value;
		}
	}
}
