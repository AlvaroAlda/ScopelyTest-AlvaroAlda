using System;
using UnityEngine;

public class DefendingBase : MonoBehaviour
{
    [SerializeField] private float baseLife;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Gradient gradient;
    [SerializeField] private GameEvents gameEvents;

    private float _currentLife;
    
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
        UpdateHealthVisual();
    }

    public void HitBase(float damage)
    {
        if (_currentLife <= 0)
            return;
        
        _currentLife -= damage;
        UpdateHealthVisual();
        
        if(_currentLife <= 0)
            GameManager.SharedInstance.LoseGame();
    }

    private void UpdateHealthVisual()
    {
        meshRenderer.material.color = gradient.Evaluate(1 - (_currentLife / baseLife));
    }
}
