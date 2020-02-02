using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Story.Model;
using Dialogue;
using JetBrains.Annotations;

namespace Story {
    public class StoryFlowManager : MonoBehaviour, IOptionClickHandler, INextClickHandler {
        private StoryDialogueSequence currentDialogueSequence;
        private uint currentDialogueSequenceIndex;

        public DialogueBox DialogueBox;
        public Background Background;
        public DialogCharacterController CharacterController;

        public StoryScene StartingScene;

        public StoryCharacter LeftCharacter;
        public StoryCharacter RightCharacter;

        public void HandleOptionClick(StoryOption option) {
            Debug.Log("I got the option click event!");
        }

        public void HandleNextClick() {
            if (currentDialogueSequence)
            {
                if (currentDialogueSequenceIndex < currentDialogueSequence.Dialogues.Count - 1)
                {
                    currentDialogueSequenceIndex++;
                    StoryDialogue dialogue = currentDialogueSequence.Dialogues[(int) currentDialogueSequenceIndex];

                    if (CharacterController)
                    {
                        if (dialogue.Speaker == LeftCharacter)
                        {
                            CharacterController.DoTalking(DialogCharacterController.Character.Left);
                        }

                        if (dialogue.Speaker == RightCharacter)
                        {
                            CharacterController.DoTalking(DialogCharacterController.Character.Right);
                        }
                    }
                    
                    DialogueBox.SetDialogue(dialogue);
                }
                else
                {
                    //We've Reached the End of Our Sequence
                    if (currentDialogueSequence.Buttons.Count == 0)
                    {
                        //Let's just repeat the sequence. No where to go.
                    }
                    else if (currentDialogueSequence.Buttons.Count > 0)
                    {
                        //Let's just assume they clicked the first button for now
                        ITransitionDestination target = currentDialogueSequence.Buttons[0].GetTarget();
                        if (target is StoryScene)
                        {
                            StartNewScene((StoryScene)target);
                        } else if (target is StoryDialogueSequence)
                        {
                            StartNewSequence((StoryDialogueSequence)target);
                        }
                    }
                }
            }
        }

        void Start() {
            if(DialogueBox) {
                DialogueBox.RegisterForOptionClickEvents(this);
                DialogueBox.RegisterForNextClickEvents(this);
            }
            
            StartNewScene(StartingScene);

            if (CharacterController)
            {
                CharacterController.SetCharacters(LeftCharacter, RightCharacter);
            }
        }

        private void StartNewScene([NotNull] StoryScene scene)
        {
            Debug.Log("Starting Scene: " + scene);
            
            if (CharacterController)
            {
                CharacterController.DoOutro(DialogCharacterController.Character.Both);
            }
            
            StartNewSequence(scene.StartingDialogue);

            if (Background)
            {
                Background.SetBackground(scene.Setting);
            }
        }
        
        private void StartNewSequence([NotNull] StoryDialogueSequence sequence)
        {
            Debug.Log("Starting Sequence: " + sequence);
            currentDialogueSequence = sequence;
            currentDialogueSequenceIndex = 0;

            StoryDialogue dialogue = currentDialogueSequence.Dialogues[(int) currentDialogueSequenceIndex];

            if (CharacterController)
            {
                if (dialogue.Speaker == LeftCharacter)
                {
                    CharacterController.DoIntro(DialogCharacterController.Character.Left);
                }

                if (dialogue.Speaker == RightCharacter)
                {
                    CharacterController.DoIntro(DialogCharacterController.Character.Right);
                }
            }
            
            if (DialogueBox)
            {
                DialogueBox.SetDialogue(dialogue);
            }
        }
    }
}