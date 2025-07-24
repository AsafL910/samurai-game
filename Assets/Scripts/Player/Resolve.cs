using UnityEngine;

public class Resolve : MonoBehaviour
{
    public float fillSpeed;

    void Start()
    {
        Debug.Log(PlayerState.GetPlayerStatus().GetSceneIndex());
    }

    public void Update()
    {
        if (PlayerState.GetPlayerStatus().GetTotalResolve() > PlayerState.GetPlayerStatus().GetResolve())
        {
            PlayerState.GetPlayerStatus().SetResolve(PlayerState.GetPlayerStatus().GetResolve() + Time.deltaTime * fillSpeed);
        }
        else
        {
            PlayerState.GetPlayerStatus().SetResolve(PlayerState.GetPlayerStatus().GetTotalResolve());
        }

    }

}
