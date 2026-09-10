
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movimiento
    public float speed = 5;
    private Rigidbody2D rb2D;
    private float move;

    // Salto
    public float jumpForce = 4f;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private Animator animator;

    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb2D = GetComponent<Rigidbody2D>();

        // Obtener el componente Animator del jugador
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Obtener dirección horizontal
        move = Input.GetAxisRaw("Horizontal");

        // 2. Aplicar movimiento horizontal manteniendo la velocidad vertical actual
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        // 3. Voltear la orientación del personaje (solo cuando se mueva)
        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }

        // 4. Salto (debe estar fuera del if de movimiento para saltar estando quieto)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        // Actualizar la velocidad horizontal en el Animator
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Actualizar la velocidad vertical en el Animator
        animator.SetFloat("VerticalSpeed", rb2D.linearVelocity.y);

        // Actualizar el estado de si el jugador está en el suelo
        animator.SetBool("IsGrounded", isGrounded);
    }

    void FixedUpdate()
    {
        // Comprobar si hay un objeto en la capa "groundLayer"
        // dentro del radio en la posición del "groundCheck"
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lógica para recolectar el objeto
        if (collision.transform.CompareTag("Coin"))
        {
            // Destruir la moneda cuando el jugador la recolecta
            Destroy(collision.gameObject);
        }
    }
}

