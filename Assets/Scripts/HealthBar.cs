using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    private float _maxHealth;

    public void ResetHealthBar(float maxHealth = 1)
    {
        _maxHealth = maxHealth;
        healthBarImage.fillAmount = 1;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        healthBarImage.fillAmount = currentHealth / _maxHealth;
    }
}
