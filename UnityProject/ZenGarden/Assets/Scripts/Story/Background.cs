using Story.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class Background : MonoBehaviour
    {
        public Image backgroundImage;
        
        public void SetBackground(StorySetting setting)
        {
            backgroundImage.sprite = setting.BackgroundImage;
        }
    }
}