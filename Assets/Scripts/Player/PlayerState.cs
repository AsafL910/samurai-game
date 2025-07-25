public class PlayerState
{
    public static bool shouldResetPlayer = false;
    private static PlayerStatus playerStatus;
    public static PlayerStatus GetPlayerStatus()
    {
        if (playerStatus == null)
        {
            playerStatus = new PlayerStatus();
            playerStatus.NewStart();
        }

        return playerStatus;
    }

    public static void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }

}