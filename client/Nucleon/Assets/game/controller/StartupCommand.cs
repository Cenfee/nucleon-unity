using UnityEngine;

using PureMVC.Patterns;

namespace game.controller
{
	public class StartupCommand : MacroCommand {

		protected override void InitializeMacroCommand ()
		{
			base.InitializeMacroCommand ();

			AddSubCommand(typeof(StartupControllerCommand));
			AddSubCommand(typeof(StartupModelCommand));
			AddSubCommand(typeof(StartupViewCommand));
		}
	}

}