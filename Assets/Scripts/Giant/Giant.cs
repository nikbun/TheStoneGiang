using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Giant : MonoBehaviour
{
	private const string IS_MOVE_FORWARD = "IsMoveForward";
	private const string SCENE_NAME = "SampleScene";

	[SerializeField] private SlotFacade _slotFacade;
	[SerializeField] private Animator _animator;

	public SlotFacade Slots => _slotFacade;

	private void Update()
	{
		var keyboard = Keyboard.current;
		MoveForward();
		RealoadLevel(keyboard);
	}

	private void RealoadLevel(Keyboard keyboard)
	{
		if (keyboard.rKey.wasPressedThisFrame)
		{
			SceneManager.LoadScene(SCENE_NAME);
		}
	}

	private void MoveForward()
	{
		_animator.SetBool(IS_MOVE_FORWARD, !HasEmptySlots());
	}

	private bool HasEmptySlots()
	{
		return _slotFacade.AllEmptySlots.Count != 0;
	}
}
