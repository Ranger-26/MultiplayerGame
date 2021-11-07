using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerController : NetworkBehaviour
    {
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private float _playerSpeed = 2.0f;
        private float _jumpHeight = 1.0f;
        private float _gravityValue = -30.81f;

        private void Start()
        {
            _controller = gameObject.GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (hasAuthority)
                CalculateMovement();
        }

        void CalculateMovement()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.transform.TransformDirection(move);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _controller.Move(move * Time.deltaTime * _playerSpeed * 2);
            }
            else
            {
                _controller.Move(move * Time.deltaTime * _playerSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
