using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthPresenter : MonoBehaviour
{
	[SerializeField] private HealthView _healthView;
	[SerializeField] private float _hideTime;
	[SerializeField] private bool _defferedHide;
	private Health _health;
	private float _hideTimer;
	private bool _isHiding;

	private void Update()
	{
		_healthView.UpdatePosition(Mouse.current.position.ReadValue());
		if (_isHiding)
		{
			if (_hideTimer > 0)
			{
				_hideTimer -= Time.deltaTime;
			}
			else
			{
				_isHiding = false;
				HideNow();
			}
		}
	}

	public void Show(int health, int maxHealth)
	{
		_isHiding = false;
		_healthView.SetHealth(health, maxHealth);
		_healthView.Show();
	}

	public void Show(Health health)
	{
		_isHiding = false;
		_health = health;
		_health.HealthUpdatedEvent += UpdateHealth;
		_health.HealthOutEvent += Hide;
		UpdateHealth();
	}

	private void UpdateHealth()
	{
		if (_health != null)
		{
			_healthView.SetHealth(_health.HealthCount, _health.MaxHealthCount);
			_healthView.Show();
		}
		else
		{
			Hide();
		}
	}

	public void Hide()
	{
		if (_defferedHide)
		{
			HideDeffered();
		}
		else
		{
			HideNow();
		}
	}

	private void HideNow()
	{
		if (_health != null)
		{
			_health.HealthUpdatedEvent -= UpdateHealth;
			_health.HealthOutEvent -= Hide;
			_health = null;
		}
		_healthView.Hide();
	}

	private void HideDeffered()
	{
		_hideTimer = _hideTime;
		_isHiding = true;
	}
}
