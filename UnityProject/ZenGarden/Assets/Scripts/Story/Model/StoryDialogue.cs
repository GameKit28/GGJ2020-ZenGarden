using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story.Model {
    [System.Serializable]
    public class StoryDialogue
    {
        public StoryCharacter Speaker;
        public string Dialogue;
        public Emotion Emotion = Emotion.Neutral;
    }

    public enum Emotion
    {
        Neutral,
        Happy,
        Upset,
        Stoned,
    }
}