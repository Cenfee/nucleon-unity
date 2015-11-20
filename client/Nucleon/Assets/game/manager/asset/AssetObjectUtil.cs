using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace game.manager.asset
{

	public class AssetObjectUtil
	{
		private Dictionary<string, Dictionary<string, object>> _viewAssetObjects = new Dictionary<string, Dictionary<string, object>>();

		public Sprite getSprite(string viewName, string url, AssetManager.onCompleteWithData onComplete = null)
		{
			if (_viewAssetObjects.ContainsKey (viewName) && _viewAssetObjects [viewName].ContainsKey (url))
				return (Sprite)_viewAssetObjects [viewName] [url];

			Texture2D texture = (Texture2D)AssetManager.getInstance ().getAsset (viewName, url);
			if (!texture) 
			{
				game.manager.debug.DebugManager.getInstance().warn("AssetObjectUtil-getSprite:"+url+"在AssetManager中没有找到");
				return null;
			}


			Sprite sprite = Sprite.Create (texture, new Rect(0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));    

			if(!_viewAssetObjects.ContainsKey (viewName))
				_viewAssetObjects.Add(viewName, new Dictionary<string, object>());

			_viewAssetObjects [viewName].Add(url, sprite);

			return sprite;
		}

		public void removeViewAssetObject(string viewName)
		{
			_viewAssetObjects.Remove (viewName);
		}
	}

}