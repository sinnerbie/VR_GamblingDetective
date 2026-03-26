using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<InventoryItem> _KeyStash = new List<InventoryItem>();
    [SerializeField] private List<InventoryItem> _MiscStash = new List<InventoryItem>();
    [Header("Define Item Positions")]
    [SerializeField] private List<Transform> _KeyPositions = new List<Transform>();
    [SerializeField] private List<Transform> _MiscPositions = new List<Transform>();

    [Header("Asign Default Position")]
    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Quaternion defaultRot;

    void Awake()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;
    }

    public void SnapItem(InventoryItem newIt)
    {
        if (newIt.itemType == InventoryItem.Item.Key)
        {
            if (_KeyStash.Count == _KeyPositions.Count) return;
            newIt.stashed = true;
            _KeyStash.Add(newIt);
            for (int i = 0; i < _KeyPositions.Count; i++)
                if (_KeyPositions[i].childCount == 0) 
                {
                    newIt.inventoryPos = _KeyPositions[i]; 
                    return;
                }
        }
        else if (newIt.itemType == InventoryItem.Item.Miscelaneous)
        {
            if (_MiscStash.Count == _MiscPositions.Count) return;
            newIt.stashed = true;
            _MiscStash.Add(newIt);
            for (int i = 0; i < _MiscPositions.Count; i++)
                if (_MiscPositions[i].childCount == 0) 
                {
                    newIt.inventoryPos = _MiscPositions[i]; 
                    return;
                }
        }
    }

    public void ReleaseItem(InventoryItem newIt)
    {
        newIt.stashed = false;
        newIt.inventoryPos = null;
        if (newIt.itemType == InventoryItem.Item.Key)
            _KeyStash.Remove(newIt);
        else if (newIt.itemType == InventoryItem.Item.Miscelaneous)
            _MiscStash.Remove(newIt);
    }

    public void OnGrabInventory()
    {
        // Play animation
        for (int i = 0; i < _KeyStash.Count; i++)
            _KeyStash[i].gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        for (int i = 0; i < _MiscStash.Count; i++)
            _MiscStash[i].gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
    }

    public void OnReleaseInventory()
    {
        for (int i = 0; i < _KeyStash.Count; i++)
            _KeyStash[i].gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
        for (int i = 0; i < _MiscStash.Count; i++)
            _MiscStash[i].gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;

        transform.position = defaultPos;
        transform.rotation = defaultRot;
    }
}
