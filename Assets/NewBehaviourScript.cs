using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TouchDetection : MonoBehaviour
{
    public GameObject textObject; // Référence à l'objet texte UI
    private ARRaycastManager arRaycastManager;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        textObject.SetActive(false); // Cacher le texte au début
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    // Afficher ou cacher le texte
                    textObject.SetActive(!textObject.activeSelf);
                }
            }
        }
    }
}