using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace game.controller
{
	public class StartupViewCommand : SimpleCommand {
		
		public override void Execute(INotification note)
		{
			base.Execute (note);

			Facade.RegisterMediator(new game.view.gameplay.GameMediator());
		}
	}
	
}