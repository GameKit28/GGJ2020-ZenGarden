using Story.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogCharacters : MonoBehaviour
{
    [SerializeField] private StoryCharacter leftCharacter;
    [SerializeField] private StoryCharacter rightCharacter;

    private DialogCharacterController dialogCharacterController;

    private void Start()
    {
        dialogCharacterController = GameObject.FindObjectOfType<DialogCharacterController>();
        if (dialogCharacterController == null)
        {
            Debug.LogError("Couldn't find controller for testing", this);
        }
        dialogCharacterController.SetCharacters(leftCharacter, rightCharacter);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogCharacterController.DoIntro(DialogCharacterController.Character.Left);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogCharacterController.DoTalking(DialogCharacterController.Character.Left);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dialogCharacterController.DoOutro(DialogCharacterController.Character.Left);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dialogCharacterController.DoIntro(DialogCharacterController.Character.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            dialogCharacterController.DoTalking(DialogCharacterController.Character.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            dialogCharacterController.DoOutro(DialogCharacterController.Character.Right);
        }
    }
}
