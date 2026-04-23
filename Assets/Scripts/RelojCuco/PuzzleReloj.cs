using UnityEngine;

public class PuzzleReloj : MonoBehaviour
{
    [Header("Referencias")]
    public Transform pivoteHoras;
    public Transform pivoteMinutos;
    public GameObject recompensa;

    [Header("Configuración del Puzzle")]
    [Tooltip("Número al que debe apuntar la aguja de horas (1-12)")]
    public int numeroHoras = 12;
    [Tooltip("Número al que debe apuntar la aguja de minutos (0-60, donde 0 = 12)")]
    public int numeroMinutos = 0;
    [Tooltip("Tolerancia en grados para considerar que apunta al número")]
    public float tolerancia = 5f;

    private bool puzzleIniciado = false;
    private bool resuelto = false;

    // Grados por número en el reloj
    private float gradosPorHora = 360f / 12f;      // 30 grados por hora
    private float gradosPorMinuto = 360f / 60f;    // 6 grados por minuto

    void Start()
    {
        Invoke(nameof(IniciarChequeo), 0.5f);
    }

    void IniciarChequeo()
    {
        puzzleIniciado = true;
    }

    void Update()
    {
        if (!puzzleIniciado || resuelto) return;

        // Leemos la rotación actual de los pivotes
        float rotH = NormalizarAngulo(pivoteHoras.localEulerAngles.z);
        float rotM = NormalizarAngulo(pivoteMinutos.localEulerAngles.z);

        // Calculamos el ángulo objetivo para cada aguja
        float objetivoHoras = CalcularAnguloObjetivoHoras(numeroHoras);
        float objetivoMinutos = CalcularAnguloObjetivoMinutos(numeroMinutos);

        // Verificamos si ambas agujas están en los ángulos objetivo
        bool horasCorrectas = Mathf.Abs(DiferenciaAngulos(rotH, objetivoHoras)) <= tolerancia;
        bool minutosCorrectos = Mathf.Abs(DiferenciaAngulos(rotM, objetivoMinutos)) <= tolerancia;

        if (horasCorrectas && minutosCorrectos)
        {
            ResolverPuzzle();
        }
    }

    float CalcularAnguloObjetivoHoras(int numero)
    {
        // Convertir número (1-12) a ángulo (0-360)
        // 12 = 0 grados, 1 = 30 grados, 2 = 60 grados, etc.
        if (numero == 12) return 0f;
        return numero * gradosPorHora;
    }

    float CalcularAnguloObjetivoMinutos(int numero)
    {
        // Convertir número (0-60, donde 0 = 12) a ángulo (0-360)
        // 0 = 0 grados, 5 = 30 grados, 10 = 60 grados, etc.
        if (numero == 0) return 0f;
        if (numero == 60) return 0f;
        return numero * gradosPorMinuto;
    }

    float DiferenciaAngulos(float a, float b)
    {
        float diff = Mathf.Abs(a - b);
        return Mathf.Min(diff, 360f - diff);
    }

    float NormalizarAngulo(float angulo)
    {
        angulo = angulo % 360f;
        if (angulo < 0f) angulo += 360f;
        return angulo;
    }

    void ResolverPuzzle()
    {
        Debug.Log($"¡Puzzle resuelto! Horas apunta al {numeroHoras}, Minutos apunta al {numeroMinutos}");
        resuelto = true;
        if (recompensa) recompensa.SetActive(true);
    }
}