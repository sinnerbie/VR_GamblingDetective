//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class CajonFalso : MonoBehaviour
{

    public Animator anim;
    public string boolName = "isOpen";

    public InputActionReference triggerAbrir;

    bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        triggerAbrir.action.performed += OnTriggerPressedAbrir;
        triggerAbrir.action.Enable();

    }

    void OnDisable()
    {
        triggerAbrir.action.performed -= OnTriggerPressedAbrir;
        triggerAbrir.action.Disable();

        
    }

    void OnTriggerPressedAbrir(InputAction.CallbackContext ctx)
    {
        Debug.Log("armario");
        isOpen = !isOpen;
        anim.SetBool(boolName, isOpen);
    }


    
}
