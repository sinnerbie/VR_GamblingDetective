using UnityEngine;
using UnityEngine.InputSystem;

public class Linterna : MonoBehaviour
{
    public Light flashlightLight;
    public InputActionReference triggerPressed;

    public Light flashLightUltraVioleta;
    public InputActionReference triggerPressedUltraVioleta;

    //private bool estaEncendido = false;

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
        if (flashlightLight == null || flashLightUltraVioleta == null) return;

        // Si la UV está encendida, F apaga TODO
        if (flashLightUltraVioleta.enabled)
        {
            flashLightUltraVioleta.enabled = false;
            flashlightLight.enabled = false;
            flashlightLight.color = Color.white;
            Debug.Log("Apagadas normal y UV");
            return;
        }

        // Toggle de la luz normal
        flashlightLight.enabled = !flashlightLight.enabled;

        if (flashlightLight.enabled)
        {
            flashlightLight.color = Color.white;
            Debug.Log("Luz normal encendida");
        }
        else
        {
            Debug.Log("Luz normal apagada");
        }
    }

    void OnTriggerPressedUltraVioleta(InputAction.CallbackContext ctx)
    {
        if (flashLightUltraVioleta == null || flashlightLight == null) return;

        // La UV solo funciona si la normal está encendida
        if (!flashlightLight.enabled)
            return;

        flashLightUltraVioleta.enabled = !flashLightUltraVioleta.enabled;

        if (flashLightUltraVioleta.enabled)
        {
            flashlightLight.color = Color.purple;
            Debug.Log("UV encendida");
        }
        else
        {
            flashlightLight.color = Color.white;
            Debug.Log("UV apagada");
        }
    }




    /*
    void OnTriggerPressed(InputAction.CallbackContext ctx)
    {
        //Linterna normal (Teclado --> F)
       
        if (flashlightLight == null) return;
        flashlightLight.enabled = !flashlightLight.enabled;
        estaEncendido = true;
        Debug.Log("Luz normal");
        if (!flashlightLight.enabled)
        {

            estaEncendido = false;
            
        }
    }
    */

    /*
    void OnTriggerPressedUltraVioleta(InputAction.CallbackContext ctx)
    {
        //Linterna Ultra Violeta (Teclado --> V)
        if (estaEncendido == true)
        {
            if (flashLightUltraVioleta == null) return;
            flashLightUltraVioleta.enabled = !flashLightUltraVioleta.enabled;
            flashlightLight.color = Color.purple;
            
                Debug.Log("Luz ultra violeta");
        }

        if (!flashLightUltraVioleta.enabled)
        {
            flashlightLight.color = Color.white;
        }

    }
    */


}
