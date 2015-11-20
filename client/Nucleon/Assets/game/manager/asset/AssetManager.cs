using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace game.manager.asset
{

	public class AssetManager
	{
		private static AssetManager _instance;
		public static AssetManager getInstance()
		{
			if (_instance == null) {
				_instance = new AssetManager ();
			}
			return _instance;
		}

		public delegate void onProgress(float value);
		public delegate void onComplete();
		public delegate void onCompleteWithData(object data);

		private Dictionary<string, string[]> _viewAssets = new Dictionary<string, string[]>();

		public AssetUtil assetUtil = new AssetUtil();
		public AssetObjectUtil assetObjectUtil = new AssetObjectUtil();


		public AssetManager()
		{

		}


		public void loadViewAsset(string viewName, string[] res, onProgress progress, onComplete complete, bool isDisplay=true)
		{
			if(this._viewAssets.ContainsKey(viewName) == false)
				this._viewAssets[viewName] = new string[0];

			res.CopyTo(_viewAssets[viewName], _viewAssets[viewName].Length);

			if (isDisplay)
				loadWithDisplay (res, progress, complete);
			else
				load (res, progress, complete);
		}

		public void unloadViewAsset(string viewName)
		{
			if (_viewAssets.ContainsKey(viewName)) 
			{
				game.manager.debug.DebugManager.getInstance().error("AssetManager:unloadViewAssets 没有" + viewName);
				return;
			}

			string[] viewAsset = _viewAssets [viewName];

			assetUtil.unloadAssets (viewAsset);

			assetObjectUtil.removeViewAssetObject (viewName);
			_viewAssets.Remove (viewName);
		}

		public void loadWithDisplay(string[] res, onProgress progress, onComplete complete)
		{
			load (res, progress, complete);
		}

		public void load(string[] res, onProgress progress, onComplete complete)
		{
			assetUtil.loadAssets (res, progress, complete);
		}

		public object getAsset(string viewName, string url, onCompleteWithData onComplete = null)
		{
			object asset = assetUtil.getAsset (url, delegate(object data) 
			                                   {
				onComplete(data);
											});
			return asset;
		}
	}

}