using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
	[SerializeField] private HealthPresenter _healthPresenter;

	public HealthPresenter Health => _healthPresenter;
}
