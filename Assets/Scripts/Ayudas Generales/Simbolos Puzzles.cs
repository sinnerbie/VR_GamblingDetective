using UnityEngine;

public class SimbolosPuzzles : MonoBehaviour
{

    [Header("Símbolos")]
    public GameObject simbolosPuzzles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        simbolosPuzzles.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            simbolosPuzzles.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            simbolosPuzzles.SetActive(false);
    }
}
