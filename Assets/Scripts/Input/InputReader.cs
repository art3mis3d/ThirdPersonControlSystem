using ProjectX;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader", order = 0)]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
	// Assign delegate{} to events to initialize them with an empty delegate
	// so we can skip the null check when we use them

	// Gameplay
	public event UnityAction JumpEvent = delegate { };
	public event UnityAction JumpCanceledEvent = delegate { };
	public event UnityAction LightAttackEvent = delegate { };
	public event UnityAction LightAttackCanceledEvent = delegate { };
	public event UnityAction HeavyAttackEvent = delegate { };
	public event UnityAction HeavyAttackCanceledEvent = delegate { };
	public event UnityAction InteractEvent = delegate { };
	public event UnityAction InventoryActionButtonEvent = delegate { };
	public event UnityAction<Vector2> MoveEvent = delegate { };
	public event UnityAction<Vector2> CameraEvent = delegate { };
	public event UnityAction StartedSprinting = delegate { };
	public event UnityAction StoppedSprinting = delegate { };

	// Shared between menus and dialogues
	public event UnityAction MoveSelectionEvent = delegate { };

	// Menus
	public event UnityAction MenuMouseMoveEvent = delegate { };
	public event UnityAction MenuClickButtonEvent = delegate { };
	public event UnityAction MenuUnpauseEvent = delegate { };
	public event UnityAction MenuPauseEvent = delegate { };
	public event UnityAction MenuCloseEvent = delegate { };
	public event UnityAction OpenInventoryEvent = delegate { }; // Used to bring up the inventory
	public event UnityAction CloseInventoryEvent = delegate { }; // Used to bring up the inventory
	public event UnityAction<float> TabSwitched = delegate { };

	// Cheats (has effect only on the editor)
	public event UnityAction CheatMenuEvent = delegate { };

	private GameInput _gameInput;

	public void OnEnable()
	{
		if (_gameInput == null)
		{
			_gameInput = new GameInput();
			_gameInput?.Gameplay.SetCallbacks(this);
		}
		
		_gameInput.Gameplay.Enable();
	}

	public void OnDisable()
	{
		DisableAllInput();
	}
	
	private void DisableAllInput()
	{
		_gameInput.Gameplay.Disable();
	}

	public void OnLightAttack(InputAction.CallbackContext context)
	{
		
	}
	public void OnAttack(InputAction.CallbackContext context)
	{
		//switch (context.phase)
		//{
		//	case InputActionPhase.Performed:
		//		HeavyAttackEvent.Invoke();
		//		break;
		//	case InputActionPhase.Canceled:
		//		HeavyAttackCanceledEvent.Invoke();
		//		break;
		//}
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				LightAttackEvent.Invoke();
				break;
			case InputActionPhase.Canceled:
				LightAttackCanceledEvent.Invoke();
				break;
		}
	}

	public void OnOpenInventory(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			OpenInventoryEvent.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			InteractEvent.Invoke();
	}

	public void OnClimb(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				JumpEvent.Invoke();
				break;
			case InputActionPhase.Canceled:
				JumpCanceledEvent.Invoke();
				break;
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		MoveEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnSprint(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				StartedSprinting.Invoke();
				break;
			case InputActionPhase.Canceled:
				StoppedSprinting.Invoke();
				break;
		}
	}

	public void OnCamera(InputAction.CallbackContext context)
	{
		CameraEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			MenuPauseEvent.Invoke();
	}
}
