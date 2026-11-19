using UnityEngine;
using UnityEngine.InputSystem;

public class EquipItem : MonoBehaviour
{
    private InputActionAsset InputActions;
    private InputAction equip;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
       InputActions.FindActionMap("Player").Disable(); 
    }
    private void Awake()
    {
        equip = InputSystem.actions.FindAction("Equip");
    }
}
