using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton pour accéder à UIManager globalement

    [Header("UI Elements")]
    public TextMeshProUGUI piecesText; // Référence au texte affichant les pièces
    public GameObject keyIcon;   // Référence à l'icône de la clé
    public GameObject BombIcon;   // Référence à l'icône de la bombe

    private int pieces = 0; // Compteur de pièces
    private bool hasKey = false; // Booléen pour savoir si le joueur a la clé
    private bool hasBomb = false; // Booléen pour savoir si le joueur a la bombe

    private void Awake()
    {
        // Assurer qu'il n'y a qu'une seule instance de UIManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Supprimer l'objet si une instance existe déjà
        }
    }

    private void Start()
    {
        UpdatePiecesUI();
        UpdateKeysUI();
        UpdateBombUI();
    }

    /// <summary>
    /// Méthode pour ajouter une pièce.
    /// </summary>
    public void CollectPiece()
    {
        pieces++;
        UpdatePiecesUI();
        Debug.Log($"Pièce collectée ! Total : {pieces}");
    }

    /// <summary>
    /// Méthode pour récupérer la clé.
    /// </summary>
    public void CollectKey()
    {
        hasKey = true;
        UpdateKeysUI();
        Debug.Log($"Clé collectée !");
    }

    /// <summary>
    /// Vérifie si le joueur possède la clé.
    /// </summary>
    public bool HasKey()
    {
        return hasKey;
    }


    /// <summary>
    /// Méthode pour récupérer la Bombe.
    /// </summary>
    public void CollectBomb()
    {
        hasBomb = true;
        UpdateBombUI();
        Debug.Log($"Bombe collectée !");
    }

    /// <summary>
    /// Vérifie si le joueur possède la Bombe.
    /// </summary>
    public bool HasBomb()
    {
        return hasBomb;
    }

    /// <summary>
    /// Met à jour le texte affichant les pièces.
    /// </summary>
    private void UpdatePiecesUI()
    {
        if (piecesText != null)
        {
            piecesText.text = $"{pieces}";
        }
        else
        {
            Debug.LogWarning("Référence à 'piecesText' manquante dans UIManager !");
        }
    }

    /// <summary>
    /// Met à jour l'affichage de l'icône de la clé.
    /// </summary>
    private void UpdateKeysUI()
    {
        if (keyIcon != null)
        {
            keyIcon.SetActive(hasKey); // Affiche ou cache l'icône de la clé
        }
        else
        {
            Debug.LogWarning("Référence à 'keyIcon' manquante dans UIManager !");
        }
    }

    /// <summary>
    /// Met à jour l'affichage de l'icône de la clé.
    /// </summary>
    private void UpdateBombUI()
    {
        if (BombIcon != null)
        {
            BombIcon.SetActive(hasBomb); // Affiche ou cache l'icône de la bombe
        }
        else
        {
            Debug.LogWarning("Référence à 'BombIcon' manquante dans UIManager !");
        }
    }

}
