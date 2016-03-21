using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using game.controller;

namespace game
{

	public class ApplicationFacade : Facade 
	{

		private static ApplicationFacade _instance;
		public static ApplicationFacade getInstance()
		{
			if (_instance == null) 
			{
				_instance = new ApplicationFacade ();
			}
			return _instance;
		}

		public void startup(object app = null)
		{
			SendNotification( AppConstant.STARTUP, app );
		}

		protected override void InitializeController ()
		{
			base.InitializeController ();

			RegisterCommand (AppConstant.STARTUP, typeof(StartupCommand));
		}

	}
}
