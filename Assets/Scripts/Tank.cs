using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Tank : MonoBehaviour
{
    [Header("Характеристики движения")]
    [SerializeField] [Range(0, 20)] private float _speed;
    [SerializeField] [Range(0, 20)] private float _reverceSpeed;
    [SerializeField] [Range(0, 300)] private float _rotationSpeed;
    [SerializeField] [Range(0, 5)] private float _rotateSpeedTower;

    [Header("Гусеничные ленты")]
    [SerializeField] private Caterpillar _caterpillarLeft;
    [SerializeField] private Caterpillar _caterpillarRight;

    private Rigidbody _rigidbody;
    private bool _isRotate;

    public float RotateSpeedTower => _rotateSpeedTower;

    public event UnityAction ButtonFireClick;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ToControl(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        ToShoot(KeyCode.Mouse0);
    }

    private void ToControl(KeyCode forward, KeyCode backward, KeyCode leftRotate, KeyCode rightRotate)
    {
        if (Input.GetKey(forward))
        {
            _caterpillarLeft.Move(transform.forward, _speed, true);
            _caterpillarRight.Move(transform.forward, _speed, true);
            _isRotate = false;
        }
        if (Input.GetKey(backward))
        {
            _caterpillarLeft.Move(-transform.forward, _reverceSpeed, false);
            _caterpillarRight.Move(-transform.forward, _reverceSpeed, false);
            _isRotate = false;
        }
        if (Input.GetKey(leftRotate))
        {
            Rotate(-Vector3.up);
            _caterpillarLeft.SetIsForward(false);
            _caterpillarRight.SetIsForward(true);
            _isRotate = true;
        }
        if (Input.GetKey(rightRotate))
        {
            Rotate(Vector3.up);
            _caterpillarLeft.SetIsForward(true);
            _caterpillarRight.SetIsForward(false);
            _isRotate = true;
        }

        TransmissionToTracks(_isRotate);
    }

    private void ToShoot(KeyCode fire)
    {
        if (Input.GetKey(fire))
            ButtonFireClick?.Invoke();
    }

    private void TransmissionToTracks(bool isRotate)
    {
        float currentSpeed = _rigidbody.velocity.magnitude;

        _caterpillarLeft.Velocity = isRotate ? currentSpeed * 2.0f: currentSpeed;
        _caterpillarRight.Velocity = isRotate ? currentSpeed * 2.0f : currentSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(direction * _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(newRotation);
    }
}
