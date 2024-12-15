using UnityEngine;

namespace Core.Helpers
{
    public static class ScreenPositionHelper
    {
        public static Vector3 GetMiddleMostWorldPosition(Camera cam, float margin = 0f)
        {
            var screenPosition = new Vector3(
                Screen.width / 2f + margin, // Horizontal position based on width percentage
                Screen.height / 2f + margin, // Vertical position based on height percentage
                10
            );
            return cam.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 GetLeftMostWorldPosition(Camera cam, float margin = 0f)
        {
            var screenPosition = new Vector3(
                margin, // Horizontal position based on width percentage
                0, // Vertical position based on height percentage
                10
            );
            return cam.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 GetRightMostWorldPosition(Camera cam, float margin = 0f)
        {
            var screenPosition = new Vector3(
                Screen.width + margin, // Horizontal position based on width percentage
                0, // Vertical position based on height percentage
                10
            );
            return cam.ScreenToWorldPoint(screenPosition);
        }
    }
}