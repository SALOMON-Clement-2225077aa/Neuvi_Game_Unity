using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Change la scène vers "GameScene"
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Ferme complètement le jeu
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}