using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Story.Model;

namespace Story {
    public class StoryFlowManager : MonoBehaviour, IOptionClickHandler, INextClickHandler {
        private StoryDialogueSequence currentDialogueSequence;
        private uint currentDialogueSequenceIndex;

        public DialogueBox DialogueBox;

        public StoryScene StartingScene;

        public void HandleOptionClick(StoryOption option) {
            Debug.Log("I got the option click event!");
        }

        public void HandleNextClick() {
            if (currentDialogueSequence)
            {
                if (currentDialogueSequenceIndex < currentDialogueSequence.Dialogues.Count - 1)
                {
                    currentDialogueSequenceIndex++;
                    DialogueBox.SetDialogue(currentDialogueSequence.Dialogues[(int)currentDialogueSequenceIndex]);
                }
            }
        }

        void Start() {
            if(DialogueBox) {
                DialogueBox.RegisterForOptionClickEvents(this);
                DialogueBox.RegisterForNextClickEvents(this);

                if (StartingScene)
                {
                    StartNewSequence(StartingScene.StartingDialogue);
                }
            }
        }

        private void StartNewSequence(StoryDialogueSequence sequence)
        {
            currentDialogueSequence = sequence;
            currentDialogueSequenceIndex = 0;
            if (DialogueBox)
            {
                DialogueBox.SetDialogue(currentDialogueSequence.Dialogues[(int)currentDialogueSequenceIndex]);
            }
        }
    }
}