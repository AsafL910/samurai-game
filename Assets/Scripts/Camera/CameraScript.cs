using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerTarget;

    private Vector3 velocity = Vector3.zero;

    public Vector3 offset;

    public static CameraScript instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
        private void LateUpdate()
    {
        Vector3 cameraPosition = playerTarget.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, velocity,);
    }

}
