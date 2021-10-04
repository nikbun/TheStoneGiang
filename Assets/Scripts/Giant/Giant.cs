using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Giant : MonoBehaviour
{
	private const string IS_MOVE_FORWARD = "IsMoveForward";

	[SerializeField] private SlotFacade _slotFacade;
	[SerializeField] private Animator _animator;

	public SlotFacade Slots => _slotFacade;

	private void Update()
	{
		var keyboard = Keyboard.current;
		MoveForward(keyboard);
	}

	private void MoveForward(Keyboard keyboard)
	{
		if (keyboard.dKey.wasPressedThisFrame)
		{
			_animator.SetBool(IS_MOVE_FORWARD, true);
		}
		else if (keyboard.dKey.wasReleasedThisFrame)
		{
			_animator.SetBool(IS_MOVE_FORWARD, false);
		}
	}
}
