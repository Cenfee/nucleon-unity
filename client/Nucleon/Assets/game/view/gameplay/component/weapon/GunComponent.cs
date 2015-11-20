using UnityEngine;
using System.Collections;
using game.view.gameplay.component;
using game.manager.unity.classes;
using game.manager.designPattern;
using game.view.gameplay.component.bullet;
using game.view.gameplay.component.action;
using game.manager.asset;

namespace game.view.gameplay.component.weapon
{

	public class GunComponent : MonoBehaviour
	{
		public GunComponent()
		{
		}

		public void Start()
		{

		}

		public void shoot()
		{
			string direction = GetComponent<MoveComponent>().getMoveDirection();

			Vector3 position = new Vector3 (
				transform.localPosition.x, 
				transform.localPosition.y,
				transform.localPosition.z);

			float xSpeed = 0f;
			float ySpeed = 0f;


			GameObject bullet = GameView.getBulletSystem ().GetComponent<BulletSystem> ().createBullet (position);

			if (direction == "up") 
			{
				xSpeed = 0f;
				ySpeed = 0.1f;
				position.y += GetComponent<CircleCollider2D>().radius + bullet.GetComponent<CircleCollider2D>().radius + 0.1f;
			}
			else if (direction == "down") 
			{
				xSpeed = 0f;
				ySpeed = -0.1f;
				position.y -= GetComponent<CircleCollider2D>().radius + bullet.GetComponent<CircleCollider2D>().radius + 0.1f;
			}
			else if (direction == "left") 
			{
				xSpeed = -0.1f;
				ySpeed = 0f;
				position.x -= GetComponent<CircleCollider2D>().radius + bullet.GetComponent<CircleCollider2D>().radius + 0.1f;
			}
			else if (direction == "right") 
			{
				xSpeed = 0.1f;
				ySpeed = 0f;
				position.x += GetComponent<CircleCollider2D>().radius + bullet.GetComponent<CircleCollider2D>().radius + 0.1f;
			}

			bullet.transform.localPosition = position;
			MoveComponent bulletMove = bullet.GetComponent<MoveComponent> ();

			bulletMove.setMoveSpeedX(xSpeed);
			bulletMove.setMoveSpeedY(ySpeed);
		}
	}

}