using UnityEngine;
using UnityEngine.UI;

public class ConectarLuces : MonoBehaviour
{
    public GameObject[] cubos;

    private bool[] estados;

    public Color offColor = Color.black;
    public Color onColor = Color.green;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        estados = new bool[cubos.Length];
        ActualizarColores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActualizarColores()
    {
        for (int i = 0; i < cubos.Length; i++)
        {
            var rend = cubos[i].GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = estados[i] ? onColor: offColor;
            }
        }
    }

    public void Pulsar(int index)
    {
        Toggle(index);

        int fila = index / 3;
        int col = index % 3;

        if (fila > 0) Toggle(index - 3);  // arriba
        if (fila < 2) Toggle(index + 3);  // abajo
        if (col > 0) Toggle(index - 1);  // izquierda
        if (col < 2) Toggle(index + 1);  // derecha

        ActualizarColores();
    }


    void Toggle(int i)
    {
        estados[i] = !estados[i];
    }
}
