using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Story.Model {
    [CreateAssetMenu(fileName = "New Character", menuName = GameMenuName + "Story Dialogue Sequence")]
    public class StoryDialogueSequence : StoryAsset, ITransitionDestination
    {
        public List<StoryDialogue> Dialogues = new List<StoryDialogue>();

        public List<StoryOption> Buttons;
    }
}