//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using game.view.gameplay.component.bullet;
using game.view.gameplay.component.monster;

namespace game.view.gameplay.component.collider
{
	public class ColliderSystem : MonoBehaviour
	{

		public void OnCollisionEnter2D(Collision2D other) 
		{
			

		}

		public void OnTriggerEnter2D(Collider2D other) 
		{
			if (tag == "Bullet") {

				if (other.tag == "Wall") {
					GameView.getBulletSystem ().GetComponent<BulletSystem> ().destroyBullet (gameObject);
				} else if (other.tag == "Monster") {
					GameView.getBulletSystem ().GetComponent<BulletSystem> ().destroyBullet (gameObject);
					GameView.getMonsterSystem ().GetComponent<MonsterSystem> ().destroyMonster (other.gameObject);
				}
			} else if (tag == "Monster") {


			}
		}
	}
}

