using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //velocidad de movimiento
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject explosionPrefab; 
    public float lifetime = 2f; 
    //limites
    private Vector2 minBounds; 
    private Vector2 maxBounds;
    private float halfWidth;   
    private float halfHeight;
    public GameObject gameOverPantalla;  

    void Start()
    {
   
        rb = GetComponent<Rigidbody2D>();
        Camera cam = Camera.main;
        minBounds = cam.ScreenToWorldPoint(Vector3.zero); // Esquina inferior izquierda
        maxBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)); // Esquina superior derecha

        //calcula la mitad del tamaño del sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        //captura la entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // A y D o Flechas Izquierda y Derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W y S o Flechas Arriba y Abajo
    }

    void FixedUpdate()
    {
        //calcula la nueva posicion
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        //aplica la nueva posicion al Rigidbody2D
        rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BalaJefe") ||
            collision.gameObject.CompareTag("JefeFinal") ||
            collision.gameObject.CompareTag("Enemigo") ||
            collision.gameObject.CompareTag("Enemigo2"))
        {
            SpawnExplosion(transform.position);
            Destroy(gameObject); // Destruye la nave

            ShowGameOverPanel();
        }
    }

    void SpawnExplosion(Vector3 position)
    {
        // Instanciar la explosión en la posición dada
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion, lifetime); // Destruye el objeto explosión después de un tiempo
    }

    
    // Mostrar el panel de Game Over
    void ShowGameOverPanel()
    {
        if (gameOverPantalla != null)
        {
            gameOverPantalla.SetActive(true);
        }
    }
}
