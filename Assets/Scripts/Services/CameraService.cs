using UnityEngine;

namespace Services
{
    public static class CameraService
    {
        public static Vector3 GetMousePositionInPlaneXZ(this Camera camera, Vector3 mousePosition, float distance = 0)
        {
            mousePosition.z = Mathf.Abs(camera.transform.position.y - distance);
            
            var worldPosition = camera.ScreenToWorldPoint(mousePosition);
            worldPosition.y = 0; 
            
            return worldPosition;
        }
    }
}