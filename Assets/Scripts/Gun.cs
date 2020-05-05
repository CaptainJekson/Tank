using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Tank _tank;
    [SerializeField] private Transform _camera;

    [Header("Отклонение пушки")]
    [SerializeField] [Range(0, 15)] private float _minIncline;
    [SerializeField] [Range(-15, 0)] private float _maxIncline;

    [Header("Снаряд")]
    [SerializeField] private Shell _shell;
    [SerializeField] private Transform _barrel;
    [SerializeField] [Range(0, 500)] private float _forceShell;

    [Header("Характеристики стрельбы")]
    [SerializeField] [Range(0, 10)] private float _shootingRate;

    private float _initialPositionCamera;
    private float _shootCooldown;

    private bool CanShoot => _shootCooldown <= 0.0f;

    private void Awake()
    {
        _initialPositionCamera = _camera.eulerAngles.x;
    }

    private void Update()
    {
        if (_shootCooldown > 0)
            _shootCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ToIncline();
    }

    private void OnEnable()
    {
        _tank.ButtonFireClick += Shoot;
    }

    private void OnDisable()
    {
        _tank.ButtonFireClick -= Shoot;
    }

    private void ToIncline()
    {
        float inclune = Mathf.Clamp(_camera.eulerAngles.x - _initialPositionCamera, _maxIncline, _minIncline);
        transform.localRotation = Quaternion.Euler(inclune, 0.0f, 0.0f);
    }

    private void Shoot()
    {
        if(CanShoot)
        {
            Shell newShell = Instantiate(_shell, _barrel.position, transform.rotation);
            newShell.ShellLaunch(_forceShell);

            _shootCooldown = _shootingRate;
        }
    }
}
