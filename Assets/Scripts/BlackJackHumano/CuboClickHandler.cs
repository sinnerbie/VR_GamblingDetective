using UnityEngine;

public class CuboClickHandler : MonoBehaviour
{
    private BlackJackHumano manager;
    private CuboData miCuboData;

    public void Inicializar(BlackJackHumano managerRef, CuboData cuboData)
    {
        manager = managerRef;
        miCuboData = cuboData;
    }

    void OnMouseDown()
    {
        if (manager != null && miCuboData != null)
        {
            manager.SeleccionarCubo(miCuboData);
        }
    }
}
