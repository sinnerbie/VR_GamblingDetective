using UnityEngine;
using UnityEngine.EventSystems;

public class GestorDeRaycaster : MonoBehaviour
{
    void Start()
    {
        // Este script solo es informativo, no es necesario si configuras todo manualmente
        Debug.Log("=== VERIFICACIÓN DE CONFIGURACIÓN PARA CLICS ===");

        // Verificar cámara principal
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("No hay cámara con tag 'MainCamera' en la escena!");
            return;
        }

        // Verificar Physics Raycaster
        PhysicsRaycaster raycaster = mainCam.GetComponent<PhysicsRaycaster>();
        if (raycaster == null)
        {
            Debug.LogWarning("La cámara principal NO tiene PhysicsRaycaster. Los clics NO funcionarán!");
            Debug.Log("SOLUCIÓN: Selecciona la cámara principal -> Add Component -> Physics Raycaster");
        }
        else
        {
            Debug.Log("? PhysicsRaycaster encontrado en la cámara principal");
        }

        Debug.Log("=== FIN VERIFICACIÓN ===");
    }
}