using UnityEngine;
using UnityEngine.InputSystem;


public class couronneMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator anim;
    private PlayerInput playerInput;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            anim.enabled = !anim.enabled;
        }
    }
}
