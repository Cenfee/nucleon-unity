
using System;
namespace game.manager.designPattern
{
	public interface IComponent
	{
		void addBinding(object owner);
		void removeBinding();
	}
}

