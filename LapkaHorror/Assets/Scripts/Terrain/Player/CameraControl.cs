using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] public Transform _player;
    
    public float _mouseSensivity = 100f;
   
    private float _xRotation = 0f;

    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation  =Quaternion.Euler(_xRotation, 0f, 0f);
        _player.Rotate(Vector3.up * _mouseX);
    }

}
