using UnityEngine;

public class DefendingBase : MonoBehaviour
{
    [SerializeField] private float baseLife;

    private float _currentLife;

    private void OnEnable()
    {
        _currentLife = baseLife;
    }

    public void HitBase(float damage)
    {
        if (_currentLife <= 0)
            return;
        
        _currentLife -= damage;
        if(_currentLife <= 0)
            GameManager.SharedInstance.LoseGame();
    }
}
