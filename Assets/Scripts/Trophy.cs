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
        var endBoardScript = FindObjectOfType<EndBoardScript>();
        endPanel = endBoardScript.endPanel;
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Gong");
        }

        PauseMenuScript.instance.setUIelements(false);
        endPanel.SetActive(true);
        float precentage = ((float)PlayerPrefs.GetInt("score") / (float)PlayerPrefs.GetInt("totalscore")) * 100f;
        endText.text += $"{precentage}% of the game!";
        TimeManager.instance.StopTime();
    }
}
