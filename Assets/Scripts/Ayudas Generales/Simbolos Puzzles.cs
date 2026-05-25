using UnityEngine;

public class SimbolosPuzzles : MonoBehaviour
{
    [Header("Símbolos")]
    public GameObject simbolosPuzzles;

    void Start()
    {
        simbolosPuzzles.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name == "ZonaOcultar")
                return;

            simbolosPuzzles.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name == "ZonaOcultar")
                return;

            simbolosPuzzles.SetActive(false);
        }
    }
}