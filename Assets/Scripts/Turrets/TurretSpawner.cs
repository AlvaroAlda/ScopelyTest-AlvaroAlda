using CustomObjectPool;
using Managers;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Turrets
{
    public class TurretSpawner : MonoBehaviour
    {
        [SerializeField] private Turret[] availableTurrets;
        [SerializeField] private TurretButton templateButton;

        private Turret _selectedTurret;
        private Camera _mainCamera;
        
        private void Start()
        {
            InitializeButtons();
            SelectTurret(availableTurrets[0]);

            _mainCamera = Camera.main;
        }

        private void InitializeButtons()
        {
            foreach (var turret in availableTurrets)
            {
                var turretButton = Instantiate(templateButton, templateButton.transform.parent);
                turretButton.gameObject.SetActive(true);
                turretButton.InitializeButton(turret, () => SelectTurret(turret));
            }
        }

        private void SelectTurret(Turret turret)
        {
            _selectedTurret = turret;
        }
        
        private void Update()
        {
            if (GameManager.SharedInstance.LevelFailed || GameManager.SharedInstance.LevelWin)
                return;
        
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!CurrencyManager.SharedInstance.TrySpendCurrency(_selectedTurret.TurretData.SpawnPrize))
                    return;
            
                var turret = PoolProvider.SharedInstance.GetPrefab(_selectedTurret);
                var turretPosition = _mainCamera.GetMousePositionInPlaneXZ(Input.mousePosition);
                
                turret.transform.position = turretPosition;
                turret.gameObject.SetActive(true);
            }
        }
    }
}