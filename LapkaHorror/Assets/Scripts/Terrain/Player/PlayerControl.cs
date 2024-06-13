using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public CharacterController _playerController;
    [SerializeField] public Transform _groundCheck;
    [SerializeField] public Transform _respawnPoint; 
    [SerializeField] public LayerMask _groundMask;
    
    private Dictionary<string, KeyCode> _movementKeys;
    
    public float _speed = 10.0f;
    public float _jumpHeight = 2f;

    private float _groundDistance = 0.4f;
    private float _gravity = -9.81f;
    
    private bool _isGrounded;

    private Vector3 _moveDirection;
    private Vector3 _velocity;

    private void Start()
    {
        _movementKeys = KeyBindingManager.Instance.GetMovementKeyBindings(); 
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
        CheckGroundStatus();
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        ProcessMovement();
        ProcessJump();
    }

    private void CheckGroundStatus()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void ProcessMovement()
    {
        _moveDirection = Vector3.zero;

        if (Input.GetKey(_movementKeys["MoveForwardBtn"]))
        {
            _moveDirection += transform.forward;
        }

        if (Input.GetKey(_movementKeys["MoveBackBtn"]))
        {
            _moveDirection -= transform.forward;
        }

        if (Input.GetKey(_movementKeys["MoveLeftBtn"]))
        {
            _moveDirection -= transform.right;
        }

        if (Input.GetKey(_movementKeys["MoveRightBtn"]))
        {
            _moveDirection += transform.right;
        }

        _playerController.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void ProcessJump()
    {
        if (Input.GetKey(_movementKeys["JumpBtn"]) && _isGrounded)
        {
            _velocity.y = 0;
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }
}