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

    public enum Character
    {
        Left = 1,
        Right = 2,
        Both = Left | Right,
    }

    public void SetCharacters(StoryCharacter leftCharacter, StoryCharacter rightCharacter)
    {
        this.leftCharacter.sprite = leftCharacter.sprite;
        this.rightCharacter.sprite = rightCharacter.sprite;

        this.leftCharacterTalking.sprite = leftCharacter.talkingSprite;
        this.rightCharacterTalking.sprite = rightCharacter.talkingSprite;
    }

    public void DoIntro(Character character)
    {
        Debug.Log("For " + character + ", Has: " + character.Has(Character.Left) + ", " + character.Has(Character.Right));
        if (character.Has(Character.Left))
        {
            leftCharacterAnimator.SetTrigger("Intro");
        }

        if (character.Has(Character.Right))
        {
            rightCharacterAnimator.SetTrigger("Intro");
        }
    }

    public void DoOutro(Character character)
    {
        if (character.Has(Character.Left))
        {
            leftCharacterAnimator.SetTrigger("Outro");
        }

        if (character.Has(Character.Right))
        {
            rightCharacterAnimator.SetTrigger("Outro");
        }
    }

    public void DoTalking(Character character)
    {
        if (character.Has(Character.Left))
        {
            leftCharacterAnimator.SetTrigger("Talk");
        }

        if (character.Has(Character.Right))
        {
            rightCharacterAnimator.SetTrigger("Talk");
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
