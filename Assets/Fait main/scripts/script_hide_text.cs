using UnityEngine;

public class HideTextAtDistance : MonoBehaviour
{
    public Transform player; // Reference to the player's transform (you can assign this in the inspector)
    public float maxDistance = 3f; // Distance at which the text disappears
    private Renderer textRenderer;

    void Start()
    {
        // Get the renderer of the text object
        textRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Check the distance between the player and the text object
        float distance = Vector3.Distance(player.position, transform.position);

        // Disable the text if the player is further than maxDistance
        if (distance > maxDistance)
        {
            textRenderer.enabled = false;
        }
        else
        {
            textRenderer.enabled = true;
        }
    }
}
