using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
	[SerializeField] private HealthPresenter _healthPresenter;
	[SerializeField] private ManaView _manaView;

	public HealthPresenter Health => _healthPresenter;
	public ManaView Mana => _manaView;
}
