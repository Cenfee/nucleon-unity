using UnityEngine;
using System.Collections;

namespace game.manager.config
{

	public class ConfigManager
	{
		private static ConfigManager _instance;
		public static ConfigManager getInstance()
		{
			if (_instance == null) {
				_instance = new ConfigManager ();
			}
			return _instance;
		}

	}

}