using System.Collections;
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

    [Header("Fuera")]
    [SerializeField] GameObject puerta;
    public Animator ani;

    private string codigoActual = "";
    private bool canOpen;

    private void Start()
    {
        ActualizarUI();
        Debug.Log(codigoCorrecto);
    }

    public void PulsarNumero(int numero)
    {
        
        if (codigoActual.Length >= codigoCorrecto.Length)
            return;

        codigoActual += numero.ToString();
        ActualizarUI();

       // if (codigoActual.Length == codigoCorrecto.Length)
            //canOpen = true;
       // ComprobarCodigo();
    }
    public void PulsarPomo()
    {

        ComprobarCodigo();
    }
    public void LimpiarCodigo()
    {
        codigoActual = "";
        ActualizarUI();
    }

    private void ComprobarCodigo()
    {
        Debug.Log("numero elegido " + codigoActual);
        if (codigoActual == codigoCorrecto)
        {
            Debug.Log("Código correcto");
            alAcertar?.Invoke();
            ani.SetTrigger("Open");
            ani.SetBool("Fail", false);
        }
        else
        {
            Debug.Log("Código incorrecto");
            alFallar?.Invoke();
            ani.SetBool("Fail", true);
            StartCoroutine(WaitAnim());
            //LimpiarCodigo();
        }
    }
    private IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(0.3f);
        ani.SetBool("Fail", false);

    }

    public void Correcto()
    {
        Debug.Log("Caja Abierta");
        puerta.SetActive(false);
    }

    private void ActualizarUI()
    {
        if (valorPuesto == null)
            return;

        valorPuesto.text = string.IsNullOrEmpty(codigoActual) ? "----" : codigoActual;
        ani.SetBool("Fail", false);
    }
}