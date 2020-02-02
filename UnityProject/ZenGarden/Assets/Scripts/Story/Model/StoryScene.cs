using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Story.Model {
    [CreateAssetMenu()]
    public class StoryScene : NamedScriptableObject, ITransitionDestination 
    {
        public StorySetting Setting;
        public StoryDialogueSequence StartingDialogue;
    }
}