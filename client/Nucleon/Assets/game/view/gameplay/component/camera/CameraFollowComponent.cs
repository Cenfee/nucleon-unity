using UnityEngine;
using System.Collections;
using game.manager.unity.classes;
using game.manager.update;

namespace game.view.gameplay.component.camera
{

	public class CameraFollowComponent : MonoBehaviour
	{

		public CameraFollowComponent()
		{

		}

		public void Start()
		{
		}
	
		public void FixedUpdate()
		{

		}
		public void LateUpdate()
		{
			GameObject hero = GameView.getHero ();
			if (transform.localPosition.x != hero.transform.localPosition.x ||
			    transform.localPosition.y != hero.transform.localPosition.y) 
			{
				
				transform.Translate(
					hero.transform.localPosition.x - transform.localPosition.x, 
					hero.transform.localPosition.y - transform.localPosition.y, 
					0);
				
				//transform.localPosition = new Vector3 (_hero.transform.localPosition.x, _hero.transform.localPosition.y, -10);
			}

		}
	}
}