using UnityEngine;
using UnityEngine.InputSystem;

public class Linterna : MonoBehaviour
{
    public Light flashlightLight;
    public InputActionReference triggerPressed;

    public Light flashLightUltraVioleta;
    public InputActionReference triggerPressedUltraVioleta;

    void OnEnable()
    {
        triggerPressed.action.performed += OnTriggerPressed;
        triggerPressed.action.Enable();

        triggerPressedUltraVioleta.action.performed += OnTriggerPressedUltraVioleta;
        triggerPressedUltraVioleta.action.Enable();
    }

    void OnDisable()
    {
        triggerPressed.action.performed -= OnTriggerPressed;
        triggerPressed.action.Disable();

        triggerPressedUltraVioleta.action.performed -= OnTriggerPressedUltraVioleta;
        triggerPressedUltraVioleta.action.Disable();
    }

   
    void OnTriggerPressed(InputAction.CallbackContext ctx)
    {
        //Linterna normal (Teclado --> F)
        if (flashlightLight == null) return;
        flashlightLight.enabled = !flashlightLight.enabled;
        Debug.Log("Luz normal");
    }

    void OnTriggerPressedUltraVioleta(InputAction.CallbackContext ctx)
    {
        //Linterna Ultra Violeta (Teclado --> V)
        if (flashLightUltraVioleta == null) return;
        flashLightUltraVioleta.enabled = !flashLightUltraVioleta.enabled;
        Debug.Log("Luz ultra violeta");
    }

}
