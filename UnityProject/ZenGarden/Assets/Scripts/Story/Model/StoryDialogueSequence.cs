using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Story.Model {
    [CreateAssetMenu()]
    public class StoryDialogueSequence : NamedScriptableObject, ITransitionDestination
    {
        public List<StoryDialogue> Dialogues = new List<StoryDialogue>();

        public List<StoryOption> Buttons;
    }
}