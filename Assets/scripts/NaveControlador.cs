using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector2 minBounds; // Límites inferiores de la pantalla
    private Vector2 maxBounds; // Límites superiores de la pantalla
    private float halfWidth;   // Mitad del ancho del sprite
    private float halfHeight;  // Mitad del alto del sprite

    void Start()
    {
        // Obtiene el Rigidbody2D al iniciar
        rb = GetComponent<Rigidbody2D>();

        // Calcula los límites de la pantalla en coordenadas del mundo
        Camera cam = Camera.main;
        minBounds = cam.ScreenToWorldPoint(Vector3.zero); // Esquina inferior izquierda
        maxBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)); // Esquina superior derecha

        // Calcula la mitad del tamaño del sprite (para evitar que salga parcialmente de la pantalla)
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        // Captura la entrada del jugador en los ejes horizontales y verticales
        movement.x = Input.GetAxisRaw("Horizontal"); // A y D o Flechas Izquierda y Derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W y S o Flechas Arriba y Abajo
    }

    void FixedUpdate()
    {
        // Calcula la nueva posición basada en el movimiento
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Restringe la posición para que esté dentro de los límites de la pantalla
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        // Aplica la nueva posición al Rigidbody2D
        rb.MovePosition(newPosition);
    }
}
