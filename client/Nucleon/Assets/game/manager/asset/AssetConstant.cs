using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace game.manager.asset
{
public class AssetConstant 
{
		public static string COMMON = "common";
		public static string GAME = "game";
		public static Dictionary<string, string[]> viewAssets = new Dictionary<string, string[]>
		{
			{"common", new string[0]},
			{"game", new string[0]}
		};
}
}
