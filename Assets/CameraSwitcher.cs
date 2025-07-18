using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.usePhysicalProperties = true;
        mainCamera.sensorSize = new Vector2(55f, 30f);
        mainCamera.fieldOfView = 60;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mainCamera.usePhysicalProperties = true;
            mainCamera.sensorSize = new Vector2(55f, 30f);
            mainCamera.fieldOfView = 60;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // mainCamera.usePhysicalProperties = false;
            mainCamera.sensorSize = new Vector2(36f, 24f);
            mainCamera.fieldOfView = 50;
        }
    }
}
