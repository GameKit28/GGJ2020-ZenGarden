using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story.Model {
    [CreateAssetMenu()]
    public class StoryDialogueSequence : ScriptableObject, ITransitionDestination
    {
        public List<StoryDialogue> Dialogues = new List<StoryDialogue>();

        public List<StoryOption> Buttons;
    }
}