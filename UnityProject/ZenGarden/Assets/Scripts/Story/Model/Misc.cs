using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 //TODO: Break each of these classes into their own files. -Kit
 namespace Story.Model {
    public class StoryOption {
        public string ButtonText = "Next";

        public List<StoryTransition> Target;

    }

    public class StoryTransition {
        public ITransitionDestination Target;
    }

    public class PuzzleLauncher : ITransitionDestination
    {

    }

    public interface ITransitionDestination {}
 }