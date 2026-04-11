using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class GuardarEnBolsillo : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] string tagBolsillo = "Bolsillo";
    [SerializeField] bool hacerHijoDelBolsillo = true;
    [SerializeField] Vector3 posicionLocalEnBolsillo = Vector3.zero;
    [SerializeField] Vector3 rotacionLocalEnBolsillo = Vector3.zero;

    Rigidbody rb;
    XRGrabInteractable grabInteractable;

    Transform bolsilloActual;
    Transform padreOriginal;

    bool gravedadOriginal;
    bool isKinematicOriginal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        padreOriginal = transform.parent;
        gravedadOriginal = rb.useGravity;
        isKinematicOriginal = rb.isKinematic;
    }

    void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagBolsillo))
        {
            bolsilloActual = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagBolsillo) && bolsilloActual == other.transform)
        {
            bolsilloActual = null;
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        SacarDelBolsillo();
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (bolsilloActual != null)
        {
            IntentarGuardarEnBolsillo();
        }
        else
        {
            Caer();
        }
    }

    public void SacarDelBolsillo()
    {
        transform.SetParent(padreOriginal);
        rb.useGravity = gravedadOriginal;
        rb.isKinematic = isKinematicOriginal;
    }

    public void Caer()
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void IntentarGuardarEnBolsillo()
    {
        if (bolsilloActual == null)
            return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        if (hacerHijoDelBolsillo)
        {
            transform.SetParent(bolsilloActual);
            transform.localPosition = posicionLocalEnBolsillo;
            transform.localRotation = Quaternion.Euler(rotacionLocalEnBolsillo);
        }
        else
        {
            transform.position = bolsilloActual.TransformPoint(posicionLocalEnBolsillo);
            transform.rotation = bolsilloActual.rotation * Quaternion.Euler(rotacionLocalEnBolsillo);
        }
    }
}