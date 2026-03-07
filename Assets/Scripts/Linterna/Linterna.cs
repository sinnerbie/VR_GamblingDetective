using UnityEngine;
using UnityEngine.InputSystem;

public class Linterna : MonoBehaviour
{
    public Light flashlightLight;
    public InputActionReference triggerPressed;

    public Light flashLightUltraVioleta;
    public InputActionReference triggerPressedUltraVioleta;

    private int estadoLinterna = 0; // 0 = luz apagada, 1 = luz normal, 2 = luz UV

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

    void LuzNormalEncendida()
    {
       //Encendemos luz normal
       flashlightLight.enabled = true;
       flashlightLight.color = Color.white;

       //Aseguramos que la luz UV esté apagada
       flashLightUltraVioleta.enabled = false;
    }

    void LuzNormalApagada()
    {
        //Apagamos luz normal
        flashlightLight.enabled = false;
    }

    void LuzUVEncendida()
    {
        //Encendemos luz UV
        flashLightUltraVioleta.enabled = true;
        flashLightUltraVioleta.color = new Color(0.5f, 0f, 1f); // Color morado para UV

        //Aseguramos que la luz normal esté apagada
        flashlightLight.enabled = false;
    }

    void LuzUVApagada()
    {
        //Apagamos luz UV
        flashLightUltraVioleta.enabled = false;
    }

    //Metodo para manejar la Luz normal
    void OnTriggerPressed(InputAction.CallbackContext ctx)
    {
        switch (estadoLinterna)
        {
            case 0:
                LuzNormalEncendida();
                estadoLinterna = 1;
                break;
            case 1:
                LuzNormalApagada();
                estadoLinterna = 0;
                break;
            case 2:
                LuzUVApagada();
                //LuzNormalEncendida();
                estadoLinterna = 0;
                break;
            default:
                Debug.LogError("Estado de linterna desconocido: " + estadoLinterna);
                break;
        }
    }

    //Metodo para manejar la Luz UV
    void OnTriggerPressedUltraVioleta(InputAction.CallbackContext ctx)
    {
        switch (estadoLinterna)
        {
            case 0:
                Debug.Log("No se puede encender la UV si la luz normal está apagada");
                break;
            case 1:
                LuzNormalApagada();
                LuzUVEncendida();
                estadoLinterna = 2;
                break;
            case 2:
                LuzUVApagada();
                LuzNormalEncendida();
                estadoLinterna = 1;
                break;
            default:
                Debug.LogError("Estado de linterna desconocido: " + estadoLinterna);
                break;
        }

    }

    //Codigo funcional pero sucio, lo dejo comentado por si se quiere revisar o usar como referencia, pero el codigo de arriba es mas limpio y organizado

    /*
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
    */


    /*
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
    */
}
