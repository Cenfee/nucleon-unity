using UnityEngine;
using System.Collections;

namespace game.view.gameplay.component.interation
{

	public class Interation 
	{
		private GameView _root;


		public Interation(GameView root)
		{
			_root = root;
		}

		public void update()
		{
			//Debug.Log (Camera.main.ScreenToWorldPoint(Input.mousePosition));


			if (Input.GetMouseButtonUp (0)) 
			{
				Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			}

			
			foreach ( Touch touch in Input.touches) 
			{
				
				if (touch.phase == TouchPhase.Ended) 
				{
					
					
				}
			}
		}
	}
}