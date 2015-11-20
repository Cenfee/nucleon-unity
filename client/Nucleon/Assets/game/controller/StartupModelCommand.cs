using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace game.controller
{
	public class StartupModelCommand : SimpleCommand {
		
		public override void Execute(INotification note)
		{
			base.Execute (note);

			Facade.RegisterProxy (new game.model.game.GameModel ());
		}
	}
	
}