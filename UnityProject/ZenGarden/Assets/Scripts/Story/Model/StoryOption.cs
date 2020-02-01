using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Story.Model {
    [System.Serializable]
    public class StoryOption {
        public string ButtonText = "Next";
        public Object Target;

        public ITransitionDestination GetTarget()
        {
            if(Target.GetType().IsSubclassOf(typeof(ITransitionDestination)))
            {
                return (ITransitionDestination)Target;
            }
            throw new SystemException("Target attached to StoryOption is not a ITransitionDestination");
        }
    }
}