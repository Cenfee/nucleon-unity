using UnityEngine;
using System.Collections;
using game.manager.designPattern;
using game.manager.unity.classes;
using DG.Tweening;
using game.manager.update;

namespace game.view.gameplay.component.action
{
	public class ManualMoveComponent : MonoBehaviour
	{

		private float _moveSpeed = 0.2f;
		private MoveComponent _moveComponent;
		
		public void Start()
		{
			_moveComponent = GetComponent<MoveComponent> ();
		}
		public void FixedUpdate()
		{

		}

		
		public void move(string direction)
		{
			_moveComponent.setMoveDirection (direction);

			float moveSpeedX = 0f;
			float moveSpeedY = 0f;
			Vector3 localScale = transform.localScale;

			if (direction == "up") {
				moveSpeedX = 0;
				moveSpeedY = _moveSpeed;
			}
			else if (direction == "down") {
				moveSpeedX = 0;
				moveSpeedY = -_moveSpeed;
			}
			else if (direction == "left") {
				moveSpeedX = -_moveSpeed;
				moveSpeedY = 0;
				localScale = new Vector3(1, 1, 1);
			}
			else if (direction == "right") {
				moveSpeedX = _moveSpeed;
				moveSpeedY = 0;
				localScale = new Vector3(-1, 1, 1);
			}	

			_moveComponent.setMoveSpeedX (moveSpeedX);
			_moveComponent.setMoveSpeedY (moveSpeedY);
			transform.localScale = localScale;
		}
		public void stopMove()
		{
			_moveComponent.setMoveSpeedX (0);
			_moveComponent.setMoveSpeedY (0);
		}
		
		public void setSpeedX(int value)
		{
			_moveComponent.setMoveSpeedX (value);
		}
		public void setSpeedY(int value)
		{
			_moveComponent.setMoveSpeedY (value);
		}

	}
}