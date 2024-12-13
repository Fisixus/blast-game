using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Helpers
{
    public static class ButtonActionHelper
    {
        public static void AssignAsyncAction(Button button, Func<UniTask> asyncAction)
        {
            button.onClick.AddListener(async () =>
            {
                // Disable the button to prevent multiple clicks
                button.interactable = false;

                try
                {
                    // Execute the asynchronous action
                    await asyncAction();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error during button action: {ex.Message}");
                }
            });
        }
    }
}