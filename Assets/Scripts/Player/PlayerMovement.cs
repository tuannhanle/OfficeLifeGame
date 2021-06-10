using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public enum PlayerMomvementState { Freeze, Idle, Run, Walk }

public class PlayerMovement : MonoBehaviour
{
    [Header("Change Lane")]

    //[SerializeField] private int _currentLane = 1;
    //[SerializeField] private float _targetPosion = 0;
    [SerializeField] private float _distanceTwoLanes;
    [SerializeField] private float _changeLaneSpeed;

    [Header("Physically")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxDistanceAmount;

    //private CharacterController _characterController;
    private Transform _characterController;
    private PlayerController _playerController;
    private Vector3 _playerVelocity;
    private float _gravityValue = -9.81f;
    private float _defaultSpeed, _defaulChangeLaneSpeed;
    private bool _isGroundedPlayer = true;
    public void Test()
    {
       
    }

    private void Awake()

    {
        _defaultSpeed = _speed;
        _defaulChangeLaneSpeed = _changeLaneSpeed;

        //_characterController = GetComponent<CharacterController>();
        _characterController = transform;

        _playerController = GetComponent<PlayerController>();
        _gravityValue = Physics.gravity.y;
        //SetMovementState(PlayerMomvementState.Idle);
        this.RegisterListener(EventID.OnCastMovementState, (o) => SetMovementState((PlayerMomvementState)o));
    }
    private void Update()
    {
        SetRunMovement();
    }
    private void SetMovementState(PlayerMomvementState playerMomvementState)
    {
        switch (playerMomvementState)
        {
            case PlayerMomvementState.Freeze:
                _speed = 0f;
                _changeLaneSpeed = 0f;
                break;
            case PlayerMomvementState.Idle:
                _speed = 0f;
                break;
            case PlayerMomvementState.Run:
                _speed = _defaultSpeed;
                _changeLaneSpeed = _defaulChangeLaneSpeed;
                break;
            case PlayerMomvementState.Walk:
                _speed = _defaultSpeed/2f;
                break;
            default:
                break;
        }
    }

    //public void ChangeLane(int direction) // +1/-1
    //{
    //    if (!_isRunning)
    //        return;

    //    int targetLane = _currentLane + direction;

    //    if (targetLane < 0 || targetLane > 3)
    //        // Ignore, we are on the borders.
    //        return;

    //    _currentLane = targetLane;
    //    _targetPosion = (_currentLane - 1) * _distanceTwoLanes;
    //}
    public void SetRunMovement()
    {
        // cho nhan vat rot
        //_isGroundedPlayer = _characterController.isGrounded;
        //if (_isGroundedPlayer && _playerVelocity.y < 0)  // make sure player stand on ground
        //{
        //    _playerVelocity.y = 0f;
        //}
        _playerVelocity.y = 0f; //add new


        _playerVelocity.y += _gravityValue * Time.deltaTime;
        //_characterController.Translate(_playerVelocity * Time.deltaTime * _jumpForce);

        //trai phai

        var target = new Vector3(transform.position.x, transform.position.y, transform.position.z + _playerController.moveHorizontal);

        if (this.transform.position.z <= _maxDistanceAmount && this.transform.position.z >= -_maxDistanceAmount)
        {
            _characterController.transform.position = Vector3.MoveTowards(_characterController.transform.position, target, _changeLaneSpeed * Time.deltaTime);

        }
        if (this.transform.position.z >= _maxDistanceAmount)
        {
            _characterController.transform.position =  new Vector3(transform.position.x, transform.position.y, _maxDistanceAmount);
        }
        if (this.transform.position.z <= - _maxDistanceAmount)
        {
            _characterController.transform.position = new Vector3(transform.position.x, transform.position.y, -_maxDistanceAmount);
        }

        //di thang
        var moveForward = Vector3.forward;
        _characterController.Translate(moveForward * Time.deltaTime * _speed);




        //var target  = new Vector3(transform.position.x, transform.position.y, _targetPosion);
        //_characterController.transform.position = Vector3.MoveTowards(_characterController.transform.position, target, _changeLaneSpeed * Time.deltaTime);

    }


    //public void Jump()
    //{
    //    if (_isGroundedPlayer)
    //    {
    //        _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);

    //    }
    //}
}
