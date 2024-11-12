using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    // Variables
    public float speed = 5f;         // Vitesse de déplacement
    public float jumpForce = 5f;     // Force du saut
    private bool isGrounded = true;  // Vérifie si le joueur est au sol

    private Rigidbody2D rb;

    void Start()
    {
        // Initialisation du Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Récupération des entrées de l'utilisateur
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Mouvement horizontal
        Vector2 movement = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        // Saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Détecte si le joueur touche le sol pour permettre le saut
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
