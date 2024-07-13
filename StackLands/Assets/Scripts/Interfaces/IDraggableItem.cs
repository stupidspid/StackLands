using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggableItem
{
    public void OnClick();
    public void OnDrag();
    public void OnDragEnd();
}
