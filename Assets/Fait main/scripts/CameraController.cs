using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Le joueur que la caméra suit
    public Vector3 offset = new Vector3(0, 5, -10); // Décalage entre le joueur et la caméra
    public float sensitivity = 5.0f; // Sensibilité de la souris
    public float rotationSpeed = 10.0f; // Vitesse de rotation
    public float minY = -20.0f; // Limite inférieure pour la rotation verticale
    public float maxY = 60.0f; // Limite supérieure pour la rotation verticale

    private float currentRotationX = 0.0f; // Rotation actuelle en X (verticale)
    private float currentRotationY = 0.0f; // Rotation actuelle en Y (horizontale)

    void LateUpdate()
    {
        // Vérifiez si le clic droit est maintenu
        if (Input.GetMouseButton(1))
        {
            // Récupérer les mouvements de la souris
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Mettre à jour les rotations
            currentRotationY += mouseX; // Rotation autour de l'axe Y (horizontal)
            currentRotationX -= mouseY; // Rotation autour de l'axe X (vertical)

            // Limiter la rotation verticale
            currentRotationX = Mathf.Clamp(currentRotationX, minY, maxY);
        }

        // Appliquer la rotation à la caméra
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        transform.position = player.position + rotation * offset; // Appliquer la rotation au décalage
        transform.LookAt(player); // Regarder toujours le joueur
    }
}
