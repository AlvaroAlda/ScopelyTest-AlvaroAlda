using EventChannels.GameEvents;
using Managers;
using UnityEngine;
using UnityEngine.Events;

public class DefendingBase : MonoBehaviour
{
    [SerializeField] private float baseLife;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Gradient gradient;
    [SerializeField] private GameEvents gameEvents;

    private float _currentLife;

    public UnityEvent<float> onBaseReset;
    public UnityEvent<float> onBaseDamaged;

    private void OnEnable()
    {
        gameEvents.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        gameEvents.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        _currentLife = baseLife;
        onBaseReset?.Invoke(_currentLife);
        UpdateHealthVisual();
    }

    public void HitBase(float damage)
    {
        if (_currentLife <= 0)
            return;

        _currentLife -= damage;
        onBaseDamaged?.Invoke(_currentLife);
        
        UpdateHealthVisual();

        if (_currentLife <= 0)
            GameManager.SharedInstance.LoseGame();
    }

    private void UpdateHealthVisual()
    {
        meshRenderer.material.color = gradient.Evaluate(1 - _currentLife / baseLife);
    }
}