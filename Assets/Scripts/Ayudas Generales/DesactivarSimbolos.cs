using UnityEngine;

public class DesactivarSimbolos : MonoBehaviour
{
    [Header("Símbolos")]
    public GameObject simbolosPuzzles;

    [Header("Colliders")]
    public Collider zonaDesactivar;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        simbolosPuzzles.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        simbolosPuzzles.SetActive(true);
    }
}