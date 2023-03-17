using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour, InputActions.IGameplayActions
{
    InputActions m_input;
    //public event UnityAction OnJumpEvent, OnRollEvent, OnStopRollEvent;

    public float jumpTime;
    [SerializeField] float earlyJumpForgiveness = 0.1f;

    // Input state
    public bool jumpPressed
    {
        get
        {
            return jumpTime + earlyJumpForgiveness - Time.time > 0;
        }
    }
    public bool rollPressed;

    private void OnEnable()
    {
        m_input = new InputActions();

        // 对所有的动作表注册回调函数
        m_input.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        m_input.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        m_input.Gameplay.Enable();

        // hide cursor and lock
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpTime = Time.time;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
            rollPressed = true;
        if (context.canceled)
            rollPressed = false;
    }
}
