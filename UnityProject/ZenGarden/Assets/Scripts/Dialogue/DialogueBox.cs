﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Story.Model;

namespace Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        public GameObject LeftSpeakerPanel;
        public GameObject RightSpeakerPanel;
        public Text LeftNameText;
        public Text RightNameText;
        public Text DialogueText;

        public GameObject NextButtonPanel;
        public GameObject DynamicButtonPanel;
        public GameObject SingleOptionButtonPanel;

        public GameObject DynamicButtonPrefab;

        
        public StoryDialogue DefaultDialogue;

        List<INextClickHandler> NextClickListeners = new List<INextClickHandler>();
        List<IOptionClickHandler> OptionClickListeners = new List<IOptionClickHandler>();
    
        // Start is called before the first frame update
        void Awake()
        {
            SetDialogue(DialogCharacterController.Character.Left, DefaultDialogue);
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

        public void SetDialogue(DialogCharacterController.Character character, StoryDialogue dialogue) {
            LeftSpeakerPanel.gameObject.SetActive(false);
            RightSpeakerPanel.gameObject.SetActive(false);
            if (character == DialogCharacterController.Character.Left)
            {
                LeftSpeakerPanel.gameObject.SetActive(true);
                LeftNameText.text = dialogue.Speaker.Name;
            }
            else if (character == DialogCharacterController.Character.Right)
            {
                RightSpeakerPanel.gameObject.SetActive(true);
                RightNameText.text = dialogue.Speaker.Name;
            }

            DialogueText.text = dialogue.Dialogue;
        }

        public void SetOptions(List<StoryOption> options)
        {
            if (options.Count == 0)
            {
                //Enable Just the Next Button
                NextButtonPanel.SetActive(true);
                DynamicButtonPanel.SetActive(false);
                SingleOptionButtonPanel.SetActive(false);
            }
            else
            {
                NextButtonPanel.SetActive(false);
                DynamicButtonPanel.SetActive(false);
                SingleOptionButtonPanel.SetActive(false);

                //Delete all children
                foreach (Transform child in DynamicButtonPanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                
                if (options.Count > 1)
                {
                    DynamicButtonPanel.SetActive(true);
                    //Create new children
                    foreach (StoryOption sOption in options)
                    {
                        GameObject newButton = GameObject.Instantiate(DynamicButtonPrefab, DynamicButtonPanel.transform);
                        newButton.GetComponentInChildren<Text>().text = sOption.ButtonText;
                        newButton.GetComponent<Button>().onClick.AddListener(() => { ClickOptionEvent(sOption); });
                    }

                }
                else
                {
                    SingleOptionButtonPanel.SetActive(true);
                    SingleOptionButtonPanel.GetComponentInChildren<Text>().text = options[0].ButtonText;
                    SingleOptionButtonPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                    SingleOptionButtonPanel.GetComponentInChildren<Button>().onClick.AddListener(() => { ClickOptionEvent(options[0]); });
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

