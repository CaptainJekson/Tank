using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _camera;

    [SerializeField] [Range(1, 20)] private float _timeLerp;

    [SerializeField] [Range(50, 300)] private float _mouseSensitivity;
    [SerializeField] [Range(-20, 20)] private float _max;
    [SerializeField] [Range(0, 60)] private float _min;

    float xRotation = 0.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _timeLerp * Time.deltaTime);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _max, _min);

        _camera.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
