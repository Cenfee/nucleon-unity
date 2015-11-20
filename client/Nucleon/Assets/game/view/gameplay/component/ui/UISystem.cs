using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using game.manager.asset;
using game.manager.unity.classes;

using game.view.gameplay.component.weapon;
using game.view.gameplay.component.action;

namespace game.view.gameplay.component.ui
{

	public class UISystem : MonoBehaviour
	{
		private GameObject _canvas;
		private GameObject _main;



		public void Start()
		{
			_canvas = GameObject.Find ("Canvas");


			createNew ();
		}

		private void createNew()
		{

			GameObject prefab = (GameObject)AssetManager.getInstance().getAsset(AssetConstant.GAME, "Assets/Resources/game/main");		
			_main = GameObject.Instantiate (prefab);
			_main.transform.SetParent(_canvas.transform);
			_main.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
			_main.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0);
			
			//------------------keyPanel-------------------
			GameObject upButton = _main.transform.Find ("KeyPanel").Find ("Up").gameObject;
			GameObject downButton = _main.transform.Find ("KeyPanel").Find ("Down").gameObject;
			GameObject leftButton = _main.transform.Find ("KeyPanel").Find ("Left").gameObject;
			GameObject rightButton = _main.transform.Find ("KeyPanel").Find ("Right").gameObject;
			
			
			EventTriggerListener upTriggerListener = EventTriggerListener.Get (upButton);
			EventTriggerListener downTriggerListener = EventTriggerListener.Get (downButton);
			EventTriggerListener leftTriggerListener = EventTriggerListener.Get (leftButton);
			EventTriggerListener rightTriggerListener = EventTriggerListener.Get (rightButton);
			
			upTriggerListener.onPointerDown = upDownHandler;
			upTriggerListener.onPointerUp = upUpHandler;
			downTriggerListener.onPointerDown = downDownHandler;
			downTriggerListener.onPointerUp = downUpHandler;
			leftTriggerListener.onPointerDown = leftDownHandler;
			leftTriggerListener.onPointerUp = leftUpHandler;
			rightTriggerListener.onPointerDown = rightDownHandler;
			rightTriggerListener.onPointerUp = rightUpHandler;
			
			GameObject attackButton = _main.transform.Find ("AttackBtn").gameObject;
			EventTriggerListener attackTriggerListener = EventTriggerListener.Get (attackButton);
			attackTriggerListener.onPointerClick = attackClickHandler;
		}

		private void upDownHandler(GameObject target)
		{
			keyPanelHandler ("up", "down");
		}
		private void upUpHandler(GameObject target)
		{
			keyPanelHandler ("up", "up");
		}
		private void downDownHandler(GameObject target)
		{
			keyPanelHandler ("down", "down");
		}
		private void downUpHandler(GameObject target)
		{
			keyPanelHandler ("down", "up");
		}
		private void leftDownHandler(GameObject target)
		{
			keyPanelHandler ("left", "down");
		}
		private void leftUpHandler(GameObject target)
		{
			keyPanelHandler ("left", "up");
		}
		private void rightDownHandler(GameObject target)
		{
			keyPanelHandler ("right", "down");
		}
		private void rightUpHandler(GameObject target)
		{
			keyPanelHandler ("right", "up");
		}

		private void keyPanelHandler(string direction, string keyType)
		{
			if (keyType == "up") {
				GameView.getHero().GetComponent<ManualMoveComponent>().stopMove();
			}
			
			if (keyType == "down") {
				GameView.getHero().GetComponent<ManualMoveComponent>().move(direction);
			}
		}

		private void attackClickHandler(GameObject target)
		{
			GameView.getHero().GetComponent<GunComponent>().shoot ();
		}
	}
}
