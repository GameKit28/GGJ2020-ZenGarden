using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story.Model {
    [CreateAssetMenu()]
    public class StoryDialogue : ScriptableObject, ITransitionDestination
    {
        public StoryCharacter Speaker;
        public string Dialogue;

        //public StoryOption[] Options = new StoryOption[1];
        public StoryDialogue NextDialogue;

    }
}