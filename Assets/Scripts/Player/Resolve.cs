using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolve : MonoBehaviour
{
    public PlayerStatus player;
    public float fillSpeed;

    public void Update()
    {
        if (player.GetTotalResolve() > player.GetResolve())
        {
            player.SetResolve( player.GetResolve() + Time.deltaTime * fillSpeed);
        }
        else
        {
            player.SetResolve(player.GetTotalResolve());
        }

    }

}
