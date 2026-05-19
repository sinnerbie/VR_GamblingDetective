using UnityEngine;

public class CuboClickHandler : MonoBehaviour
{
    public BlackJackHumano manager;
    public CuboData miCuboData;

    public void Inicializar(BlackJackHumano managerRef, CuboData cuboData)
    {
        manager = managerRef;
        miCuboData = cuboData;
    }

    public void BotonClick()
    {
        manager.SeleccionarCubo(miCuboData);
    }

    void OnMouseDown()
    {
        if (manager != null && miCuboData != null)
        {
            manager.SeleccionarCubo(miCuboData);
        }
    }
}
