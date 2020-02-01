using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public Button NextButton; 

    // Start is called before the first frame update
    void Start()
    {
        SetDialogue("TestName", "TestDialogue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogue(string name, string dialogue) {
        NameText.text = name;
        DialogueText.text = dialogue;
    }
}
