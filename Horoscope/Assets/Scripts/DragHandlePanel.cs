using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlePanel : Graphic, IDragHandler
{
    private Camera _camera;

    public void Initialize(Camera camera)
    {
        this._camera = camera;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var speed = this._camera.fieldOfView / 60f;
        this._camera.transform.Rotate(Vector3.up, -eventData.delta.x / 3f * speed);
        this._camera.transform.Rotate(Vector3.right, eventData.delta.y / 3f * speed);
    }
}
