using UnityEngine;
using System.Collections;

namespace game.manager.debug
{
public class DebugManager
{
	private static DebugManager _instance;
	public static DebugManager getInstance()
	{
		if (_instance == null) {
			_instance = new DebugManager ();
		}
		return _instance;
	}

	public void trace(object value)
	{
		Debug.Log ("[trace] " + value);
	}

	public void warn(object value)
	{
		Debug.Log ("[warn] " + value);
	}

	public void error(object value)
	{
		Debug.Log ("[error] " + value);
	}

}

}