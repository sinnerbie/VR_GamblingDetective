using UnityEngine;
using TMPro;


public class CajaFuerte : MonoBehaviour
{
    int clave1, clave2, clave3, clave4;

    public Renderer cajaRenderer;
    public Color colorCorrecto = Color.green;

    //Numeros Randoms que hay que poner en el candado
    public TextMeshProUGUI num1;
    public TextMeshProUGUI num2;
    public TextMeshProUGUI num3;
    public TextMeshProUGUI num4;

    //Numeros que hay que acertar en el candado
    public TextMeshProUGUI num1Combinacion;
    public TextMeshProUGUI num2Combinacion;
    public TextMeshProUGUI num3Combinacion;
    public TextMeshProUGUI num4Combinacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NumerosAleatorios();
    }

    void Update()
    {
       
    }

    public void NumerosAleatorios()
    {
        clave1 = Random.Range(0, 10);
        clave2 = Random.Range(0, 10);
        clave3 = Random.Range(0, 10);
        clave4 = Random.Range(0, 10);

        num1.text = clave1.ToString();
        num2.text = clave2.ToString();
        num3.text = clave3.ToString();
        num4.text = clave4.ToString();

        //Combinacion final en consola
        Debug.Log($"CLAVE: {clave1}{clave2}{clave3}{clave4}");
    }

    public void ComprobarCombinacion()
    {
        int intento1 = int.Parse(num1Combinacion.text);
        int intento2 = int.Parse(num2Combinacion.text);
        int intento3 = int.Parse(num3Combinacion.text);
        int intento4 = int.Parse(num4Combinacion.text);

        if (intento1 == clave1 &&
            intento2 == clave2 &&
            intento3 == clave3 &&
            intento4 == clave4)
        {
            CajaCorrecta();
        }
        else
        {
            Debug.Log("No es esa combinación");
            ResetearCandado();
        }
    }

    public void IncrementarNumero(TextMeshProUGUI textoNumero)
    {
        int valorActual = int.Parse(textoNumero.text);

        valorActual++;

        if (valorActual > 9)
            valorActual = 0;

        textoNumero.text = valorActual.ToString();
    }

    void CajaCorrecta()
    {
        Debug.Log("Combinación correcta");
        cajaRenderer.material.color = colorCorrecto;
    }

    void ResetearCandado()
    {
        num1Combinacion.text = "0";
        num2Combinacion.text = "0";
        num3Combinacion.text = "0";
        num4Combinacion.text = "0";
    }
}
