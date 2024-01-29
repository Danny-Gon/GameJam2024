using UnityEngine;

public class Colision : MonoBehaviour
{
    private Animator animator;
    public bool chocar = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bloque"))
        {
            animator.SetTrigger("crash");
        }
    }
}
