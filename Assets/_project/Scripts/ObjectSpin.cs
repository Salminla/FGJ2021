using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    public float speed = 85;
    public Vector3 axis = Vector3.forward;
    void Update()
    {
        transform.Rotate(axis * (speed * Time.deltaTime));
    }
}
