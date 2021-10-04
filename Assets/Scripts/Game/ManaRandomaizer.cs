using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRandomaizer : MonoBehaviour
{
	[SerializeField] private float _minChangeTime;
	[SerializeField] private float _maxChangeTime;
	[SerializeField] private UI _ui;
	private float _changeTimer;

	public float ManaPercent { get; private set; }

	private void Update()
	{
		if (_changeTimer > 0)
		{
			_changeTimer -= Time.deltaTime;
		}
		else
		{
			UpdateValue();
			_changeTimer = Random.Range(_minChangeTime, _maxChangeTime);
		}
	}

	private void UpdateValue()
	{
		ManaPercent = Random.Range(0.1f, 1f);
		_ui.Mana.SetManaSmooth(ManaPercent);
	}
}
