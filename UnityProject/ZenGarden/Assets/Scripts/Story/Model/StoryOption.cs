using System.Collections.Generic;
using System.Collections;

namespace Story.Model {
    [System.Serializable]
    public class StoryOption {
        public string ButtonText = "Next";

        public List<StoryTransition> Target;
    }
}