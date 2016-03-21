using UnityEngine;
using System.Collections;

using LitJson;
using game.view.gameplay.component.action;
using game.view.gameplay.component.interation;
using game.view.gameplay.component.camera;
using game.view.gameplay.component.weapon;
using game.view.gameplay.component.monster;
using game.view.gameplay.component.bullet;
using game.view.gameplay.component.ui;
using game.manager.asset;
using game.core.config;

using System.Collections.Generic;

namespace game.view.gameplay.component
{
	public class GameView : MonoBehaviour
	{

		public static GameObject getMap(){return _map;}
		public static GameObject getHero(){return _hero;}
		public static GameObject getMonsterSystem(){return _monsterSystem;}
		public static GameObject getBulletSystem(){return _bulletSystem;}
		public static GameObject getMainCamera(){return _mainCamera;}
		public static GameObject getUISystem(){return _uiSystem;}

		private static GameObject _map;	
		private static GameObject _hero;		
		private static GameObject _monsterSystem;	
		private static GameObject _bulletSystem;
		private static GameObject _mainCamera;
		private static GameObject _uiSystem;

			// Use this for initialization
		void Start ()
		{
			//加载全局元素
			_monsterSystem = new GameObject ("MonsterSystem");
			_monsterSystem.AddComponent<MonsterSystem> ();

			_bulletSystem = new GameObject ("BulletSystem");
			_bulletSystem.AddComponent<BulletSystem> ();

			//todo
			GameObject mapPrefab = (GameObject)AssetManager.getInstance().getAsset(AssetConstant.GAME, "Assets/Resources/game/map/mapPrefab");
			_map = GameObject.Instantiate (mapPrefab);
			//_map = GameObject.Find ("Map");
			_map.name = "Map";

			//加载全局元素
			GameObject heroPrefab = (GameObject)AssetManager.getInstance().getAsset(AssetConstant.GAME, "Assets/Resources/game/hero/heroPrefab");
			_hero = GameObject.Instantiate (heroPrefab);
			_hero.AddComponent<MoveComponent> ();
			_hero.AddComponent<ManualMoveComponent> ();
			_hero.AddComponent<GunComponent> ();
			_hero.transform.SetParent(_map.transform.FindChild("ObjectContainer"));
			_hero.transform.localPosition = new Vector3 (2f, 2f, 0f);
			_hero.name = "Hero";

			_mainCamera = GameObject.Find ("Main Camera");
			CameraFollowComponent component = _mainCamera.AddComponent<CameraFollowComponent> ();


			_uiSystem = new GameObject ("UISystem");
			_uiSystem.AddComponent<UISystem> ();


			ConfigManager.getInstance ();
		}
		// Update is called once per frame
		void Update ()
		{

		}

		void FixedUpdate()
		{

		}

		void LateUpdate()
		{

		}

	}

}