using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rota : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchStartPos, touchEndPos;
    private float rotationSpeed = 0.2f; // Ajuste cette valeur pour modifier la vitesse de rotation

    void Update()
    {
        if (Input.touchCount > 0) // V�rifie s'il y a au moins un doigt sur l'�cran
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) // Si le doigt bouge sur l'�cran
            {
                float swipeAmount = touch.deltaPosition.x * rotationSpeed; // Rotation bas�e sur le mouvement horizontal
                transform.Rotate(Vector3.up, -swipeAmount); // Applique la rotation autour de l'axe Y
            }
        }
    }
}