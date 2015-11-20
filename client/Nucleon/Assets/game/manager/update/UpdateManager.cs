using UnityEngine;
using System.Collections;
using game.manager.unity;
using game.manager.debug;

namespace game.manager.update
{
	public class UpdateManager 
	{
		private static UpdateManager _instance;
		public static UpdateManager getInstance()
		{
			if (_instance == null) {
				_instance = new UpdateManager ();
			}
			return _instance;
		}

		public ArrayList updateList = new ArrayList();
		private float frameTime = 1f / 60f;
		private float frameTimeCounter = 0f;

		private float frameTimeCounterForLastUpdate = 0f;

		public UpdateManager()
		{
			UnityManager.getInstance ().addUpdateFunction(update);
			UnityManager.getInstance ().addLastUpdateFunction(lateUpdate);
		}

		public void update(object sender)
		{
			frameTimeCounter += Time.deltaTime;
			if (frameTimeCounter >= frameTime) 
			{
				frameTimeCounter -= frameTime;

				for (int updateIndex = 0; updateIndex < updateList.Count; ++updateIndex) {
					IUpdate updateObject = (IUpdate)updateList [updateIndex];
					updateObject.update ();
				}

			}
		}

		public void lateUpdate(object sender)
		{
			frameTimeCounterForLastUpdate += Time.deltaTime;
			if (frameTimeCounterForLastUpdate >= frameTime)
			{
				frameTimeCounterForLastUpdate -= frameTime;

				for (int updateIndex = 0; updateIndex < updateList.Count; ++updateIndex) {
					IUpdate updateObject = (IUpdate)updateList [updateIndex];
					updateObject.lateUpdate ();
				}
			}
		}

		public void addUpdate(IUpdate value)
		{
			if (updateList.IndexOf (value) >= 0) 
			{
				DebugManager.getInstance().warn(value + "已经加入了");
				return;
			}
			updateList.Add (value);
		}
		public void removeUpdate(IUpdate value)
		{
			updateList.Remove (value);
		}
	}
}