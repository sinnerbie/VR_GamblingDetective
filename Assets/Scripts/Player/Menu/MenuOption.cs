using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOption : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuOpciones; // Canvas del menú

    [Header("Input")]
    public InputActionReference menuButton; // <XRController>{LeftHand}/menuButton

    [Header("Player")]
    public GameObject locomotion; // locomotion del jugador

    private bool enPausa = false;

    void Start()
    {
        // Aseguramos estado inicial correcto
        if (menuOpciones != null)
            menuOpciones.SetActive(false);

        if (locomotion != null)
            locomotion.SetActive(true);
    }

    void OnEnable()
    {
        if (menuButton != null)
        {
            menuButton.action.performed += OnMenuPressed;
            menuButton.action.Enable();
        }
    }

    void OnDisable()
    {
        if (menuButton != null)
        {
            menuButton.action.performed -= OnMenuPressed;
            menuButton.action.Disable();
        }
    }

    void OnMenuPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;

        if (enPausa)
            ReanudarJuego();
        else
            PausarJuego();
    }

    void PausarJuego()
    {
        if (menuOpciones != null)
            menuOpciones.SetActive(true);

        if (locomotion != null)
            locomotion.SetActive(false); // DESACTIVA movimiento

        Time.timeScale = 0f;
        enPausa = true;

        Debug.Log("Juego en pausa");
    }

    void ReanudarJuego()
    {
        if (menuOpciones != null)
            menuOpciones.SetActive(false);

        if (locomotion != null)
            locomotion.SetActive(true); // ACTIVA movimiento

        Time.timeScale = 1f;
        enPausa = false;

        Debug.Log("Juego reanudado");
    }
}