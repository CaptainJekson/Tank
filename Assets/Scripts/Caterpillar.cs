using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Caterpillar : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    public float Velocity
    {
        get { return _animator.speed; }
        set { _animator.speed = value; }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector3 direction, float speed, bool isForward)
    {
        Vector3 newPosition = transform.position + (direction * speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);

        _animator.SetBool("Forward", isForward);
    }

    public void SetIsForward(bool isForward)
    {
        _animator.SetBool("Forward", isForward);
    }
}
