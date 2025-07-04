using UnityEngine;

public class SpinZAxis : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Rotate(Vector3.up, -speed * Time.deltaTime);
    }
}
