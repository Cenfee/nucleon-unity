using UnityEngine;
using System.Collections;

namespace game.manager.asset
{
	public class AssetUtil 
	{
		private string _wwwPrefix = "";
		public string wwwPrefix
		{
			get
			{
				return _wwwPrefix;
			}
		} 

		public AssetUtil()
		{
			if (Application.platform == RuntimePlatform.Android) 
			{
				_wwwPrefix = "";
			} 
			else 
			{
				_wwwPrefix = "file://";
			};
		}


		public void unloadAssets(string[] assets)
		{

		}

		public void loadAssets(string[] res, AssetManager.onProgress progress, AssetManager.onComplete complete)
		{
			complete ();
		}

		public object getAsset(string url, AssetManager.onCompleteWithData onComplete)
		{
			if (url.IndexOf ("Assets/Resources/") > -1) 
			{
				url = url.Replace ("Assets/Resources/", "");
			}
			
			if (url.IndexOf (Application.streamingAssetsPath) > -1) 
			{
				game.manager.unity.UnityManager.getInstance().getAliveBehaviour().StartCoroutine(loadStreamingAsset(url, onComplete));	


				return null;
			}

			return Resources.Load(url);
		}

		private IEnumerator loadStreamingAsset(string url, AssetManager.onCompleteWithData onComplete)
		{
			string sPath= _wwwPrefix + url;
			
			WWW www = new WWW(sPath);
			yield return www;
			
			onComplete (www);
			/**
				string jsonEx =  @"(.*)(\.json|\.plist)$";
				bool isJson = Regex.IsMatch(url, jsonEx);
			**/
		}
	}

}