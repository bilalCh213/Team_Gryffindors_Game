using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] dialogues;
    [SerializeField] private int dialogueIndex = 0;
    [Space]
    [SerializeField] private bool dialogueOnce = false;
    [SerializeField] private float characterDelay = 50.0f;
    [SerializeField] private string dialogueInput;
    [Space]
    [SerializeField] private TextMeshProUGUI dialogueDisplay;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI[] dialogueOptions;
    [SerializeField] private PlayerController plCont;

    private float characterTimer = 0.0f;
    private int characterIndex = 0;

    private void SetDialogueActivity(bool value)
    {
        dialogueDisplay.gameObject.SetActive(value);
        dialoguePanel.SetActive(value);
        for (int i = 0; i < dialogueOptions.Length; i++)
            dialogueOptions[i].gameObject.SetActive(value);
        plCont.enabled = !value;
    }
    
    void Start()
    {
        SetDialogueActivity(true);
    }

    void OnEnable()
    {
        SetDialogueActivity(true);
    }

    void Update()
    {
        if(Input.GetButtonDown(dialogueInput))
        {
            if (characterIndex < dialogues[dialogueIndex].Length)
            {
                dialogueDisplay.text = dialogues[dialogueIndex];
                characterIndex = dialogues[dialogueIndex].Length - 1;
            }
            else
            {
                dialogueDisplay.text = "";
                
                characterTimer = 0;
                characterIndex = 0;
                dialogueIndex++;

                if (dialogueIndex >= dialogues.Length)
                {
                    SetDialogueActivity(false);
                    
                    dialogueIndex = 0;
                    characterIndex = 0;
                    characterTimer = 0;
                    dialogueDisplay.text = "";
                    
                    if (dialogueOnce)
                        Destroy(this);
                    else
                        gameObject.SetActive(false);
                }
            }
        }
        else if (characterTimer <= 0 && characterIndex < dialogues[dialogueIndex].Length)
        {
            dialogueDisplay.text += dialogues[dialogueIndex][characterIndex];
            
            characterTimer = characterDelay;
            characterIndex++;
        }
        
        if(characterTimer > 0)
        {
            characterTimer -= Time.deltaTime;
        }
    }
}
