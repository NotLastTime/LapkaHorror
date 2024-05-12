using UnityEngine;

namespace Services
{
    public class PlayerMovementService
    {
        private Transform _playerTransform;
        private Transform _cameraTransform;

        public PlayerMovementService(Transform playerTransform, Transform cameraTransform)
        {
            _playerTransform = playerTransform;
            _cameraTransform = cameraTransform;
        }

        public void Move(float playerSpeed)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _playerTransform.Translate(direction * playerSpeed * Time.deltaTime);
        }

        public void Rotate(float mouseSensitivity)
        {
            _cameraTransform.Rotate(Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime * Vector3.left);
            _playerTransform.Rotate(Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime * Vector3.up);
        }
    }
}