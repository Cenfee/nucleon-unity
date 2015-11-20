using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace game.controller
{
	public class StartupControllerCommand : SimpleCommand {
		
		public override void Execute(INotification note)
		{
			base.Execute (note);

		}
	}
	
}