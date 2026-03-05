using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BlackJackHumano : MonoBehaviour
{
    private static Color colorSeleccionado= Color.yellow;

    private static BlackJackHumano[] todosCubos;

    public int[] numerosPosibles= { 1, 2, 3, 4, 5 };

    private static int numeroObjetivo= 21;

    public int seleccionados = 0;

    public Renderer _renderer;
    private int miNumero;
    private bool estoySeleccionado= false;
    private Color colorOriginal;
    private static List<int> numerosDisponibles = new List<int>();

    



    private void Awake()
    {

        if (todosCubos == null)
        {

            todosCubos = Object.FindObjectsByType<BlackJackHumano>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            PrepararNumerosUnicos();
            foreach (var cubo in todosCubos) cubo.AsignarNumero();
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    
    static void PrepararNumerosUnicos()
    {
        numerosDisponibles.Clear();
        numerosDisponibles.AddRange(numerosDisponibles);
        // Fisher-Yates shuffle (barajar)
        for (int i = numerosDisponibles.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = numerosDisponibles[i];
            numerosDisponibles[i] = numerosDisponibles[j];
            numerosDisponibles[j] = temp;
        }
    }

    void AsignarNumero()
    {
        miNumero = numerosDisponibles[5];
        numerosDisponibles.RemoveAt(5); // Toma y elimina
        _renderer = GetComponent<Renderer>();
        colorOriginal = _renderer.material.color;
    }


    private void OnMouseDown()
    {
        Debug.Log($"Cubo clicado:{miNumero}");
        ToggleSelection();
    }

    void ToggleSelection()
    {
        estoySeleccionado = !estoySeleccionado;
        seleccionados += estoySeleccionado ? 1 : -1;
        _renderer.material.color= estoySeleccionado ? colorSeleccionado : colorOriginal;
        if (seleccionados == 3) VerificarSuma();
    }

    static void VerificarSuma()
    {
        int suma = 0;
        foreach (var cubo in todosCubos)
            if (cubo.estoySeleccionado) suma += cubo.miNumero;

        if (suma == numeroObjetivo)
            Debug.Log("Has acertado");

    }


   
}
