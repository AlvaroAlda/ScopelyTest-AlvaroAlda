using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Turrets
{
    public class TurretButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private Image image;
        
        public void InitializeButton(Turret turret, UnityAction action)
        {
            var turretData = turret.TurretData;
            
            text.text = turretData.SpawnPrize.ToString();
            image.sprite = turretData.DisplaySprite;
            button.onClick.AddListener(action);
        }
    }
}
