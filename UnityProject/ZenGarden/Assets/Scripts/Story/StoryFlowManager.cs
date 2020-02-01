using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Story.Model;

namespace Story {
    public class StoryFlowManager : MonoBehaviour, IOptionClickHandler, INextClickHandler {
        private StoryDialogueSequence currentDialogueSequence;
        private uint currentDialogueSequenceIndex;

        public DialogueBox DialogueBox;

        public void HandleOptionClick(StoryOption option) {
            Debug.Log("I got the option click event!");
        }

        public void HandleNextClick() {
            Debug.Log("I got the next click event!");
        }

        void Start() {
            if(DialogueBox) {
                DialogueBox.RegisterForOptionClickEvents(this);
                DialogueBox.RegisterForNextClickEvents(this);
            }
        }
    }
}