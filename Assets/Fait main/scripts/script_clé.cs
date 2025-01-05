using UnityEngine;

public class Key : MonoBehaviour
{

    private Vector3 startPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mise à jour du compteur global via UIManager
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.CollectKey();
            }

            // Détruit la clé après collecte
            Destroy(gameObject);
        }
    }
}
