using UnityEngine;

public class ManaRandomaizer : MonoBehaviour
{
	[SerializeField] private float _minChangeTime;
	[SerializeField] private float _maxChangeTime;
	[SerializeField] private UI _ui;

	private float _changeTimer;
	private float _smoothTimer;
	private float _oldValue;
	private float _newValue;
	
	public float ManaPercent { get; private set; }

	private void Update()
	{
		if (_changeTimer > 0)
		{
			_changeTimer -= Time.deltaTime;
			if (_smoothTimer > 0)
			{
				_smoothTimer -= Time.deltaTime;
				ManaPercent = Mathf.Lerp(_newValue, _oldValue, _smoothTimer);
				_ui.Mana.SetMana(ManaPercent);
			}
		}
		else
		{
			UpdateValue();
			_changeTimer = Random.Range(_minChangeTime, _maxChangeTime);
		}
	}

	private void UpdateValue()
	{
		_oldValue = ManaPercent;
		_newValue = Random.Range(0.1f, 1f);
		_smoothTimer = 1f;
	}
}
