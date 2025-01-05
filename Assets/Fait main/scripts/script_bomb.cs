using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mise à jour du compteur global via UIManager
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.CollectBomb();
            }

            // Récupère le script PlayerBombControl et appelle la méthode CollectBomb()
            PlayerBombControl playerBombControl = other.GetComponent<PlayerBombControl>();
            if (playerBombControl != null)
            {
                playerBombControl.CollectBomb(); // Appelle la méthode sur l'instance du script
            }

            // Détruit l'objet bombe après la collecte
            Destroy(gameObject);
        }
    }
}
