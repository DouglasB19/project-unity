using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public float speed;

    private float attackTime = .25f;
    private float attackCounter = .25f;
    private bool isAttacking;

    BoxCollider2D interactionCollider;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        interactionCollider = transform.GetChild(4).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationsAndMove();
    }

    void UpdateAnimationsAndMove()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        if(movement != Vector3.zero)
        {
            anim.SetFloat("HorizontalIdle", movement.x);
            anim.SetFloat("VerticalIdle", movement.y);
        }

        Ataque();
    }

    void Ataque()
    {
        if(isAttacking)
        {
            movement = Vector3.zero;
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                anim.SetBool("Attacking", false);
                isAttacking = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.T) && isAttacking == false)
        {
            attackCounter = attackTime;
            anim.SetBool("Attacking", true);
            isAttacking = true;
        }

        if(movement != Vector3.zero)
        {
            interactionCollider.offset = new Vector2(movement.x/2, movement.y/2);
        }
        
        MoveCharacter();
    }

    void MoveCharacter()
    {
        transform.position = transform.position + movement.normalized * speed * Time.deltaTime;
    } 
}
