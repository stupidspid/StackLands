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
        
        var result = Physics2D.Raycast(worldPos, Vector3.forward,
            inputSettings.RaycastDistance, inputSettings.SupportedLayers);
        
        if(!result)
            return;

        var dragableItem = result.transform.GetComponentInChildren<DragableItem>();
            
        if(dragableItem == null)
            return;

        StartCoroutine(DragItem(dragableItem));
    }

    private IEnumerator DragItem(DragableItem dragableItem)
    {
        Debug.Log("n");
        dragableItem.transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        Debug.Log("nnn");
    } 
}
