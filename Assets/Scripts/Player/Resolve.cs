using UnityEngine;

public class Resolve : MonoBehaviour
{
    private PlayerStatus player;
    public float fillSpeed;

    void Start()
    {
        Debug.Log(PlayerState.GetPlayerStatus().GetSceneIndex());
        player = PlayerState.GetPlayerStatus();
    }

    public void Update()
    {
        if (player.GetTotalResolve() > player.GetResolve())
        {
            player.SetResolve(player.GetResolve() + Time.deltaTime * fillSpeed);
        }
        else
        {
            player.SetResolve(player.GetTotalResolve());
        }

    }

}
