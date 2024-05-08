using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatoinAndMovementController : MonoBehaviour
{
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    [SerializeField] private float _rotationFactorPerFrame = 10f;
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _multiplierSpeed = 3f;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currenRunMovment;
    private bool _isMovementPressed;
    private bool _isRunPressed;
    int _isWalkingHash;
    int _isRunningHash;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;

        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
    }

    private void HandleGravity()
    {
        if(_characterController.isGrounded)
        {
            float _groundedGravity = .05f;
            _currentMovement.y = _groundedGravity;
            _currenRunMovment.y = _groundedGravity;
        }
        else
        {
            float _gravity = -9.8f;
            _currentMovement.y += _gravity;
            _currenRunMovment.y += _gravity;
        }
    }

   private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();

        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;

        _currenRunMovment.x = _currentMovementInput.x * _multiplierSpeed;
        _currenRunMovment.z = _currentMovementInput.y * _multiplierSpeed;

        _isMovementPressed = _currentMovementInput != Vector2.zero;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

   private void HandleAnimation()
    {
        bool _isWalking = _animator.GetBool(_isWalkingHash);
        bool _isRunning = _animator.GetBool(_isRunningHash);

        if(_isMovementPressed && !_isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if(!_isMovementPressed && _isWalking)
        {
           _animator.SetBool(_isWalkingHash, false);
        }

        if ((_isMovementPressed && _isRunPressed) && !_isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        else if ((!_isMovementPressed || !_isRunPressed) && _isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

   private void HandleRotation()
    {
        Vector3 _positionToLookAt;

        _positionToLookAt.x = _currentMovement.x;
        _positionToLookAt.y = 0.0f;
        _positionToLookAt.z = _currentMovement.z;

        Quaternion _currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation =  Quaternion.Slerp(_currentRotation, _targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void Update()
    {
        HandleAnimation();
        HandleRotation();

        if (_isRunPressed)
        {
            _characterController.Move(_currenRunMovment * _movementSpeed * Time.deltaTime);
        }
        else
        {
            _characterController.Move(_currentMovement * _movementSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }
}