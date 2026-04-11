using UnityEngine;

public class InventorySnap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<InventoryItem>() != null)
            GetComponentInParent<InventorySystem>().SnapItem(other.GetComponent<InventoryItem>()); 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InventoryItem>() != null)
            GetComponentInParent<InventorySystem>().ReleaseItem(other.GetComponent<InventoryItem>());
    }
}
