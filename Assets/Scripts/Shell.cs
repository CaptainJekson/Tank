using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ShellLaunch(float force)
    {
        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
