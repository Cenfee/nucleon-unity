using UnityEngine;
using System.Collections;

namespace game.manager.unity.classes
{

	public class GameObjectContainer : GameObjectProxy
	{
		public GameObjectContainer(string name = ""):base(null)
		{
			if (name == "")
				name = GetType ().Name;

			GameObject gameObject = new GameObject(name);	

			create (gameObject);
		}

	}

}