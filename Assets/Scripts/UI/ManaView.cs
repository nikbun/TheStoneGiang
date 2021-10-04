using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaView : MonoBehaviour
{
	[SerializeField] private float _smoothTime = 0.1f;
	[SerializeField] private GameObject _vision;
	[SerializeField] private Image _image;
	private float _smoothTimer;
	private float _oldValue;
	private float _newValue;

	private void Update()
	{
		if (_smoothTimer > 0)
		{
			_smoothTimer -= Time.deltaTime;
			_image.fillAmount = Mathf.Lerp(_newValue, _oldValue, _smoothTimer);
		}
	}

	public void Show()
	{
		_vision.SetActive(true);
	}

	public void Hide()
	{
		_vision.SetActive(false);
	}

	public void SetMana(float percent)
	{
		_image.fillAmount = Mathf.Clamp(percent, 0f, 1f);
	}

	public void SetManaSmooth(float percent)
	{
		_oldValue = _image.fillAmount;
		_newValue = percent;
		_smoothTimer = _smoothTime;
	}
}
