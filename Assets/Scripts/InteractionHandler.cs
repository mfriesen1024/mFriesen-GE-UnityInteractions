using System.Collections.Generic;
using UnityEngine;

// TODO:
// 1. Add functionality on interaction.
// 2. Add garbage cleanup for interactables.

public class InteractionHandler : MonoBehaviour
{
    // ???
    [SerializeField] GameObject panel;
    [SerializeField] List<InteractableObject> interactables;

    // Heres some junk that should be in a gamemanager.
    [SerializeField] int score = 0;
    [SerializeField] SpriteRenderer lastInteractedObjRenderer;

    void Start()
    {
        interactables = new List<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddInteractable(InteractableObject interactableObject)
    {
        interactables.Add(interactableObject);
    }

    void OnInteract()
    {
        InteractableObject interactableObject;
        interactableObject = GetNearestInteractable();
    }

    /// <summary>
    /// Gets the nearest interactable object we know of.
    /// </summary>
    /// <returns></returns>
    private InteractableObject GetNearestInteractable()
    {
        InteractableObject interactableObject;
        // Track the index and value of our nearest known interactable.
        int bestIndex = 0;
        float bestDistance = 0;

        for (int i = 0; i < interactables.ToArray().Length; i++)
        {
            InteractableObject interactable = interactables[i];

            // Double check that we're in range.
            float distance = Vector2.Distance(transform.position, interactable.transform.position);
            if (distance < interactable.detectionRange)
            {
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestIndex = i;
                }
            }
        }

        interactableObject = interactables[bestIndex];
        return interactableObject;
    }
}
