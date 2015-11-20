//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace game.manager.designPattern
{
	public class ComponentControl
	{
		private object _owner;

		private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

		public ComponentControl(object owner)
		{
			_owner = owner;
		}
		public void dispose()
		{
			foreach (Type key in _components.Keys) {
				removeComponent(key);
			}

			_components.Clear ();

			_owner = null;
		}
		public IComponent addComponent(Type componentType)
		{
			IComponent component = (IComponent)Activator.CreateInstance(componentType);
			component.addBinding (_owner);
			_components.Add (componentType, component);
			return component;
		}

		public void removeComponent(Type componentType)
		{
			IComponent component = _components[componentType];
			component.removeBinding ();
			_components.Remove (componentType);
		}

		public IComponent getComponent(Type componentType)
		{
			return _components[componentType];
		}
	}
}

