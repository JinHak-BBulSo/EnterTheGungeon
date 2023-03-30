using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMap_DragControll : MonoBehaviour, IDragHandler, IScrollHandler, IPointerClickHandler
{
    public Canvas uiCanvas;

    private RectTransform rectTransform = default;
    private float zoomSpeed = 0.1f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData_)
    {
        Vector2 mouseDelta = eventData_.delta / uiCanvas.scaleFactor;
        Vector3 newPosition = rectTransform.localPosition + new Vector3(mouseDelta.x, mouseDelta.y);

        //float xMin = -130f;
        //float xMax = 200f;
        //float yMin = -80f;
        //float yMax = 0f;

        float xMax = uiCanvas.GetComponent<RectTransform>().rect.width / 2 - rectTransform.rect.width / 2;
        float yMax = uiCanvas.GetComponent<RectTransform>().rect.height / 2 - rectTransform.rect.height / 2;
        //newPosition.x = Mathf.Clamp(newPosition.x, -xMax, xMax);
        //newPosition.y = Mathf.Clamp(newPosition.y, -yMax, yMax);

        rectTransform.localPosition = newPosition;
    }

    public void OnScroll(PointerEventData eventData_)
    {
        float minScale = 0.5f;
        float maxScale = 5f;

        Vector3 newScale = rectTransform.localScale + new Vector3(eventData_.scrollDelta.y * zoomSpeed, eventData_.scrollDelta.y * zoomSpeed, 0f);

        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);

        rectTransform.localScale = newScale;
    }

    public void OnPointerClick(PointerEventData eventData_)
    {
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData_.position, eventData_.pressEventCamera, out localPoint);

        //Collider2D[] colliders = Physics2D.OverlapPointAll(localPoint);
        //GameObject player = GameObject.FindWithTag("Player");

        //GameObject clickedObject = eventData_.pointerCurrentRaycast.gameObject;

        //Debug.Log("클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭클릭");

        //Vector3 targetPos = new Vector3(localPoint.x, localPoint.y, 0f);
        //player.transform.position = targetPos;
    }
}
