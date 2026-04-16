using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ClueItem : MonoBehaviour
{
    [SerializeField] Image photo;
    [SerializeField] TMP_Text message;

    public void UpdateContent(Sprite newIm, string newMsg)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (photo.sprite != null) photo.sprite = newIm;
        message.text = newMsg;
    }
}
