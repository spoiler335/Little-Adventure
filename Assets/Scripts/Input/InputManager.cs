using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private PlayerControls inputActions;
    public InputManager()
    {
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    public float GetForward() => inputActions.Player.Movement.ReadValue<Vector2>().x;
    public float GetRight() => inputActions.Player.Movement.ReadValue<Vector2>().y;

    public bool IsAttackClicked() => inputActions.Player.Attack.WasPressedThisFrame();
}
