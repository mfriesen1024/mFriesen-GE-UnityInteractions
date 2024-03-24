using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> Queue = new Queue<string>();
    [SerializeField] Text logText;
    [SerializeField] Image logIcon;
    [SerializeField] AutoHide autoHide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewDialogue(InteractableObject interactableObject)
    {
        string[] msgs = interactableObject.infoStrings;
        Sprite logIconSprite = interactableObject.GetComponent<SpriteRenderer>().sprite;

        logIcon.sprite = logIconSprite;

        Queue.Clear();

        foreach (string msg in msgs)
        {
            Queue.Enqueue(msg);
        }
        
        if(interactableObject.type == interactableType.dialogue) { SetDialoguePause(true); }
    }

    public void UpdateDialogue()
    {
        autoHide.ResetTimer();
        try { logText.text = Queue.Dequeue(); } catch (Exception ignored) { }

        autoHide.gameObject.SetActive(true);
        if (Queue.Count <= 0) { autoHide.shouldHide = true; SetDialoguePause(false); }
    }

    void SetDialoguePause(bool shouldPause)
    {
        if (shouldPause) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }
}
