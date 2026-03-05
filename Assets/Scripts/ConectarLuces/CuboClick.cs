using UnityEngine;

public class CuboClick : MonoBehaviour
{
    public ConectarLuces manager;
    public int index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        manager.Pulsar(index);
        Debug.Log("clic");
    }
}
