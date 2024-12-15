using System;
using System.Collections.Generic;
using Core.GridElements.GridPawns;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class UserInput : MonoBehaviour
    {
        private Camera _cam;
        private EventSystem _eventSystem;

        private static bool _isInputOn = true;
        public static event Action<BaseGridObject> OnGridObjectTouched;
        private IA_User _iaUser;

        private void Awake()
        {
            _cam = Camera.main;
            _eventSystem = EventSystem.current;
            _iaUser = new IA_User(); // Instantiate the input actions class
            _iaUser.Match.Enable(); // Enable the specific action map
            _iaUser.Match.Touch.performed += TouchItemNotifier; // Subscribe to the action
        }

        private void OnDestroy()
        {
            _iaUser.Match.Disable();
            _iaUser.Match.Touch.performed -= TouchItemNotifier;
            OnGridObjectTouched = null;
        }

        public static void SetInputState(bool isInputOn)
        {
            _isInputOn = isInputOn;
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
                OnGridObjectTouched?.Invoke(gridObject);
            }
        }
    }
}