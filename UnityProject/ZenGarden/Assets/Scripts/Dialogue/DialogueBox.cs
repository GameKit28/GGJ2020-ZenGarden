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

    List<INextClickHandler> NextClickListeners = new List<INextClickHandler>();
    List<IOptionClickHandler> OptionClickListeners = new List<IOptionClickHandler>();
    
    // Start is called before the first frame update
    void Awake()
    {
        SetDialogue(DefaultDialogue);
    }

    public void ClickNextEvent() {
        foreach (INextClickHandler handler in NextClickListeners)
        {
            handler.HandleNextClick();
        }
    }
    
    public void ClickOptionEvent(StoryOption option) {
        foreach (IOptionClickHandler handler in OptionClickListeners)
        {
            handler.HandleOptionClick(option);
        }
    }

    public void RegisterForNextClickEvents(INextClickHandler self)
    {
        NextClickListeners.Add(self);
    }
    
    public void RegisterForOptionClickEvents(IOptionClickHandler self)
    {
        OptionClickListeners.Add(self);
    }

    public void SetDialogue(StoryDialogue dialogue) {
        NameText.text = dialogue.Speaker.Name;
        DialogueText.text = dialogue.Dialogue;
    }
}

public interface INextClickHandler
{
    void HandleNextClick();
}
    
public interface IOptionClickHandler
{
    void HandleOptionClick(StoryOption option);
}
