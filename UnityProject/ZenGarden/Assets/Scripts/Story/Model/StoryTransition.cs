namespace Story.Model
{
    [System.Serializable]
    public class StoryTransition {
        public ITransitionDestination Target;
    }
    
    public interface ITransitionDestination {}
}