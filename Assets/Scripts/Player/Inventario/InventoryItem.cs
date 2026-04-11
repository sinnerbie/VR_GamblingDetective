using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public bool stashed = false;
    public bool used = false;
    public bool[] uses;
    public enum Item { Key, Miscelaneous }
    public Item itemType;
    public Transform inventoryPos;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        rb.isKinematic = false;
    }

    public void OnLetGo()
    {
        if (stashed)
        {
            rb.isKinematic = true;
            Invoke("ReturnToPosition", 0.15f);
        }
        else
        {
            transform.SetParent(null);
            rb.isKinematic = false;
        }
    }
    
    void ReturnToPosition()
    {
        transform.SetParent(inventoryPos);
        transform.position = inventoryPos.position;
        transform.rotation = Quaternion.identity;
    }
}
