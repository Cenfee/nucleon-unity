using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game.manager.asset;

namespace game.manager.view
{
	public class ViewConstant 
	{

		public static string GAME = "game";


		private static Dictionary<string, Dictionary<string, object>> _viewInfos;
		public static Dictionary<string, Dictionary<string, object>> getViewInfos()
		{
			if (_viewInfos == null) 
			{
				_viewInfos = new Dictionary<string, Dictionary<string, object>>();


				_viewInfos[GAME] = new Dictionary<string, object>
				{
					{"classObject", typeof(game.view.gameplay.component.GameView)},
					{"assets", AssetConstant.viewAssets[GAME]}
				};
			}

			return _viewInfos;
		}

	}

}
