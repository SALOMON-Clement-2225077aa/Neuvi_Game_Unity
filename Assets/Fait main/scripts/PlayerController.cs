using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Vitesse de déplacement
    public float rotationSpeed = 10.0f; // Vitesse de rotation
    public Transform cameraTransform; // Référence à la caméra
    public float jumpForce = 5.0f; // Force du saut
    private Rigidbody rb; // Référence au Rigidbody
    private bool isGrounded = true; // Vérifie si le joueur est au sol

    private Vector3 moveDirection; // Direction de déplacement

    [Header("Particles")] 
    public ParticleSystem footstepParticles; // Particules de marche
    public ParticleSystem jumpParticles;     // Particules de saut

    [Header("Audio")] 
    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip footstepSound; // Son des pas (boucle)
    public AudioClip jumpSound;     // Son du saut

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Configurer l'AudioSource pour que le son des pas soit en boucle
        if (audioSource != null && footstepSound != null)
        {
            audioSource.clip = footstepSound;
            audioSource.loop = true; // Activer la boucle pour le son des pas
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle + 90, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // Jouer les particules de marche
            if (isGrounded && footstepParticles != null && !footstepParticles.isPlaying)
            {
                footstepParticles.Play();
            }

            // Jouer le son des pas en boucle
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play(); // Jouer le son des pas en boucle
            }

            animator.SetBool("isWalking", true);
        }
        else
        {
            moveDirection = Vector3.zero;

            // Arrêter les particules de marche
            if (footstepParticles != null && footstepParticles.isPlaying)
            {
                footstepParticles.Stop();
            }

            // Arrêter le son des pas lorsque le joueur s'arrête de marcher
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop(); // Stopper la lecture en boucle des pas
            }

            animator.SetBool("isWalking", false);
        }

        rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

        // Gérer le saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            // Jouer le son du saut
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }

            // Jouer les particules de saut
            if (jumpParticles != null)
            {
                jumpParticles.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // Arrêter les particules de saut quand on touche le sol
            if (jumpParticles != null && jumpParticles.isPlaying)
            {
                jumpParticles.Stop();
            }
        }
    }
}
