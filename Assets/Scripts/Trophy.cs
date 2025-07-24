using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : MonoBehaviour
{

    public TMP_Text endText;
    public GameObject endPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Gong");
        }

        // Destroy(gameObject);
        PauseMenuScript.instance.setUIelements(false);
        endPanel.SetActive(true);
        endText.text += "112% of the game!";
        TimeManager.instance.StopTime();
    }
}
