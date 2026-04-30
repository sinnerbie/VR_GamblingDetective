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

    [Header("Cámara")]
    public GameObject playerCamera; // Cámara del jugador

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
        if (menuOpciones != null && playerCamera != null)
        {
            // Posiciona el menú frente a la cámara y lo orienta hacia ella
            Vector3 camForward = playerCamera.transform.forward;
            Vector3 camPosition = playerCamera.transform.position;
            Vector3 menuPosition = camPosition + camForward.normalized * 2.0f; // 2 unidades frente a la cámara
            menuPosition.y = camPosition.y; // A la altura de la cámara

            menuOpciones.transform.position = menuPosition;
            menuOpciones.transform.LookAt(camPosition);
            menuOpciones.transform.rotation = Quaternion.Euler(0, menuOpciones.transform.rotation.eulerAngles.y, 0); // Solo rota en Y
            menuOpciones.SetActive(true);
        }

        if (locomotion != null)
            locomotion.SetActive(false); // DESACTIVA movimiento

        Time.timeScale = 0f;
        enPausa = true;

        Debug.Log("Juego en pausa");
    }

    void ReanudarJuego()
    {
        if (menuOpciones != null && playerCamera != null)
        {
            // Reposiciona el menú a la altura de la cámara (puedes ajustar la posición si lo deseas)
            Vector3 menuPos = menuOpciones.transform.position;
            menuPos.y = playerCamera.transform.position.y;
            menuOpciones.transform.position = menuPos;
            menuOpciones.SetActive(false);
        }
        else if (menuOpciones != null)
        {
            menuOpciones.SetActive(false);
        }

        if (locomotion != null)
            locomotion.SetActive(true); // ACTIVA movimiento

        Time.timeScale = 1f;
        enPausa = false;

        Debug.Log("Juego reanudado");
    }
}