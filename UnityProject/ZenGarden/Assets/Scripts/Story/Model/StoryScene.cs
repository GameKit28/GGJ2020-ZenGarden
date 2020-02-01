using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story.Model {
    [CreateAssetMenu()]
    public class StoryScene : ScriptableObject, ITransitionDestination 
    {
        public StorySetting Setting;
        public StoryDialogueSequence StartingDialogue;
    }
}