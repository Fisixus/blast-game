using System.Collections.Generic;
using Core.GridElements.GridPawns;
using Events;
using Events.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using OnGridObjectTouchedEvent = Events.Grid.OnGridObjectTouchedEvent;

namespace Input
{
    public class UserInput : MonoBehaviour
    {
        private Camera _cam;
        private EventSystem _eventSystem;

        private bool _isInputOn = true;
        
        private void Awake()
        {
            var userActions = new IA_User(); // Instantiate the input actions class
            userActions.Match.Enable(); // Enable the specific action map
            userActions.Match.Touch.performed += TouchItemNotifier; // Subscribe to the action
            
            _cam = Camera.main;
            _eventSystem = EventSystem.current;
            
            GameEventSystem.AddListener<OnInputStateChangedEvent>(SetInputState);
        }

        private void OnDestroy()
        {
            GameEventSystem.RemoveListener<OnInputStateChangedEvent>(SetInputState);
        }


        private void SetInputState(object args)
        {
            var argsEvent = (OnInputStateChangedEvent)args;
            _isInputOn = argsEvent.IsInputOn;
        }

        private bool IsPointerOverUIObject()
        {
            // Create PointerEventData for the current event system
            PointerEventData eventData = new PointerEventData(_eventSystem);

#if UNITY_EDITOR || UNITY_STANDALONE
            // Use mouse position for PC builds and the Unity editor
            eventData.position = UnityEngine.Input.mousePosition;
#else
        // Use touch position for mobile devices
        if (UnityEngine.Input.touchCount > 0)
            eventData.position = UnityEngine.Input.GetTouch(0).position;
        else
            return false;
#endif

            // Perform a raycast and check if any UI elements were hit
            List<RaycastResult> results = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, results);

            // Return true if any UI elements were hit, false otherwise
            return results.Count > 0;
        }

        private void TouchItemNotifier(InputAction.CallbackContext context)
        {
            if (IsPointerOverUIObject() || !_isInputOn)
                return;
            var hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero);
            if (hit && hit.transform.TryGetComponent<BaseGridObject>(out var gridObject))
            {
                GameEventSystem.Invoke<OnGridObjectTouchedEvent>(new OnGridObjectTouchedEvent() { GridObject = gridObject });
            }
        }
    }
}