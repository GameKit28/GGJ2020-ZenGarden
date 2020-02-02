using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Story.Model {

    [CreateAssetMenu(fileName = "New Scene", menuName = GameMenuName + "Story Scene")]
    public class StoryScene : StoryAsset, ITransitionDestination
    {
        public StorySetting Setting;
        public StoryDialogueSequence StartingDialogue;
    }
}