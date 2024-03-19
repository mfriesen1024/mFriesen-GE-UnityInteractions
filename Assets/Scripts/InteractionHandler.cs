using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO:
// 1. Add functionality on interaction.
// 2. Add garbage cleanup for interactables.

public class InteractionHandler : MonoBehaviour
{
    [Header("Detector things")]
    [SerializeField] string allowedTag = "InterObject";
    [SerializeField] List<InteractableObject> interactables;
    [SerializeField] float longestColliderEdge = 1;

    [Header("UI")]
    // ???
    [SerializeField] GameObject logPanel;
    [SerializeField] Text scoreText;
    [SerializeField] Text logText;
    [SerializeField] Image logIcon;

    [Header("misc")]
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
        CheckInteractables();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            try { OnInteract(); } catch (Exception e) { print(e.GetType()); print(e.Message); print(e.StackTrace); }
        }

        scoreText.text = $"Score: {score}";
    }

    void AddInteractable(InteractableObject interactableObject)
    {
        interactables.Add(interactableObject);
    }

    void CheckInteractables()
    {
        foreach(InteractableObject interactableObject in interactables.ToArray())
        {
            float distance = Vector2.Distance(transform.position, interactableObject.transform.position);
            if (distance > interactableObject.detectionRange + 0.5f + longestColliderEdge)
            {
                interactables.Remove(interactableObject);
            }
        }
    }

    void OnInteract()
    {
        InteractableObject interactableObject;
        interactableObject = GetNearestInteractable();

        string msg = interactableObject.infoString;
        int value = interactableObject.collectableValue;
        Sprite logIconSprite = interactableObject.GetComponent<SpriteRenderer>().sprite;

        logIcon.sprite = logIconSprite;
        logText.text = msg;

        logPanel.SetActive(true); logPanel.SendMessage("ResetTimer");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == allowedTag)
        {
            interactables.Add(collision.GetComponentInParent<InteractableObject>());
        }
    }

    /// <summary>
    /// Gets the nearest interactable object we know of.
    /// </summary>
    private InteractableObject GetNearestInteractable()
    {
        InteractableObject interactableObject = null;
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

        try { interactableObject = interactables[bestIndex]; } catch { }
        return interactableObject;
    }
}
