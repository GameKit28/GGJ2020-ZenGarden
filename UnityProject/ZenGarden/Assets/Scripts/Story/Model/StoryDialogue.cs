using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story.Model {
    [CreateAssetMenu(fileName = "New Dialog", menuName = GameMenuName + "Story Dialog")]
    public class StoryDialogue : StoryAsset, ITransitionDestination
    {
        public StoryCharacter Speaker;
        public string Dialogue;

        //public StoryOption[] Options = new StoryOption[1];
        public StoryDialogue NextDialogue;

    }
}