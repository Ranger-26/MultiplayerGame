using UnityEngine;

namespace Player.Controllers
{
    public class LookX : MonoBehaviour
    {
        private float _sensitivity = 5f;

        void Update()
        {
            float _mouseX = Input.GetAxis("Mouse X");

            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += (_sensitivity * _mouseX);
            transform.localEulerAngles = newRotation;
        }
    }
}