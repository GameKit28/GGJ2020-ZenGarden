using System;
using System.Collections.Generic;
using Story.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameState
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int StartAt = 0;
        public int LevelsDone = 0;
        
        public List<StoryScene> StartingStoryScenes = new List<StoryScene>();
        
        private HashSet<StoryScene> CompletedStoryScenes = new HashSet<StoryScene>();

        private AudioSource AudioSource;

        public List<AudioClip> Sounds;
        
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
            for (int i = 0; i < StartAt; i++)
            {
                MarkSceneCompleted(StartingStoryScenes[i]);
            }
        }

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
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

        public int GetScenesCompleted()
        {
            return CompletedStoryScenes.Count;
        }

        public void LoadNewUnityScene(string sceneName)
        {
            Debug.Log("Loading New Unity Scene: " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void PlaySoundClip(string name)
        {
            AudioClip clipToPlay = null;
            foreach (AudioClip clip in Sounds)
            {
                if (clip.name == name)
                {
                    clipToPlay = clip;
                }
            }
            AudioSource.PlayOneShot(clipToPlay);
        }
    }
}