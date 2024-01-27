using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Config")]
    public float Velocidad;
    public float fuerzaSalto;
    public LayerMask layerGround;

    private Rigidbody2D rb;
    private bool puedoSaltar;
    public Transform tf;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float xSpeed = horizontal * Velocidad;

        Vector2 direction = new Vector2(xSpeed, rb.velocity.y);
        rb.velocity = direction;
    }

    private void Update()
    {
        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            print("Salto y espacio");

            Vector2 directionForce = Vector2.up * fuerzaSalto;
            rb.AddForce(directionForce, ForceMode2D.Impulse);
        }

        //Puedo saltar
        puedoSaltar = Physics2D.Raycast(tf.position, Vector2.down, 25f, layerGround);

        if(puedoSaltar )
        {
            print("Puedo saltar");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object  
        Gizmos.color = Color.red;
        Vector3 direction = tf.TransformDirection(Vector3.down) * 25;
        Gizmos.DrawRay(tf.position, direction);
    }
}
