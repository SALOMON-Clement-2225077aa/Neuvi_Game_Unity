using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Destination")]
    public Transform teleportDestination; // Point d'arrivée de la téléportation

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant en collision est le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Début de la téléportation");

            // Vérifie si la destination de téléportation est assignée
            if (teleportDestination != null)
            {
                // Désactive temporairement la gravité du Rigidbody du joueur
                Rigidbody rb = other.GetComponent<Rigidbody>();
                bool wasKinematic = false;

                if (rb != null)
                {
                    wasKinematic = rb.isKinematic;
                    rb.isKinematic = true; // Désactive la physique pendant la téléportation
                }

                // Téléporte le joueur et ses enfants
                TeleportPlayerAndChildren(other.transform);

                // Réactive la gravité (si nécessaire)
                if (rb != null)
                {
                    rb.isKinematic = wasKinematic; // Rétablit l'état précédent du Rigidbody
                }

                Debug.Log("Joueur téléporté !");
            }
            else
            {
                Debug.LogWarning("Destination de téléportation non définie!");
            }
        }
        else
        {
            Debug.Log("Objet autre que le joueur est entré dans le trigger");
        }
    }

    // Fonction pour téléporter le joueur et ses enfants (comme la caméra)
    private void TeleportPlayerAndChildren(Transform playerTransform)
    {
        // Sauvegarde la position initiale du joueur et des enfants
        Vector3 originalPosition = playerTransform.position;

        // Téléporte le joueur (le corps principal)
        playerTransform.position = teleportDestination.position;

        // Maintenant, on s'assure que la position de chaque enfant est correctement mise à jour
        foreach (Transform child in playerTransform)
        {
            // Sauvegarde la position locale de l'enfant
            Vector3 localPosition = child.localPosition;

            // Téléporte l'enfant (en gardant sa position locale intacte)
            child.position = teleportDestination.position + (child.position - playerTransform.position);
        }
    }
}
