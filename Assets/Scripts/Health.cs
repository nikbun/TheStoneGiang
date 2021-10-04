using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[Min(1)]
	[SerializeField] private int _maxHealth = 100;
	private int _health;

	public event Action HealthUpdatedEvent;
	public event Action HealthOutEvent;

	public int MaxHealthCount => _maxHealth;
	public int HealthCount => _health;

	private void Awake()
	{
		_health = _maxHealth;
	}

	public void GetDamage(int damage)
	{
		if (damage < 0)
		{
			throw new Exception($"Damage cannot be negative. Current damage - {damage}");
		}
		_health = Mathf.Max(0, _health - damage);
		if (_health == 0)
		{
			HealthOutEvent?.Invoke();
		}
		HealthUpdatedEvent?.Invoke();
	}

	public void GetHeal(int healing)
	{
		if (healing < 0)
		{
			throw new Exception($"Healing cannot be negative. Current healing - {healing}");
		}
		_health = Mathf.Min(_health + healing, _maxHealth);
		HealthUpdatedEvent?.Invoke();
	}
}
