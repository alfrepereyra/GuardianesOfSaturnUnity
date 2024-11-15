using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Obtiene el Rigidbody2D al iniciar
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura la entrada del jugador en los ejes horizontales y verticales
        movement.x = Input.GetAxisRaw("Horizontal"); // A y D o Flechas Izquierda y Derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W y S o Flechas Arriba y Abajo
    }

    void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
