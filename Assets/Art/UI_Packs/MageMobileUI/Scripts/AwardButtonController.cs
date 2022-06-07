using System;
using UnityEngine;
using UnityEngine.UI;

namespace DragonMobileUI.Scripts
{
    public class AwardButtonController : MonoBehaviour
    {
        public StarColor starColor;
        [Range(0, 3)]
        public int starCount;
        
        public Attributes attributes;

        private void OnValidate()
        {
            for (var i = 0; i < attributes.placeholders.Length; i++)
            {
                var placeholder = attributes.placeholders[i];
                if (i + 1 <= starCount)
                {
                    placeholder.gameObject.SetActive(true);
                    Sprite sprite;
                    switch (starColor)
                    {
                        case StarColor.Gold:
                            sprite = attributes.goldStar;
                            break;
                        case StarColor.Bronze:
                            sprite = attributes.bronzeStar;
                            break;
                        case StarColor.Silver:
                            sprite = attributes.silverStar;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    placeholder.sprite = sprite;
                }
                else
                {
                    placeholder.gameObject.SetActive(false);
                }
            }
        }
    }

    [Serializable]
    public enum StarColor
    {
        Gold, Bronze, Silver
        
    }

    [Serializable]
    public class Attributes
    {
        public Sprite goldStar;
        public Sprite bronzeStar;
        public Sprite silverStar;

        public Image[] placeholders;
    }
}
