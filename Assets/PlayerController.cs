using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    private Animator animator;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //x,y,z
        Vector3 currentVector = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.y * moveSpeed);


        Quaternion newRotation = rb.rotation; // Start with the current rotation

        if (currentVector.x != 0 || currentVector.z != 0)
        {
            if (currentVector.x > 0 && currentVector.z > 0)
            {
                newRotation = Quaternion.Euler(0, 45, 0);  // Face top-right
            }
            else if (currentVector.x > 0 && currentVector.z < 0)
            {
                newRotation = Quaternion.Euler(0, 135, 0); // Face bottom-right
            }
            else if (currentVector.x < 0 && currentVector.z > 0)
            {
                newRotation = Quaternion.Euler(0, 315, 0); // Face top-left
            }
            else if (currentVector.x < 0 && currentVector.z < 0)
            {
                newRotation = Quaternion.Euler(0, 225, 0); // Face bottom-left
            }
            else if (currentVector.x > 0)
            {
                newRotation = Quaternion.Euler(0, 90, 0);  // Face right
            }
            else if (currentVector.x < 0)
            {
                newRotation = Quaternion.Euler(0, 270, 0); // Face left
            }
            else if (currentVector.z > 0)
            {
                newRotation = Quaternion.Euler(0, 0, 0);   // Face forward
            }
            else if (currentVector.z < 0)
            {
                newRotation = Quaternion.Euler(0, 180, 0); // Face backward
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        rb.rotation = newRotation;
        rb.velocity = currentVector;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
    }
}
