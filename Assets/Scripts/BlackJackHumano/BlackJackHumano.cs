using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BlackJackHumano : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private List<CuboData> cubos = new List<CuboData>(); // Lista de cubos con sus valores
    [SerializeField] private int objetivo = 21; // Número objetivo a alcanzar

    [Header("Colores")]
    [SerializeField] private Color colorNormal = Color.cyan; // Color inicial de los cubos
    [SerializeField] private Color colorSeleccionado = Color.green; // Color al seleccionar un cubo
    [SerializeField] private Color colorVictoria = Color.gold; // Color al ganar

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoSumaActual; // Texto que muestra la suma actual
    [SerializeField] private TextMeshProUGUI textoMensaje; // Texto que muestra mensajes al jugador

    // Variables privadas
    private List<CuboData> cubosSeleccionados = new List<CuboData>(); // Cubos que el jugador ha seleccionado
    private int sumaActual = 0; // Suma acumulada de los cubos seleccionados
    private bool juegoTerminado = false; // Controla si el juego ha finalizado (victoria)

    void Start()
    {
        // Configurar cada cubo al inicio del juego
        foreach (var cubo in cubos)
        {
            if (cubo.cuboObject == null) continue; // Saltar si no hay cubo asignado

            cubo.colorOriginal = colorNormal; // Guardar el color normal para futuros reinicios
            ConfigurarCubo(cubo); // Añadir componentes necesarios para la interacción
            MostrarNumeroEnCubo(cubo); // Mostrar el valor numérico en el cubo
            CambiarColorCubo(cubo, colorNormal); // Establecer color inicial
        }

        ActualizarUI();
        ActualizarMensaje("¡Selecciona cubos hasta sumar 21!");
    }


    // Configura un cubo con Collider y ClickHandler para detectar interacciones
    void ConfigurarCubo(CuboData cubo)
    {
        // Asegurar que el cubo tiene un collider para detectar clicks
        if (cubo.cuboObject.GetComponent<Collider>() == null)
            cubo.cuboObject.AddComponent<BoxCollider>();

        // Añadir o obtener el componente que maneja los clicks
        var clickHandler = cubo.cuboObject.GetComponent<CuboClickHandler>();
        if (clickHandler == null)
            clickHandler = cubo.cuboObject.AddComponent<CuboClickHandler>();

        // Vincular este manager con el cubo
        clickHandler.Inicializar(this, cubo);
    }


    // Muestra el valor numérico del cubo usando TextMeshPro
    void MostrarNumeroEnCubo(CuboData cubo)
    {
        var textMesh = cubo.cuboObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
            textMesh.text = cubo.valor.ToString();
    }


    // Método principal llamado cuando el jugador hace clic en un cubo
    // Contiene toda la lógica de selección, suma y reinicio
    public void SeleccionarCubo(CuboData cubo)
    {
        // Verificar si el juego ya terminó (victoria)
        if (juegoTerminado)
        {
            ActualizarMensaje("¡Juego terminado!");
            return;
        }

        // Evitar seleccionar el mismo cubo dos veces
        if (cubosSeleccionados.Contains(cubo))
        {
            ActualizarMensaje($"¡El cubo {cubo.valor} ya fue seleccionado!");
            return;
        }

        int nuevaSuma = sumaActual + cubo.valor;

        // CASO 1: Suma EXACTAMENTE 21 - VICTORIA
        if (nuevaSuma == objetivo)
        {
            cubosSeleccionados.Add(cubo);
            sumaActual = nuevaSuma;
            ActualizarUI();

            // Cambiar color de TODOS los cubos seleccionados a dorado (victoria)
            foreach (var c in cubosSeleccionados)
                CambiarColorCubo(c, colorVictoria);

            juegoTerminado = true; // Marcar juego como terminado
            ActualizarMensaje($"¡FELICIDADES! ¡Sumaste exactamente {objetivo}!");
            DesactivarSelecciones(); // Bloquear más interacciones
        }
        // CASO 2: Suma MENOR a 21 - CONTINUAR JUGANDO
        else if (nuevaSuma < objetivo)
        {
            cubosSeleccionados.Add(cubo);
            sumaActual = nuevaSuma;
            ActualizarUI();
            CambiarColorCubo(cubo, colorSeleccionado); // Marcar cubo como seleccionado
            ActualizarMensaje($"Suma actual: {sumaActual}. Te faltan {objetivo - sumaActual}");
        }
        // CASO 3: Suma MAYOR a 21 - REINICIAR SELECCIÓN
        else
        {
            ActualizarMensaje($"¡Te pasaste! Sumabas {sumaActual} y sumaste {cubo.valor} = {nuevaSuma}. Reiniciando...");
            ReiniciarSeleccion(); // Reiniciar toda la selección actual
        }
    }


    // Reinicia la selección actual sin afectar el estado de victoria
    // Útil cuando el jugador se pasa de 21
    public void ReiniciarSeleccion()
    {
        // Restaurar todos los cubos seleccionados a su color normal
        foreach (var cubo in cubosSeleccionados)
            CambiarColorCubo(cubo, colorNormal);

        // Limpiar lista de seleccionados y reiniciar suma
        cubosSeleccionados.Clear();
        sumaActual = 0;
        ActualizarUI();
        ActualizarMensaje("Selección reiniciada. ¡Vuelve a intentarlo!");
    }


    // Reinicia completamente el juego, permitiendo empezar de nuevo desde cero
    public void ReiniciarJuego()
    {
        ReiniciarSeleccion(); // Reiniciar selección actual
        juegoTerminado = false; // Reactivar el juego
        ActivarSelecciones(); // Volver a permitir clicks
        ActualizarMensaje("¡Juego reiniciado! Selecciona cubos para sumar 21.");
    }


    // Cambia el color de un cubo específico
    void CambiarColorCubo(CuboData cubo, Color color)
    {
        var renderer = cubo.cuboObject.GetComponent<Renderer>();
        if (renderer != null) renderer.material.color = color;
    }


    // Actualiza el texto que muestra la suma actual en la UI
    void ActualizarUI()
    {
        if (textoSumaActual != null)
            textoSumaActual.text = $"Suma: {sumaActual} / {objetivo}";
    }


    // Actualiza el mensaje informativo en la UI
    void ActualizarMensaje(string mensaje)
    {
        if (textoMensaje != null)
            textoMensaje.text = mensaje;
        else
            Debug.Log(mensaje);
    }


    // Desactiva todos los clicks en los cubos (cuando se gana)
    void DesactivarSelecciones()
    {
        foreach (var cubo in cubos)
        {
            var clickHandler = cubo.cuboObject?.GetComponent<CuboClickHandler>();
            if (clickHandler != null) clickHandler.enabled = false;
        }
    }


    // Activa todos los clicks en los cubos (cuando se reinicia el juego)

    void ActivarSelecciones()
    {
        foreach (var cubo in cubos)
        {
            var clickHandler = cubo.cuboObject?.GetComponent<CuboClickHandler>();
            if (clickHandler != null) clickHandler.enabled = true;
        }
    }
}

// Estructura de datos para cada cubo
// Almacena la referencia al GameObject y su valor numérico
[System.Serializable]
public class CuboData
{
    public GameObject cuboObject; // Referencia al cubo en la escena
    public int valor; // Valor numérico que aporta este cubo a la suma
    [HideInInspector] public Color colorOriginal; // Color original para restaurar después
}