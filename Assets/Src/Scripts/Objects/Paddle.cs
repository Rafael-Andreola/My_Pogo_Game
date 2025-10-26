using UnityEngine;

public class Paddle
{
    private PlayerType playerType;
    private PlayerSide playerSide;

    public Paddle(PlayerType type, PlayerSide side)
    {
        playerType = type;
        playerSide = side;
    }

    public PlayerType GetPlayerType() => playerType;
    public PlayerSide GetPlayerSide() => playerSide;
}
