using UnityEngine;

public class PuzzleReloj : MonoBehaviour
{
    [Header("Referencias")]
    public Transform agujaHoras, agujaMinutos;
    public float tolerancia = 5f;
    public GameObject recompensa;

    private bool puzzleIniciado = false; // Nueva bandera
    private bool resuelto = false;

    void Start()
    {
        // Pequeño delay para permitir primeras rotaciones
        Invoke(nameof(IniciarChequeo), 0.5f);
    }

    void IniciarChequeo()
    {
        puzzleIniciado = true;
    }

    void Update()
    {
        if (!puzzleIniciado || resuelto) return;

        float rotH = NormalizarAngulo(agujaHoras.localEulerAngles.z);
        float rotM = NormalizarAngulo(agujaMinutos.localEulerAngles.z);

        if (rotH < tolerancia && rotM < tolerancia)
        {
            ResolverPuzzle();
        }
    }

    float NormalizarAngulo(float angulo)
    {
        // Convierte a 0-360
        if (angulo >= 360f) angulo -= 360f;
        if (angulo < 0f) angulo += 360f;
        return angulo;
    }

    void ResolverPuzzle()
    {
        Debug.Log("¡Puzzle resuelto!");
        resuelto = true;
        if (recompensa) recompensa.SetActive(true);
    }
}