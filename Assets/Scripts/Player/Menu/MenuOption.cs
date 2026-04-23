using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOption : MonoBehaviour
{
    [Header("UI")]
    public GameObject menuOpciones; // Canvas del menú

    [Header("Input")]
    public InputActionReference menuButton; // <XRController>{LeftHand}/menuButton

    private bool enPausa = false;

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
        // Evita múltiples activaciones raras
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

        Time.timeScale = 0f;
        enPausa = true;

        Debug.Log("Juego en pausa");
    }

    void ReanudarJuego()
    {
        if (menuOpciones != null)
            menuOpciones.SetActive(false);

        Time.timeScale = 1f;
        enPausa = false;

        Debug.Log("Juego reanudado");
    }
}
