using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ControlBotonReloj : MonoBehaviour
{
    [Header("Referencias")]
    public XRBaseInteractable botonInteractable; // El XRSimpleInteractable del botón
    public Transform aguja; // Aguja a rotar
    public float gradosPorPulsacion = 6f; // 6 min, 30 horas
    public bool sentidoHorario = true;

    void OnEnable()
    {
        Debug.Log(" tu abuela");
        if (botonInteractable != null)
        {
            
            botonInteractable.activated.AddListener(OnBotonActivado);
        }
    }

    void OnDisable()
    {
        if (botonInteractable != null)
        {
            botonInteractable.activated.RemoveListener(OnBotonActivado);
        }
    }

    void OnBotonActivado(ActivateEventArgs args)
    {
        if (aguja == null) return;
        Debug.Log("me comi a tu abuela");
        float rot = gradosPorPulsacion * (sentidoHorario ? 1f : -1f);
        aguja.Rotate(0, rot, 0);
    }
}