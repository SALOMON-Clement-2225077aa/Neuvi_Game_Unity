using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mise � jour du compteur global via UIManager
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.CollectBomb();
            }

            // R�cup�re le script PlayerBombControl et appelle la m�thode CollectBomb()
            PlayerBombControl playerBombControl = other.GetComponent<PlayerBombControl>();
            if (playerBombControl != null)
            {
                playerBombControl.CollectBomb(); // Appelle la m�thode sur l'instance du script
            }

            // D�truit l'objet bombe apr�s la collecte
            Destroy(gameObject);
        }
    }
}
