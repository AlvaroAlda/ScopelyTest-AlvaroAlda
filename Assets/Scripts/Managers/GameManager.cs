using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameEvents gameEvents;

    public bool LevelFailed { get; private set; }
    public bool LevelWin { get; private set; }

    private void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        LevelFailed = false;
        LevelWin = false;

        gameEvents.TriggerGameStart();
    }

    public void WinGame()
    {
        if (LevelFailed)
            return;

        LevelWin = true;
        gameEvents.TriggerGameWin();
    }

    public void LoseGame()
    {
        if (LevelWin)
            return;

        LevelFailed = true;
        gameEvents.TriggerGameLost();
    }
}