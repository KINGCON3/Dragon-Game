using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OldMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;

    private Vector3 movement;
    private Rigidbody rb;
    //private Animator animator;

    //public bool hasWand = false;
    //public bool hasWand;
    //private int direction;

    //bool canMove = true;
    //bool isAttacking = false;

    //public Transform up;
    //public Transform down;
    //public Transform left;
    //public Transform right;

    //public GameObject projPrefab;
    //public float projSpeed = 10;

    //public GameObject RightHitBox;
    //public GameObject LeftHitBox;

    //Collider2D swordColliderRight;
    //private Collider2D swordColliderLeft;

    //public AudioSource wand;
    //private AudioSource audioSource;

    private void Awake()
    {
        //if (playerprefs.getstring("haswand").equals("1"))
        //{
        //    haswand = true;
        //}
        //else
        //{
        //    haswand = false;
        //}
        //audiosource = getcomponent<audiosource>();
        rb = GetComponent<Rigidbody>();
        //animator = getcomponent<animator>();
        //rb.constraints = Rigidbodyconstraints.none;
        //rb.constraints = Rigidbodyconstraints.freezerotation;
        //swordcolliderright = righthitbox.getcomponent<collider2d>();
        //swordcolliderleft = lefthitbox.getcomponent<collider2d>();
    }

    private void OnMove(InputValue value)
    {
        Debug.Log("yes");
        movement = value.Get<Vector3>();

        //if (movement.x != 0 || movement.y != 0)
        //{

            //if (movement.x > 0)
            //{
            //    gameObject.BroadcastMessage("SetDirection", 1);
            //    //direction = 1;
            //}
            //else if (movement.x < 0)
            //{
            //    gameObject.BroadcastMessage("SetDirection", 2);
            //    //direction = 2;
            //}
            //else if (movement.y > 0)
            //{
            //    gameObject.BroadcastMessage("SetDirection", 3);
            //    //direction = 3;
            //}
            //else if (movement.y < 0)
            //{
            //    gameObject.BroadcastMessage("SetDirection", 4);
            //    //direction = 4;
            //}

            //animator.SetFloat("X", movement.x);
            //animator.SetFloat("Y", movement.y);

            //animator.SetBool("IsWalking", true);
    //    }
    //    else
    //    {
    //        //animator.SetBool("IsWalking", false);
    //    }
    }

    private void FixedUpdate()
    {
        Debug.Log("esf");
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x != 0 || movement.y != 0)
        {
            rb.velocity = movement * speed;
        }
        //if (canMove)
        //{
        rb.AddForce(movement * speed);
        //}
    }

    //public void OnAttack()
    //{
    //    if (!hasWand)
    //    {
    //        if (!animator.GetBool("isAttacking"))
    //        {
    //            animator.SetTrigger("swordAttack");
    //            animator.SetBool("isAttacking", true);
    //        }
    //    }
    //    else
    //    {
    //        if (!animator.GetBool("isAttacking"))
    //        {
    //            animator.SetTrigger("wandAttack");
    //            animator.SetBool("isAttacking", true);
    //        }
    //    }
    //}
    //void canAttack()
    //{
    //    animator.SetBool("isAttacking", false);
    //}

    //void LockMovement()
    //{
    //    canMove = false;
    //}

    //void UnlockMovement()
    //{
    //    canMove = true;
    //}

    //void ShootProj()
    //{
    //    PlaySound(wand);
    //    //1 = right
    //    //2 = left
    //    //3 = up
    //    //4 = down

    //    switch (direction)
    //    {
    //        case 1:
    //            // Right
    //            var projright = Instantiate(projPrefab, right.position, right.rotation);
    //            projright.GetComponent<Rigidbody2D>().velocity = right.right * projSpeed;
    //            break;
    //        case 2:
    //            // Left
    //            var projleft = Instantiate(projPrefab, left.position, left.rotation);
    //            projleft.GetComponent<Rigidbody2D>().velocity = -left.right * projSpeed;
    //            break;
    //        case 3:
    //            // Up
    //            var projup = Instantiate(projPrefab, up.position, up.rotation);
    //            projup.GetComponent<Rigidbody2D>().velocity = up.up * projSpeed;
    //            break;
    //        case 4:
    //            // Down
    //            var projdown = Instantiate(projPrefab, down.position, down.rotation);
    //            projdown.GetComponent<Rigidbody2D>().velocity = -down.up * projSpeed;
    //            break;
    //    }
    //}

    //void PlaySound(AudioSource sound)
    //{
    //    if (sound != null && audioSource != null)
    //    {
    //        //swing1.Stop();
    //        //swing2.Stop(); 
    //        sound.Play();
    //        //StartCoroutine(StopAudioAfterDelay(0.40f));
    //    }
    //    else
    //    {
    //        Debug.LogError("AudioClip or AudioSource is null.");
    //    }
    //}
}
