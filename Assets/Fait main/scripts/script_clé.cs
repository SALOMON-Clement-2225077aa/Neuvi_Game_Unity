using UnityEngine;

public class Key : MonoBehaviour
{

    private Vector3 startPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mise � jour du compteur global via UIManager
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.CollectKey();
            }

            // D�truit la cl� apr�s collecte
            Destroy(gameObject);
        }
    }
}
