using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace game.manager.scene
{
	public class SceneManager
{

		private static SceneManager _instance;
		public static SceneManager getInstance()
		{
			if (_instance == null) {
				_instance = new SceneManager ();
			}
			return _instance;
		}
	
		public delegate void onProgress(float value);
		public delegate void onComplete();

		private string _name="";
		private game.manager.view.ViewManager.onProgress _progress;
		private game.manager.view.ViewManager.onComplete _complete;

		public SceneManager()
		{

		}

		public void switchScene(string name, game.manager.view.ViewManager.onProgress progress = null, game.manager.view.ViewManager.onComplete complete = null)
		{
			_name = name;
			_progress = progress;
			_complete = complete;

			if (game.manager.config.ConfigConstant.ONE_SCENE_TEST)
			{
				sceneLoadCompleteHandler ();
				return;
			}

			game.manager.unity.AliveGameObject aliveGameObject = game.manager.unity.UnityManager.getInstance().getAliveBehaviour();
			aliveGameObject.StartCoroutine (switchSceneAsync (name, null, delegate() 
			{

			}));

		}

		private IEnumerator switchSceneAsync(string name, onProgress progress = null, onComplete complete = null)
		{
			AsyncOperation async = Application.LoadLevelAsync(name);
			if(complete != null)
				complete ();
			yield return async;
			Debug.Log ("switchSceneAsync complete");
		}

		public void sceneLoadCompleteHandler()
		{
			if (_name == "")
				return;

			game.manager.view.ViewManager.getInstance ().showView (_name, new Dictionary<string, object>        
			                                                       {
				{"onComplete", new game.manager.view.ViewManager.onComplete[]
					{
						delegate(object view)
						{
							_name = "";
							_complete(view);

							_progress = null;
							_complete = null;
						}
					}
				}
			});
		}
}
}

