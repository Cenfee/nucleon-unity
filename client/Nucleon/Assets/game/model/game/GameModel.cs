using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

namespace game.model.game
{

	public class GameModel : Proxy {

		public new static string NAME = "GameModel";

		public GameModel():base(NAME, null)
		{

		}
	}
}