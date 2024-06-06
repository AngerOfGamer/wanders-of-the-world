using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan pemain
    private Rigidbody2D rb; // Referensi ke komponen Rigidbody2D
    private Animator animator; // Referensi ke komponen Animator

    [Header("Dialogue")]
    [SerializeField] private DialogueUi dialogueUi;
    public DialogueUi DialogueUi => dialogueUi;
    public DialogueInteract Interact { get; set; }

    void Start()
    {
        // Mendapatkan referensi ke komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        // Mendapatkan referensi ke komponen Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dialogueUi.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interact != null)
            {
                Interact.Interact(this);
            }
        }

        // Mendapatkan input horizontal
        float moveInput = Input.GetAxis("Horizontal");

        // Menggerakkan objek ke kanan atau kiri sesuai dengan input horizontal
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Mengubah arah sprite pemain jika bergerak ke kanan
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Menghadap ke kanan
            // Mengatur parameter "isWalking" ke true untuk memulai animasi berjalan
            animator.SetBool("isWalking", true);
        }
        // Mengubah arah sprite pemain jika bergerak ke kiri
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Menghadap ke kiri
            // Mengatur parameter "isWalking" ke true untuk memulai animasi berjalan
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Jika tidak ada input gerakan, mengatur parameter "isWalking" ke false untuk kembali ke animasi idle
            animator.SetBool("isWalking", false);
        }
    }
}
