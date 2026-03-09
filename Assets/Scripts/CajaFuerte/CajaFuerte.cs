using UnityEngine;
using TMPro;


public class CajaFuerte : MonoBehaviour
{
    int[] claves = new int[4]; //clave1, clave2, clave3, clave4

    public Renderer cajaRenderer;
    public Color colorCorrecto = Color.green;

    //Lista de Numeros Randoms que hay que poner en el candado
    public TextMeshProUGUI[] numeros;

    //Lista Numeros que hay que acertar en el candado
    public TextMeshProUGUI[] numCombinacion; 

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
        for (int i = 0; i < claves.Length; i++)
        {
            claves[i] = Random.Range(0, 10);
            numeros[i].text = claves[i].ToString();
        }

        //Combinacion final en consola
        Debug.Log($"CLAVE: {claves[0]}{claves[1]}{claves[2]}{claves[3]}");
    }

    public void ComprobarCombinacion()
    {
        //int i = 0;
        //while(i < 4)
        //{
        //    if (claves[i] == int.Parse(numCombinacion[i].text)) 
        //    {
        //        i++;
        //    }
        //    else
        //    {
        //        ResetearCandado();
        //        return;
        //    }
        //}
        //CajaCorrecta();

        for (int i = 0; i < claves.Length; i++)
        {
            if (claves[i] != int.Parse(numCombinacion[i].text))
            {
                ResetearCandado();
                return;
            }
        }
        CajaCorrecta();
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
        for(int i = 0; i < numCombinacion.Length; i++)
        {
            numCombinacion[i].text = "0";
        }
    }
}
