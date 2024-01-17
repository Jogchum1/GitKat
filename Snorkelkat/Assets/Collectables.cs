using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    [SerializeField] private List<Image> stickers = new List<Image>();
    [SerializeField] private List<TMP_Text> stickerTexts = new List<TMP_Text>();

    public void CollectSticker(int index)
    {
        stickers[index].color = Color.white;
        //stickers[index].gameObject.GetComponent<Animator>().enabled = true;
        stickerTexts[index].gameObject.SetActive(false);
    }
}
