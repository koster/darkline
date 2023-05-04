
using UnityEngine;

namespace Source.Util
{
    public static class Mouse
    {
        public static Vector3 worldPos  {
            get
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;
                return Camera.main.ScreenToWorldPoint(mousePos);
            }
        }
    }
}