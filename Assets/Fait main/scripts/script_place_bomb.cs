using UnityEngine;

public class PlayerBombControl : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab de la bombe
    public Transform player;      // Transform du joueur
    public GameObject explosionPrefab; // Prefab de l'explosion
    private bool hasBomb = false; // Vérifie si le joueur peut poser une bombe

    private Vector3 bombPosition; // Stocke la position de la bombe

    void Update()
    {
        // Si le joueur a une bombe et appuie sur 'E'
        if (hasBomb && Input.GetKeyDown(KeyCode.E))
        {
            PlaceBomb();
        }
    }

    public void CollectBomb()
    {
        // Permet au joueur de collecter une bombe
        hasBomb = true;
    }

    void PlaceBomb()
    {
        if (bombPrefab != null)
        {
            // Place la bombe à gauche du joueur
            bombPosition = player.position - player.right * 1.5f;

            // Ajuste pour poser la bombe sur le sol (si nécessaire)
            RaycastHit hit;
            if (Physics.Raycast(bombPosition + Vector3.up * 1f, Vector3.down, out hit, 2f))
            {
                bombPosition.y = hit.point.y;
            }

            // Instancie la bombe
            GameObject placedBomb = Instantiate(bombPrefab, bombPosition, Quaternion.identity);

            // Ignore la collision entre le joueur et la bombe
            Physics.IgnoreCollision(placedBomb.GetComponent<Collider>(), player.GetComponent<Collider>());

            // Désactive la possibilité de poser une nouvelle bombe
            hasBomb = false;

            // Détruit la bombe après 2 secondes
            Destroy(placedBomb, 2f);

            // Déclenche l'explosion après 2 secondes
            Invoke(nameof(CreateExplosion), 2f);
        }
    }

    void CreateExplosion()
    {
        if (explosionPrefab != null)
        {
            // Instancie l'explosion à l'emplacement de la bombe
            GameObject explosion = Instantiate(explosionPrefab, bombPosition, Quaternion.identity);

            // Détruit les objets destructibles dans le rayon de l'explosion
            Collider[] hitObjects = Physics.OverlapSphere(bombPosition, 2f); // Rayon de l'explosion
            foreach (Collider hit in hitObjects)
            {
                if (hit.CompareTag("Destructible")) // Vérifie si l'objet est destructible
                {
                    Destroy(hit.gameObject); // Détruit l'objet
                    Debug.Log($"{hit.name} a été détruit !");
                }
            }

            // Détruit l'explosion après 0.5 seconde
            Destroy(explosion, 0.5f);

            // Permet de récupérer une nouvelle bombe après l'explosion
            hasBomb = true;
        }
    }
}
