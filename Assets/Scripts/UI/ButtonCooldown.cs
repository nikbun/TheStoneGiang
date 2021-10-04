using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private Image _filler;
	[SerializeField] private float _cooldownTime;
	private float _cooldownTimer;

	private event Action OnClickEvent;

	private void Awake()
	{
		_button.onClick.AddListener(OnClick);
	}

	private void Update()
	{
		if (_cooldownTimer > 0)
		{
			_filler.fillAmount = _cooldownTimer / _cooldownTime;
			_cooldownTimer -= Time.deltaTime;
		}
		else
		{
			Enable();
		}
	}

	private void OnClick()
	{
		OnClickEvent?.Invoke();
		Disable();
		_cooldownTimer = _cooldownTime;
	}

	private void Enable()
	{
		_filler.fillAmount = 0f;
		_button.interactable = true;
	}

	private void Disable()
	{
		_filler.fillAmount = 1f;
		_button.interactable = false;
	}
}
