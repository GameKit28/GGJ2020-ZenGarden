using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Story.Model;
using Dialogue;
using GameState;
using JetBrains.Annotations;

namespace Story {
    public class StoryFlowManager : MonoBehaviour, IOptionClickHandler, INextClickHandler {
        private StoryDialogueSequence currentDialogueSequence;
        private uint currentDialogueSequenceIndex;

        public DialogueBox DialogueBox;
        public Background Background;
        public DialogCharacterController CharacterController;

        public StoryScene StartingSceneOverride;

        public StoryCharacter LeftCharacter;
        public StoryCharacter RightCharacter;

        public void HandleOptionClick(StoryOption option) {
            Debug.Log("I got the option click event! " + option.ButtonText);

            //Let's just assume they clicked the first button for now
            ITransitionDestination target = option.GetTarget();
            if (target is StoryScene)
            {
                StartNewScene((StoryScene)target);
            } else if (target is StoryDialogueSequence)
            {
                StartNewSequence((StoryDialogueSequence)target);
            } else if (target is UnitySceneTransition)
            {
                GameManager.Instance.LoadNewUnityScene((target as UnitySceneTransition).SceneToLoad);
            }
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
                    
                    SetDialogue();
                }
                else
                {
                    Debug.LogError("We shouldn't have reached this point because the NextButtonPanel should be hidden.");
                }
            }
        }

        void Start() {
            if(DialogueBox) {
                DialogueBox.RegisterForOptionClickEvents(this);
                DialogueBox.RegisterForNextClickEvents(this);
            }

            StoryScene firstScene = StartingSceneOverride ? StartingSceneOverride : GameManager.Instance.GetNextScene();
            StartNewScene(firstScene);

            if (CharacterController)
            {
                CharacterController.SetCharacters(LeftCharacter, RightCharacter);
            }
        }

        private void StartNewScene([NotNull] StoryScene scene)
        {
            Debug.Log("Starting Scene: " + scene);
            GameManager.Instance.MarkSceneCompleted(scene);
            
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
                SetDialogue();
            }
        }

        private void SetDialogue()
        {
            
            bool lastInSequence = currentDialogueSequenceIndex == currentDialogueSequence.Dialogues.Count - 1;
            if (lastInSequence)
            {
                DialogueBox.SetOptions(currentDialogueSequence.Buttons);
            }
            else
            {
                DialogueBox.SetOptions(new List<StoryOption>());
            }
            
            StoryDialogue dialogue = currentDialogueSequence.Dialogues[(int) currentDialogueSequenceIndex];
            DialogueBox.SetDialogue(dialogue);
        }
    }
}