using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class DrawerInteractable : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private XRSimpleInteractable xrSimpleInteractable;

    [Header("Movimiento del cajón")]
    [SerializeField] private Vector3 openedLocalPosition;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool startsOpened = false;

    private Vector3 originalLocalPosition;
    private Vector3 targetLocalPosition;
    private bool isOpened;

    private void Reset()
    {
        xrSimpleInteractable = GetComponent<XRSimpleInteractable>();
    }

    private void Awake()
    {
        if (xrSimpleInteractable == null)
            xrSimpleInteractable = GetComponent<XRSimpleInteractable>();

        if (xrSimpleInteractable != null && xrSimpleInteractable.interactionManager == null)
        {
            XRInteractionManager manager = FindFirstObjectByType<XRInteractionManager>();

            if (manager != null)
            {
                xrSimpleInteractable.interactionManager = manager;
            }
            else
            {
                Debug.LogWarning($"[{name}] No se encontró ningún XRInteractionManager en la escena.");
            }
        }

        originalLocalPosition = transform.localPosition;
        isOpened = startsOpened;
        targetLocalPosition = isOpened ? openedLocalPosition : originalLocalPosition;
        transform.localPosition = targetLocalPosition;
    }

    private void OnEnable()
    {
        if (xrSimpleInteractable == null)
            xrSimpleInteractable = GetComponent<XRSimpleInteractable>();

        if (xrSimpleInteractable != null)
            xrSimpleInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        if (xrSimpleInteractable != null)
            xrSimpleInteractable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            targetLocalPosition,
            moveSpeed * Time.deltaTime);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        ToggleDrawer();
    }

    public void ToggleDrawer()
    {
        isOpened = !isOpened;
        targetLocalPosition = isOpened ? openedLocalPosition : originalLocalPosition;
    }
}