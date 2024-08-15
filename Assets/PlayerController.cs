using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem.LowLevel;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public Inventory inventory;

    public Rigidbody rb;
    public float moveSpeed = 5f;
    public PlayerInputActions playerControls;
    public GameObject bullet;
    public GameObject currentDragon;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    private Animator animator;
    private Animator dragon;
    private int storedBullets = 5;
    private float counter = 0;
    private float delay;
    private string visibleDragon;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
        animator = GetComponent<Animator>();
    }

    public ItemSlotInfo getFirst()
    {
        //Debug.Log(item.GiveName());
        List<ItemSlotInfo> items = inventory.getItems();
        foreach (ItemSlotInfo i in items)
        {
            if (i.item != null)
            {
                return i;
            }
        }
        return null;
    }

    public override void OnNetworkSpawn()
    {
        //base.OnNetworkSpawn();
        if (!IsOwner)
        {
            //AudioListener aL = this.GetComponentInChildren<AudioListener>();
            transform.FindChild("Main Camera").gameObject.SetActive(false);
            //aL.enabled = false;
            enabled = false;
            Debug.Log("not the owner");
        }
        //base.OnNetworkSpawn();
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
        delay += Time.deltaTime;

        if (storedBullets < 5)
        {
            counter += Time.deltaTime;
            if (counter >= 2)
            {
                storedBullets += 1;
                counter = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && !inventory.inventoryMenu.activeSelf && storedBullets > 0 && delay >= 0.2f)
        {
            Debug.Log("trying to shoot");
            storedBullets--;
            delay = 0f;

            dragon.SetTrigger("Shoot");

            if (IsServer)
            {
                SpawnToNetwork();
            }
            else
            {
                SpawnOnServerRpc();
            }

        }

        visibleDragon = getFirst().item.GiveName();
        Transform dragonsTransform = transform.Find("Dragons");
        Transform current = dragonsTransform.Find(visibleDragon);
        current.gameObject.SetActive(true);
        dragon = current.GetComponent<Animator>();
    }

    private void SpawnToNetwork()
    {
        Vector3 shootDir = (currentDragon.transform.position - rb.transform.position).normalized;
        GameObject bulletTransform = Instantiate(bullet, currentDragon.transform.position, Quaternion.LookRotation(shootDir) * Quaternion.Euler(0, -90, 0));
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
        bulletTransform.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
    private void SpawnOnServerRpc()
    {
        SpawnToNetwork();
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
            dragon.SetBool("isFlying", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            if (dragon.isInitialized && dragon.isActiveAndEnabled) {
            dragon.SetBool("isFlying", false);
            }
        }

        rb.rotation = newRotation;
        rb.velocity = currentVector;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
    }
}
