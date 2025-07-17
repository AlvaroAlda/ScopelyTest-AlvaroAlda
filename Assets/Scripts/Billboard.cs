using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool doConstantUpdate;
    private Camera _mainCamera;
    
    private void Awake()
    {
        _mainCamera = Camera.main;

        UpdateBillboard();
    }

    private void LateUpdate()
    {
        if (doConstantUpdate)
            UpdateBillboard();
    }
    
    private void UpdateBillboard()
    {
        if(_mainCamera)
            transform.LookAt(transform.position + _mainCamera.transform.forward);
    }
}
