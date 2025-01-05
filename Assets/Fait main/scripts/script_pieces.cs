using UnityEngine;

public class Coin : MonoBehaviour
{
    // Animation Pi�ce
    [Header("Animation Settings")]
    public float rotationSpeed = 50f;    // Vitesse de rotation
    public float floatAmplitude = 0.5f; // Amplitude du flottement
    public float floatFrequency = 1f;   // Fr�quence du flottement

    private Vector3 startPosition;

    [Header("Audio")] 
    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip collectSound;     // Son de récolte de la pièce

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Animation : Rotation
        transform.Rotate(new Vector3(0, 0, rotationSpeed));

        // Animation : Flottement vertical
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPosition + new Vector3(0, floatOffset, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le joueur a bien le tag "Player"
        {
            // Mise � jour du compteur global via UIManager
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.CollectPiece();
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("UIManager non trouv� dans la sc�ne !");
            }

            // D�truit la pi�ce apr�s collecte
            Destroy(gameObject);
        }
    }
}
