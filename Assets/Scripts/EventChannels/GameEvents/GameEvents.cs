using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "GameChannel", menuName = "TowerDefense/Channels/Game", order = 0)]
public class GameEvents : ScriptableObject
{
    public UnityAction OnGameWin = delegate { };
    public void TriggerGameWin() => OnGameWin?.Invoke();
    
    public UnityAction OnGameLost = delegate { };
    public void TriggerGameLost() => OnGameLost?.Invoke();
    
    public UnityAction OnGameStart = delegate { };
    public void TriggerGameStart() => OnGameStart?.Invoke();
}
