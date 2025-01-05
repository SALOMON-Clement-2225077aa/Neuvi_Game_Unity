using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject crown; // Référence à la couronne que le joueur doit toucher
    public GameObject specialObject; // L'objet dont on vérifie l'existence
    public string goodEndingScene = "GoodEnding"; // Nom de la scène pour la bonne fin
    public string badEndingScene = "BadEnding"; // Nom de la scène pour la mauvaise fin

    private bool hasWon = false; // Pour éviter de déclencher plusieurs fois la fin

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si c'est le joueur qui touche la couronne
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true; // Marque la victoire pour éviter les répétitions
            HandleWin();
        }
    }

    private void HandleWin()
    {
        // Vérifie si l'objet spécial existe encore
        if (specialObject == null)
        {
            // Bonne fin
            Debug.Log("Bonne fin : l'objet spécial existe encore !");
            SceneManager.LoadScene(goodEndingScene);
        }
        else
        {
            // Mauvaise fin
            Debug.Log("Mauvaise fin : l'objet spécial a disparu !");
            SceneManager.LoadScene(badEndingScene);
        }
    }
}
