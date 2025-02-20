using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isDragging = false;
    public float rotationSpeed = 0.2f; // Ajuste la sensibilité de rotation

    void Update()
    {
        if (Input.touchCount == 1) // Vérifie s'il y a un seul doigt sur l'écran
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                touchEndPos = touch.position;
                Vector2 delta = touchEndPos - touchStartPos;

                float rotationX = -delta.y * rotationSpeed; // Rotation haut/bas
                float rotationY = delta.x * rotationSpeed; // Rotation gauche/droite

                transform.Rotate(Vector3.up, rotationY, Space.World);  // Rotation horizontale (autour de Y global)
                transform.Rotate(Vector3.right, rotationX, Space.Self); // Rotation verticale (autour de X local)

                touchStartPos = touchEndPos; // Mise à jour pour un mouvement fluide
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}
