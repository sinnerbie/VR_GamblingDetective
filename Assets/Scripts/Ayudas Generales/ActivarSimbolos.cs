using UnityEngine;

public class ActivarSimbolos : MonoBehaviour
{
    [Header("Símbolos")]
    public GameObject simbolosPuzzles;

    [Header("Colliders")]
    public Collider zonaActivar;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        simbolosPuzzles.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        simbolosPuzzles.SetActive(false);
    }
}