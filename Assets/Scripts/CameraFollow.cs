using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        // keep camera centered on the target, preserve camera's z position
        Vector3 pos = target.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
