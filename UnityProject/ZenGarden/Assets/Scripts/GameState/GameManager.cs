using System;
using System.Collections.Generic;
using Story.Model;
using UnityEngine;

namespace GameState
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public List<StoryScene> StartingStoryScenes = new List<StoryScene>();
        
        private HashSet<StoryScene> CompletedStoryScenes = new HashSet<StoryScene>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                GameObject.DontDestroyOnLoad(this);
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public StoryScene GetNextScene()
        {
            foreach (StoryScene scene in StartingStoryScenes)
            {
                if (!CompletedStoryScenes.Contains(scene))
                {
                    return scene;
                }
            }

            return null;
        }

        public void MarkSceneCompleted(StoryScene scene)
        {
            CompletedStoryScenes.Add(scene);
        }

        public bool HasCompletedAnyScenes()
        {
            return CompletedStoryScenes.Count > 0;
        }
    }
}