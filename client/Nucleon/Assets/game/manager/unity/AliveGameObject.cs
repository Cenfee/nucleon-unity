using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace game.manager.unity
{
	public class AliveGameObject : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
	
		}
		
		// Update is called once per frame
		void Update ()
		{
			UnityManager.getInstance ().sendUpdateEvent();
		}

		void LastUpdate()
		{
			UnityManager.getInstance ().sendLastUpdateEvent ();
		}

	}

}