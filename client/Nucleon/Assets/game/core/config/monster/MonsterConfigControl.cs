using System;
using System.Collections.Generic;

using UnityEngine;
using LitJson;
using game.manager.asset;

namespace game.core.config.monster
{
	public class MonsterConfigControl
	{
		private JsonData _monsterJsonData;
		private Dictionary<string, MonsterConfig> _monsterConfigCache = new Dictionary<string, MonsterConfig>();

		public MonsterConfigControl ()
		{
			_monsterJsonData = JsonMapper.ToObject(((TextAsset)AssetManager.getInstance ().getAsset (AssetConstant.COMMON, "Assets/Resources/config/monster/monster")).text);
		}

		public MonsterConfig getMonsterConfig(string id)
		{
			if (_monsterConfigCache.ContainsKey (id))
				return _monsterConfigCache [id];

			int index = int.Parse (id) - 1;
			if (index >= _monsterJsonData.Count)
				return null;

			MonsterConfig monsterConfig = new MonsterConfig (_monsterJsonData [index]);
			_monsterConfigCache.Add (id, monsterConfig);

			return monsterConfig;
		}
	}
}

