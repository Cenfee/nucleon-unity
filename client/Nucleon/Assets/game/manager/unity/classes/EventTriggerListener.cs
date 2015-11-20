using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace game.manager.unity.classes
{
	public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
	{
		public delegate void VoidDelegate (GameObject go);
		public VoidDelegate onPointerClick;
		public VoidDelegate onPointerDown;
		public VoidDelegate onPointerEnter;
		public VoidDelegate onPointerExit;
		public VoidDelegate onPointerUp;
		public VoidDelegate onSelect;
		public VoidDelegate onUpdateSelect;

		
		static public EventTriggerListener Get (GameObject go)
		{
			EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
			if (listener == null) listener = go.AddComponent<EventTriggerListener>();
			return listener;
		}
		public override void OnPointerClick(PointerEventData eventData)
		{
			if(onPointerClick != null) 	onPointerClick(gameObject);
		}
		public override void OnPointerDown (PointerEventData eventData)
		{
			if(onPointerDown != null) onPointerDown(gameObject);
		}
		public override void OnPointerEnter (PointerEventData eventData)
		{

			if(onPointerEnter != null) onPointerEnter(gameObject);
		}
		public override void OnPointerExit (PointerEventData eventData)
		{
			if(onPointerExit != null) onPointerExit(gameObject);
		}
		public override void OnPointerUp (PointerEventData eventData)
		{
			if(onPointerUp != null) onPointerUp(gameObject);
		}
		public override void OnSelect (BaseEventData eventData)
		{
			if(onSelect != null) onSelect(gameObject);
		}
		public override void OnUpdateSelected (BaseEventData eventData)
		{
			if(onUpdateSelect != null) onUpdateSelect(gameObject);
		}

	}
}