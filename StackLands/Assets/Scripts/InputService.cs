using System.Collections;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputService : MonoBehaviour
{
    [SerializeField] private InputServiceSettings inputSettings;

    private Camera _mainCamera;
        
    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
        
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            ProcessInput(Input.mousePosition);
    }

    private void ProcessInput(Vector3 screenPosition)
    {
        var isOverUI = EventSystem.current.IsPointerOverGameObject();
        if(isOverUI)
            return;
            
        var worldPos = _mainCamera.ScreenToWorldPoint(screenPosition);
        
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * inputSettings.RaycastDistance, Color.red, 2f);

        
        if (Physics.Raycast(ray, out hit, inputSettings.RaycastDistance, inputSettings.SupportedLayers))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("Raycast did not hit any object.");
            Debug.Log("Ray origin: " + ray.origin);
            Debug.Log("Ray direction: " + ray.direction);
            Debug.Log("Raycast distance: " + inputSettings.RaycastDistance);
            Debug.Log("Supported layers: " + inputSettings.SupportedLayers.value);
        }
        
        var result = Physics.Raycast(worldPos, Vector3.forward,
            inputSettings.RaycastDistance, inputSettings.SupportedLayers);

        
        if(!result)
            return;

        // var dragableItem = result.transform.GetComponentInChildren<DragableItem>();
        //     
        // if(dragableItem == null)
        //     return;
        //
        // StartCoroutine(DragItem(dragableItem));
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 100;
        Gizmos.DrawRay(transform.position, direction);
    }

    private IEnumerator DragItem(DragableItem dragableItem)
    {
        Debug.Log("n");
        dragableItem.transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        Debug.Log("nnn");
    } 
}
