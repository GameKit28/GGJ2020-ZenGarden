using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Story.Model;

namespace Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        public Text NameText;
        public Text DialogueText;

        public GameObject NextButtonPanel;
        public GameObject DynamicButtonPanel;

        public GameObject DynamicButtonPrefab;

        
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
            Debug.Log("Dialogue Box Received Option: " + option.ButtonText);
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

        public void SetOptions(List<StoryOption> options)
        {
            if (options.Count == 0)
            {
                //Enable Just the Next Button
                NextButtonPanel.SetActive(true);
                DynamicButtonPanel.SetActive(false);
            }
            else
            {
                NextButtonPanel.SetActive(false);
                DynamicButtonPanel.SetActive(true);
                
                //Delete all children
                foreach (Transform child in DynamicButtonPanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                
                //Create new children
                foreach (StoryOption sOption in options)
                {
                    GameObject newButton = GameObject.Instantiate(DynamicButtonPrefab, DynamicButtonPanel.transform);
                    newButton.GetComponentInChildren<Text>().text = sOption.ButtonText;
                    newButton.GetComponent<Button>().onClick.AddListener(() => {ClickOptionEvent(sOption);});
                }
            }
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
}

