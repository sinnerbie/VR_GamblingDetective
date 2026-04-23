using UnityEngine;
using UnityEngine.EventSystems;

public class ControlBotonRelojSimple : MonoBehaviour, IPointerClickHandler
{
    [Header("Referencias")]
    public Transform pivoteAguja;
    public float gradosPorPulsacion = 6f;
    public bool sentidoHorario = true;

    void Start()
    {
        Debug.Log($"Botón simple iniciado: {gameObject.name}");

        if (pivoteAguja == null)
        {
            Debug.LogError($"ERROR: Asigna el pivoteAguja en {gameObject.name}");
        }

        if (GetComponent<Collider>() == null)
        {
            Debug.LogError($"ERROR: {gameObject.name} no tiene Collider!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pivoteAguja == null) return;

        // Invertido: sentidoHorario = true ahora rota en sentido horario
        float rot = gradosPorPulsacion * (sentidoHorario ? -1f : 1f);
        pivoteAguja.Rotate(0, 0, rot, Space.Self);

        Debug.Log($"Nueva rotación: {pivoteAguja.localEulerAngles.z}");
    }

    void OnMouseDown()
    {
        OnPointerClick(null);
    }
}