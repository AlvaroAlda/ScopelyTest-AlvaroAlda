using System;
using UnityEngine;


public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;
    [SerializeField] private GameEvents gameEvents;

    private void OnEnable()
    {
        gameEvents.OnGameLost += OnGameLost;
        gameEvents.OnGameWin += OnGameWin;
    }
    
    private void OnDisable()
    {
        gameEvents.OnGameLost -= OnGameLost;
        gameEvents.OnGameWin -= OnGameWin;
    }

    private void OnGameWin()
    {
        winPopup.gameObject.SetActive(true);
    }

    private void OnGameLost()
    {
        losePopup.gameObject.SetActive(true);
    }

    public void StartNewGame()
    {
        GameManager.SharedInstance.StartNewGame();
    }
}
