using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ClueItem : MonoBehaviour
{
    [SerializeField] Image photo;
    [SerializeField] TMP_Text message;

    public void UpdateContent(Sprite newIm, string newMsg)
    {
        photo.sprite = newIm;
        message.text = newMsg;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
