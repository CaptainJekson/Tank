using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Tank _tank;

    private void FixedUpdate()
    {
        ToRotate();
    }

    private void ToRotate()
    {
        Vector3 direction = _target.position - transform.position;
        direction = Quaternion.Inverse(transform.parent.rotation) * direction;
        Vector3 directionXZ = new Vector3(direction.x, 0.0f, direction.z);

        Quaternion rotation = Quaternion.LookRotation(directionXZ, Vector3.up);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, _tank.RotateSpeedTower * Time.deltaTime);
    }
}
