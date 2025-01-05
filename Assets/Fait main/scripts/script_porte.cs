using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Param�tres de la Porte")]
    public Transform doorTransform; // R�f�rence au Transform de la porte
    public Transform lockTransform; // R�f�rence au Transform du cadenas

    public float doorOpenHeight = 3.0f; // Hauteur d'ouverture de la porte
    public float lockFlyHeight = 2.0f; // Hauteur de vol du cadenas
    public float lockDescendHeight = 5.0f; // Nouvelle hauteur de descente du cadenas
    public float animationDuration = 2.0f; // Dur�e de l'animation
    public float doorRotationSpeed = 90.0f; // Vitesse de rotation de la porte (en degr�s par seconde)

    private bool isUnlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            if (UIManager.instance.HasKey()) // V�rifie si le joueur a la cl�
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("Vous avez besoin d'une cl� pour ouvrir cette porte !");
            }
        }
    }

    private void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;

            // Lancer l'animation du cadenas et de la porte
            StartCoroutine(AnimateLock());
        }
    }

    private IEnumerator AnimateLock()
    {
        Debug.Log("Animation du cadenas commenc�e !");
        
        Vector3 initialLockPosition = lockTransform.position;
        Vector3 peakLockPosition = initialLockPosition + Vector3.up * lockFlyHeight;

        float elapsedTime = 0f;

        // Phase 1 : Le cadenas monte et tourne
        while (elapsedTime < animationDuration / 2)
        {
            float t = elapsedTime / (animationDuration / 2);
            lockTransform.position = Vector3.Lerp(initialLockPosition, peakLockPosition, t);
            lockTransform.Rotate(Vector3.right * 360 * Time.deltaTime); // Rotation sur X
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Phase 2 : Le cadenas redescend bien plus bas
        elapsedTime = 0f;
        Vector3 finalLockPosition = initialLockPosition + Vector3.down * lockDescendHeight; // Position finale plus basse
        while (elapsedTime < animationDuration / 2)
        {
            float t = elapsedTime / (animationDuration / 2);
            lockTransform.position = Vector3.Lerp(peakLockPosition, finalLockPosition, t);
            lockTransform.Rotate(Vector3.right * 360 * Time.deltaTime); // Rotation sur X
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // D�truire le cadenas
        Destroy(lockTransform.gameObject);
        Debug.Log("Cadenas d�truit apr�s animation !");

        // Lancer l'animation de la porte
        StartCoroutine(AnimateDoor());
    }

    private IEnumerator AnimateDoor()
    {
        doorTransform.eulerAngles = new Vector3(0, 90, 0);

        Debug.Log("Porte ouverte !");
        yield return null;
    }


}