using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierSlot : MonoBehaviour
{
    [SerializeField] private Image modImage;

    public void SetModSprite(Sprite sprite)
    {
        modImage.sprite = sprite;
    }
}
