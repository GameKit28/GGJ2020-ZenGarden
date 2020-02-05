using System;
using System.Collections.Generic;
using Story.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Random = System.Random;

namespace GameState
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int StartAt = 0;
        public bool SkipStories = false;
        public HashSet<string> LevelsDone = new HashSet<string>();
        public string[] MarkLevelsAsDone;
        
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
            foreach (var sceneName in MarkLevelsAsDone)
            {
                LevelsDone.Add(sceneName);
            }
        }

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public StoryScene GetNextScene()
        {
            if (SkipStories) return null;

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

        public void MarkCurrentLevelCompleted()
        {
            LevelsDone.Add(SceneManager.GetActiveScene().name);
        }

        public bool IsCurrentLevelCompleted()
        {
            return LevelsDone.Contains(SceneManager.GetActiveScene().name);
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

        public void PlaySoundClip(params string[] clipNames)
        {
            List<AudioClip> candidateClips = GetAudioClipByName(clipNames);

            if (candidateClips.Count > 0)
            {
                AudioSource.PlayOneShot(candidateClips[UnityEngine.Random.Range(0, candidateClips.Count)]);
            }
        }

        public void LoopMusic(string clipName)
        {
            List<AudioClip> candidateClips = GetAudioClipByName(clipName);

            if (candidateClips.Count > 0)
            {
                AudioSource.Stop();
                AudioSource.clip = candidateClips[0];
                AudioSource.Play();
            }
        }

        private List<AudioClip> GetAudioClipByName(params string[] clipNames)
        {
            List<AudioClip> candidateClips = new List<AudioClip>();
            //O(X*Y) Not the best, but this is a gamejam -Kit
            foreach (AudioClip clip in Sounds)
            {
                foreach(string paramName in clipNames)
                {
                    if(clip.name == paramName) candidateClips.Add(clip);
                }
            }

            return candidateClips;
        }

        internal void StartLevelSelect()
        {
            SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
        }
    }
}