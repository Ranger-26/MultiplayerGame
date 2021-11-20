using UnityEngine;

namespace Player.Controllers
{
    public class LookY : MonoBehaviour
    {
        private float _sensitivity = 5f;

        void Update()
        {
            float _mouseY = Input.GetAxis("Mouse Y");

            Vector3 newRotation = transform.localEulerAngles;
            newRotation.x += -_sensitivity * _mouseY;
            transform.localEulerAngles = newRotation;
        }
    }
}