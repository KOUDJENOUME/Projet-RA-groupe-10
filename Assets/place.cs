using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Place : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private PlacementObject[] placedObjects;
    [SerializeField] private Color activeColor = Color.red;
    [SerializeField] private Color inactiveColor = Color.gray;
    [SerializeField] private Button dismissButton;
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private AudioSource audioSource; // Ajout d'un AudioSource

    private Vector2 touchPosition = default;
    [SerializeField] private bool displayOverlay = false;
    private bool isImageTracked = false;

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
    }

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // S'assurer que l'audio ne joue pas au démarrage
        }

        foreach (PlacementObject obj in placedObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }


    private void Dismiss()
    {
        welcomePanel.SetActive(false);

        // Jouer le son après la fermeture du panneau
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Aucun AudioSource assigné !");
        }
    }

    void Update()
    {
        if (welcomePanel.activeSelf || !isImageTracked)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (placementObject != null)
                    {
                        ChangeSelectedObject(placementObject);
                    }
                }
            }
        }
    }

    void ChangeSelectedObject(PlacementObject selected)
    {
        foreach (PlacementObject current in placedObjects)
        {
            if (current == null)
            {
                Debug.LogError("L'objet PlacementObject est null.");
                continue;
            }

            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.LogError("Le MeshRenderer est introuvable sur l'objet " + current.name);
                continue;
            }

            if (selected != current)
            {
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                current.Selected = true;
                meshRenderer.material.color = activeColor;
            }

            if (displayOverlay)
                current.ToggleUI();;
        }
    }

    // Fonction appelée lorsqu'une image est détectée
    private void OnImageTracked(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Image détectée : " + trackedImage.referenceImage.name);
            isImageTracked = true;

            // Activer les objets une fois que l'image est détectée
            foreach (PlacementObject obj in placedObjects)
            {
                obj.gameObject.SetActive(true);
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log("Image perdue : " + trackedImage.referenceImage.name);
            isImageTracked = false;

            // Désactiver les objets si l'image n'est plus détectée
            foreach (PlacementObject obj in placedObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        if (imageManager != null)
            imageManager.trackedImagesChanged += OnImageTracked;
    }

    void OnDisable()
    {
        if (imageManager != null)
            imageManager.trackedImagesChanged -= OnImageTracked;
    }
}
