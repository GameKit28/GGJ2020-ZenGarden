using UnityEngine;

namespace Story.Model
{
    [CreateAssetMenu(fileName = "New Character", menuName = GameMenuName + "Story Character")]
    public class StoryCharacter : StoryAsset 
    {
        public string Name;

        public Sprite sprite;
        public Sprite talkingSprite;
    }
}