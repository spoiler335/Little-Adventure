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
}
