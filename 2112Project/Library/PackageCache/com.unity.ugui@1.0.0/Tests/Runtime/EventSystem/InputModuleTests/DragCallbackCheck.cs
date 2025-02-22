using UnityEngine;
using UnityEngine.EventSystems;

<<<<<<< HEAD
public class DragCallbackCheck : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
=======
public class DragCallbackCheck : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
{
    private bool loggedOnDrag = false;
    public bool onBeginDragCalled = false;
    public bool onDragCalled = false;
    public bool onEndDragCalled = false;
<<<<<<< HEAD
=======
    public bool onDropCalled = false;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDragCalled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (loggedOnDrag)
            return;

        loggedOnDrag = true;
        onDragCalled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDragCalled = true;
    }
<<<<<<< HEAD
=======

    public void OnDrop(PointerEventData eventData)
    {
        onDropCalled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Empty to ensure we get the drop if we have a pointer handle as well.
    }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
}
