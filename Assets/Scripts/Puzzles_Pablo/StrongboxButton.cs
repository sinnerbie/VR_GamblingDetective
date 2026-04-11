using UnityEngine;

public class StrongboxButton : MonoBehaviour
{
    [SerializeField] private Strongbox strongbox;
    [SerializeField] private int numero;

    public void Pulsar()
    {
        if (strongbox == null)
        {
            Debug.LogWarning($"No hay Strongbox asignado en {name}");
            return;
        }

        Debug.Log("numero");

        strongbox.PulsarNumero(numero);
    }
}
