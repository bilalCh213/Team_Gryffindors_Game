using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DialogueAnimationList : MonoBehaviour
{
    [SerializeField] private string[] dialogues;
    public int index = -1;
    public bool toNextScene = false;
    [SerializeField] private string nextScene;

    private TextMeshPro tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void GoToNextScene()
    {
        
    }

    void Update()
    {
        if(index > -1 && index < dialogues.Length)
        {
            tmp.text = dialogues[index];
        }
        else
        {
            tmp.text = "";
        }

        if(toNextScene)
            SceneManager.LoadScene(nextScene);
    }
}
