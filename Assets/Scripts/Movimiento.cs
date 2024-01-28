using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento : MonoBehaviour
{
    public float Speed = 3f;
    public float horizontal;
        
    public float vertical;
    public float fuerzaSalto = 20f;
    
    public LayerMask layerSuelo;
    
    public bool puedoSaltar;
    
    public Transform tf;
    private Animator animator;
    public bool EstoySaltando;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //if (animator.GetBool("IsGrounded") == true)
        //{
        //}
        transform.Translate(horizontal * Time.deltaTime * Speed, 0, 0);


        animator.SetFloat("VelocidadX", horizontal);
        animator.SetFloat("VelocidadY", vertical);

        if(horizontal >= 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }

    }


    private void Update()
    {
        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            print("Salto y espacio");

            //Vector2 directionForce = Vector2.up * fuerzaSalto;
            //rb.AddForce(directionForce, ForceMode2D.Impulse);

            //animator.SetTrigger("Salto");
            EstoySaltando = true;

            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode2D.Impulse);
        }

     
        //Puedo saltar
        puedoSaltar = Physics2D.Raycast(tf.position, Vector2.down, 3f, layerSuelo);
            
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object  
        Gizmos.color = Color.red;
        Vector3 direction = tf.TransformDirection(Vector3.down) * 3;
        Gizmos.DrawRay(tf.position, direction);
    }

    public void dejarSalto()
    {
        EstoySaltando=false;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            animator.SetBool("IsGrounded", true);
            //animator.SetTrigger("Salto");
            puedoSaltar = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            animator.SetBool("IsGrounded", false);
            puedoSaltar = false;
        }
    }
}
