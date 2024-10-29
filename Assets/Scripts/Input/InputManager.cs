using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls inputActions;
    public InputManager()
    {
        inputActions = new PlayerControls();
        inputActions.Enable();

        inputActions.Player.Pause.performed += OnPauseClicked;
    }

    private void OnPauseClicked(InputAction.CallbackContext context)
    {
        EventsModel.ON_PAUSE_CLICKED?.Invoke();
    }

    public float GetForward() => inputActions.Player.Movement.ReadValue<Vector2>().x;
    public float GetRight() => inputActions.Player.Movement.ReadValue<Vector2>().y;

    public bool IsAttackClicked() => inputActions.Player.Attack.WasPressedThisFrame();
    public bool IsSlideClicked() => inputActions.Player.Slide.WasPressedThisFrame();
}
