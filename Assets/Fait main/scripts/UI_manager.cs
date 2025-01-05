using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton pour acc�der � UIManager globalement

    [Header("UI Elements")]
    public TextMeshProUGUI piecesText; // R�f�rence au texte affichant les pi�ces
    public GameObject keyIcon;   // R�f�rence � l'ic�ne de la cl�
    public GameObject BombIcon;   // R�f�rence � l'ic�ne de la bombe

    private int pieces = 0; // Compteur de pi�ces
    private bool hasKey = false; // Bool�en pour savoir si le joueur a la cl�
    private bool hasBomb = false; // Bool�en pour savoir si le joueur a la bombe

    private void Awake()
    {
        // Assurer qu'il n'y a qu'une seule instance de UIManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Supprimer l'objet si une instance existe d�j�
        }
    }

    private void Start()
    {
        UpdatePiecesUI();
        UpdateKeysUI();
        UpdateBombUI();
    }

    /// <summary>
    /// M�thode pour ajouter une pi�ce.
    /// </summary>
    public void CollectPiece()
    {
        pieces++;
        UpdatePiecesUI();
        Debug.Log($"Pi�ce collect�e ! Total : {pieces}");
    }

    /// <summary>
    /// M�thode pour r�cup�rer la cl�.
    /// </summary>
    public void CollectKey()
    {
        hasKey = true;
        UpdateKeysUI();
        Debug.Log($"Cl� collect�e !");
    }

    /// <summary>
    /// V�rifie si le joueur poss�de la cl�.
    /// </summary>
    public bool HasKey()
    {
        return hasKey;
    }


    /// <summary>
    /// M�thode pour r�cup�rer la Bombe.
    /// </summary>
    public void CollectBomb()
    {
        hasBomb = true;
        UpdateBombUI();
        Debug.Log($"Bombe collect�e !");
    }

    /// <summary>
    /// V�rifie si le joueur poss�de la Bombe.
    /// </summary>
    public bool HasBomb()
    {
        return hasBomb;
    }

    /// <summary>
    /// Met � jour le texte affichant les pi�ces.
    /// </summary>
    private void UpdatePiecesUI()
    {
        if (piecesText != null)
        {
            piecesText.text = $"{pieces}";
        }
        else
        {
            Debug.LogWarning("R�f�rence � 'piecesText' manquante dans UIManager !");
        }
    }

    /// <summary>
    /// Met � jour l'affichage de l'ic�ne de la cl�.
    /// </summary>
    private void UpdateKeysUI()
    {
        if (keyIcon != null)
        {
            keyIcon.SetActive(hasKey); // Affiche ou cache l'ic�ne de la cl�
        }
        else
        {
            Debug.LogWarning("R�f�rence � 'keyIcon' manquante dans UIManager !");
        }
    }

    /// <summary>
    /// Met � jour l'affichage de l'ic�ne de la cl�.
    /// </summary>
    private void UpdateBombUI()
    {
        if (BombIcon != null)
        {
            BombIcon.SetActive(hasBomb); // Affiche ou cache l'ic�ne de la bombe
        }
        else
        {
            Debug.LogWarning("R�f�rence � 'BombIcon' manquante dans UIManager !");
        }
    }

}
