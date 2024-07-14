using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputService : MonoBehaviour
{
    [SerializeField] private InputServiceSettings inputSettings;

    private Camera _mainCamera;
    private Coroutine _dragCoroutine;
    private IDraggableItem _draggableItem;
        
    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
        
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessInput(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && _dragCoroutine != null)
        {
            StopCoroutine(_dragCoroutine);
            _draggableItem.OnDragEnd();
            _dragCoroutine = null;
        }
    }

    private void ProcessInput(Vector3 screenPosition)
    {
        _draggableItem = null;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
            
        var worldPos = _mainCamera.ScreenToWorldPoint(screenPosition);
        var result = Physics2D.Raycast(worldPos, Vector3.forward, 
                                       inputSettings.RaycastDistance, inputSettings.SupportedLayers);
        
        if (result.collider == null)
        {
            return;
        }

        _draggableItem = result.transform.GetComponentInChildren<IDraggableItem>();

        OnClick(result.transform.gameObject);
            
        if (_draggableItem == null)
        {
            return;
        }

        _draggableItem.OnDrag();
        _dragCoroutine = StartCoroutine(DragItemCoroutine(result.transform));
    }

    private void OnClick(GameObject clickedObject)
    {
        IDraggableItem draggable = clickedObject.GetComponent<IDraggableItem>();
        if (draggable != null)
        {
            draggable.OnClick();
        }
    }

    private IEnumerator DragItemCoroutine(Transform draggableItem)
    {
        while (!Input.GetMouseButtonUp(0))
        {
            Vector2 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            draggableItem.position = new Vector3(newPosition.x, newPosition.y, draggableItem.position.z);
            yield return null;
        }
    } 
}