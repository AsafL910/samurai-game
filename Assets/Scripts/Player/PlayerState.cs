public class PlayerState
{
    private static PlayerStatus playerStatus;
    public static PlayerStatus GetPlayerStatus()
    {
        return playerStatus;
    }

    public static void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }

}