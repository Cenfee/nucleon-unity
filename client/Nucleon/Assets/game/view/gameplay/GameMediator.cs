
using UnityEngine;

using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using game.manager.scene;

namespace game.view.gameplay
{

	public class GameMediator : Mediator 
	{

		public new const string NAME = "GameMediator";
		private MonoBehaviour _view;

		public GameMediator():base(NAME, null)
		{

		}


		public override IList<string> ListNotificationInterests ()
		{
			List<string> arr = new List<string> ();
			arr.Add (NAME + "Show");
			arr.Add (NAME + "Hide");
			return arr;
		}

		public override void HandleNotification (INotification note)
		{
			base.HandleNotification (note);

			switch (note.Name) 
			{
				case NAME + "Show":
					show();
					break;

				case NAME + "Hide":
					hide();
					break;
			}

		}

		public void show()
		{
			SceneManager.getInstance ().switchScene (game.manager.scene.SceneConstant.GAME, null, delegate(object view)            
			{
				_view = view as MonoBehaviour;
			});
		}

		public void hide()
		{
			_view = null;
		}

	}
}
