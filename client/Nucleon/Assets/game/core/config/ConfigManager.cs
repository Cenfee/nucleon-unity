
using System;

namespace game.core.config
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

		public ConfigManager ()
		{
		}

		private monster.MonsterConfigControl _monsterConfigControl = new monster.MonsterConfigControl();
		public monster.MonsterConfigControl getMonsterConfigControl()
		{
			return _monsterConfigControl;
		}
	}
}

