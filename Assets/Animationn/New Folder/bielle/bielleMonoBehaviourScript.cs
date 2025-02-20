using UnityEngine;
using UnityEngine.InputSystem;

public class bielleMonoBehaviourScript : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput; 

    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            anim.enabled = !anim.enabled;
        }
    }
}
