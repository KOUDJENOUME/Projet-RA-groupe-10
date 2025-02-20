using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    private float zoomSpeed = 0.1f;
    private float minScale = 0.1f;
    private float maxScale = 3f;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                if (initialDistance == 0)
                {
                    initialDistance = currentDistance;
                    initialScale = transform.localScale;
                }
                else
                {
                    float factor = currentDistance / initialDistance;
                    Vector3 newScale = initialScale * factor;
                    newScale = Vector3.Max(Vector3.one * minScale, newScale);
                    newScale = Vector3.Min(Vector3.one * maxScale, newScale);
                    transform.localScale = newScale;
                }
            }
        }
        else
        {
            initialDistance = 0;
        }
    }
}
