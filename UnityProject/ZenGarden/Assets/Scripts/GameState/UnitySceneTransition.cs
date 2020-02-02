using Story.Model;
using UnityEngine;

namespace GameState
{
    [CreateAssetMenu(fileName = "New Unity Scene Transition", menuName = GameMenuName + "Unity Scene Transition")]
    public class UnitySceneTransition : StoryAsset, ITransitionDestination
    {
        public string SceneToLoad;
    }
}