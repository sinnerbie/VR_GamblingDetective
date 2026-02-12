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
    [SerializeField] private TMP_Text _Test;
    [System.Serializable] public class Page { public List<string> clues = new List<string>(new string[1]); }
    [SerializeField] List<Page> pages = new List<Page>();
    [SerializeField] private int currentPage = 0;

    NotebookVisuals _Visuals;

    void Awake()
    {
        _Visuals = GetComponent<NotebookVisuals>();
    }

    void Start()
    {
        _Canvas.worldCamera = Camera.main;
        fAddClue("New Clue");
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
            if (currentPage != pages.Count - 1)
            {
                currentPage++;
                fPrintClues(currentPage);
                _Visuals.fTurnPage(true);
            }
        }
        else
        {
            if (currentPage != 0)
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
        _Visuals.fOpenCloseNotes(true);
    }

    public void fAddClue(string newClue)
    {
        StartCoroutine(fSortNewClue(newClue));
    }

    IEnumerator fSortNewClue(string newClue)
    {
        _Test.text = "";
        string content = "";
        for (int i = 0; i < pages[pages.Count - 1].clues.Count; i++)
        {
            content += pages[pages.Count - 1].clues[i];
            content += "\n";
        }
        content += newClue;
        _Test.text = content;
        yield return new WaitForEndOfFrame();
        if (_Test.isTextTruncated)
        {
            Page newPage = new Page();
            newPage.clues[0] = newClue;
            pages.Add(newPage);
        }
        else
            pages[pages.Count - 1].clues.Add(newClue);

        _Test.text = "";
        StopCoroutine("fSortNewClue");
    }

    void fPrintClues(int pageID)
    {
        string content = "";
        for (int i = 0; i < pages[pageID].clues.Count; i++)
        {
            content += pages[pageID].clues[i];
            content += "\n";
        }

        _Visuals.info = content;
    }
}
