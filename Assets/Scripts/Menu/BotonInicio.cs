using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class BotonInicio : MonoBehaviour
{
    public string escenaDestino = "GameScene";

    private InputAction anyAction;
    private bool triggered = false;
    private bool armed = false;

    public TextMeshProUGUI texto;
    public float velocidad = 1f;

    void Awake()
    {
        anyAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "*/<Button>"
        );
    }

    void OnEnable()
    {
        anyAction.performed += OnAnyInput;
        anyAction.Enable();

        StartCoroutine(ArmSystem());
    }

    IEnumerator ArmSystem()
    {
        // Espera 2 frames reales de Unity + XR estabilización
        yield return null;
        yield return null;

        armed = true;
    }

    void OnDisable()
    {
        anyAction.performed -= OnAnyInput;
        anyAction.Disable();
    }

    void Update()
    {
        if (texto == null) return;

        float alpha = Mathf.PingPong(Time.time * velocidad, 1f);

        Color color = texto.color;
        color.a = alpha;
        texto.color = color;
    }

    void OnAnyInput(InputAction.CallbackContext ctx)
    {
        if (!armed) return; 
        if (triggered) return;

        triggered = true;
        SceneManager.LoadScene(escenaDestino);
    }
}