using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LightTile : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material completeMaterial;

    [Header("Estado")]
    [SerializeField] private bool isOn = false;

    [Header("Referencias")]
    [SerializeField] private XRSimpleInteractable interactable;
    [SerializeField] private LightsOutManager manager;

    [Header("Tiles afectados por este botón")]
    [SerializeField] private List<LightTile> affectedTiles = new List<LightTile>();

    private bool showCompletedVisual;

    public bool IsOn => isOn;
    public List<LightTile> AffectedTiles => affectedTiles;

    private void Reset()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        targetRenderer = GetComponentInChildren<Renderer>();
    }

    private void Awake()
    {
        if (interactable == null)
            interactable = GetComponent<XRSimpleInteractable>();

        UpdateVisual();
    }

    private void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        if (manager != null)
        {
            manager.PressTile(this);
        }
        else
        {
            foreach (var tile in affectedTiles)
            {
                if (tile != null)
                    tile.Toggle();
            }
        }
    }

    public void Toggle()
    {
        isOn = !isOn;
        showCompletedVisual = false;
        UpdateVisual();
    }

    public void SetState(bool value)
    {
        isOn = value;
        showCompletedVisual = false;
        UpdateVisual();
    }

    public void SetCompletedVisual(bool completed)
    {
        showCompletedVisual = completed;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (targetRenderer == null) return;

        if (showCompletedVisual && completeMaterial != null)
        {
            targetRenderer.material = completeMaterial;
            return;
        }

        targetRenderer.material = isOn ? onMaterial : offMaterial;
    }
}