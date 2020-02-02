using Story.Model;
using UnityEngine;
using UnityEngine.UI;

public class DialogCharacterController : MonoBehaviour
{
    [SerializeField] private Image leftCharacter;
    [SerializeField] private Image leftCharacterTalking;
    [SerializeField] private Animator leftCharacterAnimator;

    [SerializeField] private Image rightCharacter;
    [SerializeField] private Image rightCharacterTalking;
    [SerializeField] private Animator rightCharacterAnimator;

    private const string IntroAnimation = "Intro";
    private const string OutroAnimation = "Outro";
    private const string TalkAnimation = "Talk";
    
    public enum Character
    {
        Left = 1,
        Right = 2,
        Both = Left | Right,
    }
    
    public enum StagePosition
    {
        OnStage,
        OffStage
    }

    private StagePosition LeftStagePos = StagePosition.OffStage;
    private StagePosition RightStagePos = StagePosition.OffStage;

    public void SetCharacters(StoryCharacter leftCharacter, StoryCharacter rightCharacter)
    {
        if (leftCharacter)
        {
            this.leftCharacter.sprite = leftCharacter.sprite;
            this.rightCharacter.sprite = rightCharacter.sprite;
            LeftStagePos = StagePosition.OffStage;
        }

        if (rightCharacter)
        {
            this.leftCharacterTalking.sprite = leftCharacter.talkingSprite;
            this.rightCharacterTalking.sprite = rightCharacter.talkingSprite;
            RightStagePos = StagePosition.OffStage;
        }
    }

    public void DoIntro(Character character)
    {
        Debug.Log("For " + character + ", Has: " + character.Has(Character.Left) + ", " + character.Has(Character.Right));
        if (character.Has(Character.Left) && LeftStagePos == StagePosition.OffStage)
        {
            leftCharacterAnimator.SetTrigger(IntroAnimation);
            LeftStagePos = StagePosition.OnStage;
        }

        if (character.Has(Character.Right) && RightStagePos == StagePosition.OffStage)
        {
            rightCharacterAnimator.SetTrigger(IntroAnimation);
            RightStagePos = StagePosition.OnStage;
        }
    }

    public void DoOutro(Character character)
    {
        if (character.Has(Character.Left) && LeftStagePos == StagePosition.OnStage)
        {
            leftCharacterAnimator.SetTrigger(OutroAnimation);
            LeftStagePos = StagePosition.OffStage;
        }

        if (character.Has(Character.Right) && RightStagePos == StagePosition.OnStage)
        {
            rightCharacterAnimator.SetTrigger(OutroAnimation);
            RightStagePos = StagePosition.OffStage;
        }
    }

    public void DoTalking(Character character)
    {
        if (character.Has(Character.Left))
        {
            leftCharacterAnimator.SetTrigger(TalkAnimation);
        }

        if (character.Has(Character.Right))
        {
            rightCharacterAnimator.SetTrigger(TalkAnimation);
        }
    }

    //Slide in / fade in

    //Change bg

    //Talking animations?
}

public static class CharacterUtils
{
    public static bool Has(this DialogCharacterController.Character character, DialogCharacterController.Character entry)
    {
        return (character & entry) != 0;
    }
}
