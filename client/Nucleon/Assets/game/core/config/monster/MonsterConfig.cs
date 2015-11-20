using System;
using LitJson;
namespace game.core.config.monster
{
	public class MonsterConfig
	{
		public string id;
		public string name;
		public string code;
		public float attack;
		public float defence;
		public float speed;

		public MonsterConfig (JsonData data)
		{
			id = (data["id"].ToString());
			name = (data["name"].ToString());
			code = (data["code"].ToString());
			attack = float.Parse(data["attack"].ToString());
			defence = float.Parse(data["defence"].ToString());
			speed = float.Parse(data["speed"].ToString());
		}
	}
}

