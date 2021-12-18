using Mirror;
using UnityEngine;

namespace Game.Player.Controllers
{
    public class SpectatorController : NetworkBehaviour
    {
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private float _playerSpeed = 2.0f;
        private float _jumpHeight = 1.0f;

        
        private void Start()
        {
            _controller = gameObject.GetComponent<CharacterController>();
            _controller.detectCollisions = false;
        }

        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform.GetChild(0));
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        }

        private void Update()
        {
            if (hasAuthority)
                CalculateMovement();
        }

        void CalculateMovement()
        {
            Vector3 move = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
            move = transform.transform.TransformDirection(move);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _controller.Move(move * Time.deltaTime * _playerSpeed * 2);
            }
            else
            {
                _controller.Move(move * Time.deltaTime * _playerSpeed);
            }
            
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}