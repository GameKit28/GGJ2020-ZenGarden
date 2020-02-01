using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Story.Model;

public class DialogueBox : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public Button NextButton; 

    public StoryDialogue DefaultDialogue;
    public StoryDialogue NextDialogue;

    // Start is called before the first frame update
    void Start()
    {
        SetDialogue(DefaultDialogue);
    }

    public void ClickNextEvent() {
        SetDialogue(NextDialogue);
    }

    public void SetDialogue(StoryDialogue dialogue) {
        NameText.text = dialogue.Speaker.Name;
        DialogueText.text = dialogue.Dialogue;
    }
}
