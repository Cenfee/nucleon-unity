using UnityEngine;
using System.Collections;

namespace game.manager.unity.classes
{

	public class GameObjectProxy
	{
		protected GameObject _gameObject;

		public GameObjectProxy(GameObject gameObject)
		{
			if (gameObject != null)
				create (gameObject);
		}

		protected void create(GameObject gameObject)
		{
			_gameObject = gameObject;
		}
		public virtual void dispose()
		{
			GameObject.Destroy (_gameObject);
		}


		public void setActive(bool value)
		{
			_gameObject.SetActive (value);
		}
		
		public bool activeSelf
		{
			get
			{
				return _gameObject.activeSelf;
			}
		}
		
		public Transform transform
		{
			get
			{
				return _gameObject.transform;			
			}
		}

		public int layer
		{
			get
			{
				return _gameObject.layer;
			}
			set
			{
				_gameObject.layer = value;
			}
		}

		public T AddComponent<T>()where T: Component
		{
			T component = _gameObject.AddComponent<T> ();
			return component;
		}
	}
}
