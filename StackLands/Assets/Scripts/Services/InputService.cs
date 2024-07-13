using System;
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
    private Coroutine _dragCoroutine;
    private Vector3 _startPosition;
    private IDraggableItem _draggableItem;
        
    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
        
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            ProcessInput(Input.mousePosition);
        if (Input.GetMouseButtonUp(0) && _dragCoroutine != null && _draggableItem != null)
        {
            StopCoroutine(_dragCoroutine);
            CheckTouchType();
            _draggableItem.OnDragEnd();
        }
    }

    private void ProcessInput(Vector3 screenPosition)
    {
        _draggableItem = null;
        var isOverUI = EventSystem.current.IsPointerOverGameObject();
        if(isOverUI)
            return;
            
        var worldPos = _mainCamera.ScreenToWorldPoint(screenPosition);
        
        var result = Physics2D.Raycast(worldPos, Vector3.forward,
            inputSettings.RaycastDistance, inputSettings.SupportedLayers);
        
        if(!result)
            return;

        _startPosition = result.transform.position;
        _draggableItem = result.transform.GetComponentInChildren<IDraggableItem>();
            
        if(_draggableItem == null)
            return;

        _draggableItem.OnDrag();
        _dragCoroutine = StartCoroutine(DragItemCoroutine(result.transform));
    }

    private void CheckTouchType()
    {
        var distance = Vector3.Distance(_startPosition, _mainCamera.ScreenToWorldPoint(Input.mousePosition));
        if (distance <= 20 && _draggableItem != null)
        {
            _draggableItem.OnClick();
        }
    }

    private IEnumerator DragItemCoroutine(Transform dragableItem)
    {
        while (!Input.GetMouseButtonUp(0))
        {
            Vector2 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            dragableItem.position = newPosition;
            yield return new WaitForSeconds(0.01f);
        }
    } 
}
