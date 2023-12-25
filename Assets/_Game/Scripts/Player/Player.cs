using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxLookAngle = 90f;
    [SerializeField] private float _baseSensivity = 5f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _moveSpeed = 10f;

    [Space]
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _directionTransform;
    
    private CharacterController _character;
    private float _cameraXRotation;
    private float _cameraYRotation;
    private Vector2 _movementInput;

    private void Awake()
    {
        LocalPlayer.Initialize(this);

        _character = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SetCursorLockState(true);

        AssetsManager.InstantiateAsset("Assets/_Game/Prefabs/Environment/House.prefab", Vector3.up * 10f, Quaternion.identity);
    }

    private void Update()
    {
        ProcessInput();
        Movement();
        RotateCamera();
        RotatePlayer();
    }

    private void SetCursorLockState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    private void ProcessInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
            SetCursorLockState(false);
        else
            SetCursorLockState(true);
    }

    private void RotateCamera()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _cameraXRotation -= Input.GetAxis("Mouse Y") * _baseSensivity * Time.deltaTime;
            _cameraYRotation += Input.GetAxis("Mouse X") * _baseSensivity * Time.deltaTime;
        
            _cameraXRotation = Mathf.Clamp(_cameraXRotation, -_maxLookAngle, _maxLookAngle);
        }

        _camera.transform.rotation = Quaternion.Euler(new Vector3(_cameraXRotation, _cameraYRotation, 0f));
    }

    private void RotatePlayer()
    {
        _directionTransform.rotation = Quaternion.Euler(Vector3.up * _camera.transform.eulerAngles.y);
    }

    private void Movement()
    {
        var movement = GetMoveDirection() * _moveSpeed;
        var velocity = new Vector3(movement.x, _gravity, movement.z);

        _character.Move(velocity * Time.deltaTime);
    }

    private Vector3 GetMoveDirection()
    {
        var direction = new Vector3(_movementInput.x, 0f, _movementInput.y);

        return _directionTransform.TransformDirection(direction).normalized;
    }
}