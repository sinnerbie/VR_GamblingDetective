using UnityEngine;

public class NotebookVisuals : MonoBehaviour
{
    Animator _Animator;

    void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void fTurnPage(bool nextPage)
    {
        if (nextPage)
            _Animator.Play("TurnPage");
        else
            _Animator.Play("BackPage");
    }

    public void fOpenCloseNotes(bool open)
    {
        if (open)
            _Animator.Play("Open");
        else
            _Animator.Play("Close");
    }

    public void fUpdateInfo()
    {

    }

    public void fClearInfo()
    {
        GetComponent<NotebookBehaviour>().fDeleteClues();
    }
}
