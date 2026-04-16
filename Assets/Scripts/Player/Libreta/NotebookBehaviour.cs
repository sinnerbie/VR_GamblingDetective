using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NotebookBehaviour : MonoBehaviour
{
    [Header("Adjust Canvas")]
    [SerializeField] private Canvas _Canvas;

    [Header("Manage Clues")]
    [SerializeField] private GameObject _ClueItem;
    [SerializeField] GameObject clueArea;
    [System.Serializable] public class Clue { public Sprite _sprite; public string message; }
    [SerializeField] List<Clue> _Clues = new List<Clue>();
    [SerializeField] private int cluesPerPage = 6;
    [SerializeField] private int currentPage = 1;

    NotebookVisuals _Visuals;

    void Awake()
    {
        _Visuals = GetComponent<NotebookVisuals>();
    }

    void Start()
    {
        _Canvas.worldCamera = Camera.main;
    }

    [ContextMenu("Try add")]
    public void TryAdd()
    {
        fAddClue(null, "effervecent");
    }

    [ContextMenu("Turn Page")]
    public void TryTurnPage()
    {
        fTurnToPage(true);
    }

    [ContextMenu("Open Book")]
    public void TryOpenBook()
    {
        fToggleNotebook(true);
    }

    public void fTurnToPage(bool nextPage)
    {
        if (nextPage)
        {
            float totalClues = _Clues.Count;
            if (currentPage + 1 <= Mathf.Ceil(totalClues / cluesPerPage))
            {
                currentPage++;
                fPrintClues(currentPage);
                _Visuals.fTurnPage(true);
            }
        }
        else
        {
            if (currentPage != 1)
            {
                currentPage--;
                fPrintClues(currentPage);
                _Visuals.fTurnPage(false);
            }
        }
    }

    public void fToggleNotebook(bool open)
    {
        fPrintClues(currentPage);
        _Visuals.fOpenCloseNotes(open);
    }

    public void fAddClue(Sprite thumbnail, string newClueMsg)
    {
        Clue newClu = new Clue();
        newClu._sprite = thumbnail;
        newClu.message = newClueMsg;
        _Clues.Add(newClu);
    }

    void fPrintClues(int pageID)
    {
        fDeleteClues();
        int pageNo = (pageID * cluesPerPage) - (cluesPerPage - 1);
        Debug.Log(pageNo);
        for (int i = pageNo; i < pageNo + cluesPerPage; i++)
        {
            Debug.Log(i);
            if (i < _Clues.Count)
            {
                GameObject clueObject = Instantiate(_ClueItem);
                clueObject.transform.SetParent(clueArea.transform);
                clueObject.GetComponent<ClueItem>().UpdateContent(_Clues[i]._sprite, _Clues[i].message);
            }
        }
    }

    public void fDeleteClues()
    {
        foreach (Transform child in clueArea.transform)
            GameObject.Destroy(child.gameObject);
    }
}
