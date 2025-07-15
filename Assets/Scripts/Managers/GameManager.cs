using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void StartNewGame()
    {
        
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN");
    }

    public void LoseGame()
    {
        Debug.Log("YOU LOSE");
    }
}
