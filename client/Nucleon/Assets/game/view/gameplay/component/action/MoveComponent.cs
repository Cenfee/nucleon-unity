using UnityEngine;
using System.Collections;
using game.manager.designPattern;
using game.manager.unity.classes;
using DG.Tweening;
using game.manager.update;

namespace game.view.gameplay.component.action
{
	public class MoveComponent : MonoBehaviour
	{
		
		private float _moveSpeedX = 0f;
		private float _moveSpeedY = 0f;

		private string _moveDirection = "right";


		public void Start()
		{

		}

		public void FixedUpdate()
		{
			if (getIsWalk ()) 
			{
				float newPositionY = transform.localPosition.y + getMoveSpeedY ();
				transform.localPosition = new Vector3(
					transform.localPosition.x + getMoveSpeedX (),
					newPositionY,
					-1f / newPositionY
					);
			}

		}
		

		public float getMoveSpeedX()
		{
			return _moveSpeedX;
		}
		public void setMoveSpeedX(float value)
		{
			_moveSpeedX = value;
		}
		public float getMoveSpeedY()
		{
			return _moveSpeedY;
		}
		public void setMoveSpeedY(float value)
		{
			_moveSpeedY = value;
		}

		public string getMoveDirection()
		{
			return _moveDirection;
		}
		public void setMoveDirection(string value)
		{
			_moveDirection = value;
		}

		public bool getIsWalk()
		{
			if (_moveSpeedX != 0f || _moveSpeedY != 0f) 
				return true;
			else
				return false;
		}

	}
	
}