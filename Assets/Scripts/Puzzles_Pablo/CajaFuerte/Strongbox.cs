using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Strongbox : MonoBehaviour
{
    [Header("Código")]
    [SerializeField] private string codigoCorrecto = "1234";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI valorPuesto;

    [Header("Eventos")]
    [SerializeField] private UnityEvent alAcertar;
    [SerializeField] private UnityEvent alFallar;

    private string codigoActual = "";

    private void Start()
    {
        ActualizarUI();
    }

    public void PulsarNumero(int numero)
    {
        if (codigoActual.Length >= codigoCorrecto.Length)
            return;

        codigoActual += numero.ToString();
        ActualizarUI();

        if (codigoActual.Length == codigoCorrecto.Length)
            ComprobarCodigo();
    }

    public void LimpiarCodigo()
    {
        codigoActual = "";
        ActualizarUI();
    }

    private void ComprobarCodigo()
    {
        if (codigoActual == codigoCorrecto)
        {
            Debug.Log("Código correcto");
            alAcertar?.Invoke();
        }
        else
        {
            Debug.Log("Código incorrecto");
            alFallar?.Invoke();
            LimpiarCodigo();
        }
    }

    public void Correcto()
    {
        Debug.Log("Caja Abierta");
    }

    private void ActualizarUI()
    {
        if (valorPuesto == null)
            return;

        valorPuesto.text = string.IsNullOrEmpty(codigoActual) ? "----" : codigoActual;
    }
}