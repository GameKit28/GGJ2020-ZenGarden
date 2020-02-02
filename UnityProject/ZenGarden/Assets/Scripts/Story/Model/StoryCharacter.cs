using Dialogue;
using UnityEngine;
using Utilities;

namespace Story.Model {
    [CreateAssetMenu(fileName = "New Character", menuName = GameMenuName + "Story Character")]
    public class StoryCharacter : StoryAsset {
        public string Name;

        public CharacterSpriteSet HappySprites;
        public CharacterSpriteSet NeutralSprites;
        public CharacterSpriteSet UpsetSprites;
        public CharacterSpriteSet StonedSprites;
    }
}