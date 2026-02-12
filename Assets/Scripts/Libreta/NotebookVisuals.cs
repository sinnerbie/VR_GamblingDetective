using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NotebookVisuals : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;

    Animator _Animator;

    public string info;

    void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void fTurnPage(bool nextPage)
    {
        _Text.text = "";
        if (nextPage)
            _Animator.Play("TurnPage");
        else
            _Animator.Play("BackPage");
    }

    public void fOpenCloseNotes(bool open)
    {
        _Text.text = "";
        if (open)
            _Animator.Play("Open");
        else
            _Animator.Play("Close");
    }

    public void fUpdateInfo()
    {
        _Text.text = info;
    }

    public void fClearInfo()
    {
        _Text.text = "";
    }
}
