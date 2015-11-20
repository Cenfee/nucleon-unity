using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using game.manager.unity;

namespace game.manager.view
{
	public class ViewManager
	{
		private static ViewManager _instance;
		public static ViewManager getInstance()
		{
			if (_instance == null) {
				_instance = new ViewManager ();
			}
			return _instance;
		}

		public delegate void onProgress(float value);
		public delegate void onComplete(object view);

		private Dictionary<string, Component> _viewCache = new Dictionary<string, Component>();

		private object cacheView(string name)
		{
			if (_viewCache.ContainsKey (name)) 
			{
				return _viewCache[name];
			}

			Type classObject = ViewConstant.getViewInfos()[name]["classObject"] as Type;
			//_viewCache [name] =  Activator.CreateInstance(classObject);

			Component component = UnityManager.getInstance().getAliveGameObject().AddComponent (classObject);
			_viewCache [name] = component;

			return _viewCache [name];
		}

		private void uncacheView(string name)
		{
			object view = _viewCache [name];

			MethodInfo disposeMethod = view.GetType ().GetMethod ("dispose");
			if(disposeMethod != null)
				disposeMethod.Invoke (view, null);

			GameObject.Destroy ((_viewCache [name]));
			_viewCache.Remove (name);
		}

		public void showView(string name, Dictionary<string, object> data = null)
		{
			if (!ViewConstant.getViewInfos ().ContainsKey (name))
				game.manager.debug.DebugManager.getInstance ().warn ("name" + ": 没有在管理器进行注册");

			game.manager.asset.AssetManager.getInstance ().loadViewAsset (
				name, 
				ViewConstant.getViewInfos () [name] ["assets"] as string[],
				delegate(float value) 
				{
				
				},
				delegate() 
				{
					object view = cacheView(name);
					MethodInfo showMethod = view.GetType ().GetMethod ("show");
					if(showMethod != null)
						showMethod.Invoke (view, null);

					if(data != null && data.ContainsKey("onComplete"))
						(data["onComplete"] as onComplete[])[0](view);
					
				});
		}
	}

}