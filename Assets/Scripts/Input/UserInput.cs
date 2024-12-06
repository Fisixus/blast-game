using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class UserInput : MonoBehaviour
    {
        private Camera _cam;
        private EventSystem _eventSystem;

        private bool _isInputOff = false;
        private IA_User _userActions;
        
        private void Awake()
        {
            _cam = Camera.main;
            _eventSystem = EventSystem.current;
            _userActions.Match.Enable();
            _userActions.Match.Touch.performed += TouchItemNotifier;
        }

        private void OnDisable()
        {
            //m_SignalBus.Unsubscribe<OnInputStateChangedSignal>(SetInputState);
        }


        // private void SetInputState(OnInputStateChangedSignal args)
        // {
        //     _isInputOff = args.IsInputOff;
        // }

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
            if (IsPointerOverUIObject() || _isInputOff)
                return;

            var hit = Physics2D.Raycast(_cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero);
            //if (hit && hit.transform.TryGetComponent<BaseGridObject>(out var gridObject))
                //m_SignalBus.Fire(new OnGridObjectTouchedSignal() { GridObject = gridObject });
        }
    }
}