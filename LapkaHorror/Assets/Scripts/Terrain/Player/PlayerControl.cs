using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public CharacterController _playerController;
    [SerializeField] public Transform _groundCheck;
    [SerializeField] public Transform _respawnPoint; 
    [SerializeField] public LayerMask _groundMask;
    
    public float _speed = 10.0f;
    public float _jumpHeight = 2f;

    private float _groundDistance = 0.4f;
    private float _gravity = -9.81f;
    
    private float _horizontal;
    private float _vertical;
    private bool _isGrounded;

    private Vector3 _moveDirection;
    private Vector3 _velocity;

    private void Start()
    {
        RespawnCharacter();
    }

    private void RespawnCharacter()
    {
        GetComponent<CharacterController>().enabled = false;

        if (_respawnPoint != null && _playerController != null)
        {
            _playerController.transform.position = _respawnPoint.transform.position;
        }
        
        GetComponent<CharacterController>().enabled = true;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void FixedUpdate()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _moveDirection = transform.right *_horizontal + transform.forward *_vertical;
        _playerController.Move(_moveDirection * _speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
    }
}
