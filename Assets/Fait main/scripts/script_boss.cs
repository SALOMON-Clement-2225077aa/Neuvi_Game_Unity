using UnityEngine;

public class DiagonalBounce : MonoBehaviour
{
    [Header("Paramètres de déplacement")]
    public float speed = 7.0f; // Vitesse de déplacement

    [Header("Paramètres de rotation")]
    public float rotationSpeed = 90.0f; // Vitesse de rotation en degrés par seconde

    [Header("Limites de déplacement")]
    public float minX = -128f;
    public float maxX = -110f;
    public float minZ = 60f;
    public float maxZ = 77f;

    private Vector3 direction; // Direction actuelle

    void Start()
    {
        // Initialiser la direction en diagonale (Haut-Droite)
        direction = new Vector3(1, 0, 1).normalized;
    }

    void Update()
    {
        Déplacer();
        Tourner();
        VérifierLimites();
        ConserverDansLimites();
    }

    /// <summary>
    /// Déplace l'objet dans la direction actuelle.
    /// </summary>
    void Déplacer()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    /// <summary>
    /// Fait tourner l'objet autour de l'axe Y.
    /// </summary>
    void Tourner()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Inverse la direction si les limites sont atteintes.
    /// </summary>
    void VérifierLimites()
    {
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            direction.x *= -1; // Inverser la direction en X
        }

        if (transform.position.z >= maxZ || transform.position.z <= minZ)
        {
            direction.z *= -1; // Inverser la direction en Z
        }
    }

    /// <summary>
    /// Maintient l'objet dans les limites définies.
    /// </summary>
    void ConserverDansLimites()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );
    }

    /// <summary>
    /// Dessine les limites dans l'éditeur Unity.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3((minX + maxX) / 2, transform.position.y, (minZ + maxZ) / 2),
            new Vector3(maxX - minX, 1, maxZ - minZ)
        );
    }
}   