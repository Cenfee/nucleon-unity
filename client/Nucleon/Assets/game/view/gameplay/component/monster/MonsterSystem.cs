using UnityEngine;
using System.Collections;
using game.manager.unity.classes;
using game.manager.asset;
using game.view.gameplay.component.action;
using game.view.gameplay.component.collider;

namespace game.view.gameplay.component.monster
{
	public class MonsterSystem : MonoBehaviour
	{

		private ArrayList _monsters = new ArrayList ();

		public void Start()
		{
			GameObject objectContainer = GameView.getMap ().transform.FindChild ("ObjectContainer").gameObject;
			for (int objectIndex = 0; objectIndex < objectContainer.transform.childCount; ++objectIndex) {
				GameObject theObject = objectContainer.transform.GetChild(objectIndex).gameObject;
				if(theObject.tag == "Monster")
				{
					theObject.AddComponent<ColliderSystem> ();
				}
			}
		}
		public void createMonster(Vector3 position)
		{
			GameObject monsterPrefab = (GameObject)AssetManager.getInstance().getAsset(AssetConstant.GAME, "Assets/Resources/game/monster/monsterPrefab");
			GameObject monster = GameObject.Instantiate (monsterPrefab);
			monster.AddComponent<ColliderSystem>();

			monster.transform.SetParent (GameView.getMap().transform.FindChild("ObjectContainer"));
			monster.transform.localPosition = position;
			
			addMonster (monster);
		}
		public void destroyMonster(GameObject monster)
		{
			removeMonster (monster);
			GameObject.Destroy (monster);
		}
		public void addMonster(GameObject monster)
		{
			_monsters.Add (monster);
		}
		public void removeMonster(GameObject monster)
		{
			_monsters.Remove (monster);
		}

	}
}