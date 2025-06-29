using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusBars : MonoBehaviour
{
    public Image health;
    public Image resolve;
    public PlayerStatus player;
    public Material glowMat;
    private Material defaultMat;

    public static UpdateStatusBars instance;
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
        private void Start()
	{
        defaultMat = resolve.material;
	}

	void Update()
    {
        UpdateStatusBar(health,player.GetHP(),player.GetTotalHP());
        UpdateStatusBar(resolve, player.GetResolve(), player.GetTotalResolve());

        if (player.GetResolve() == player.GetTotalResolve())
        {
            resolve.material = glowMat;
        }
        else 
        {
            resolve.material = defaultMat;
        }
    }

    void UpdateStatusBar(Image bar, float curr, float total) 
    {
        bar.fillAmount = curr / total;
    }
}
