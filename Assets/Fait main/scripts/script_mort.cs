using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{
    [Header("Respawn Point")]
    public Transform teleportDestination; // Le point de respawn

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant en collision est le joueur
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
}
