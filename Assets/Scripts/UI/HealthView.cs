using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HealthView : MonoBehaviour
{
	[SerializeField] private GameObject _vision;
	[SerializeField] private Text _text;
	private RectTransform _rectTransform;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	}

	public void Show()
	{
		_vision.SetActive(true);
	}

	public void Hide()
	{
		_vision.SetActive(false);
	}

	public void SetHealth(int health, int maxHealth)
	{
		_text.text = $"{health}/{maxHealth}";
	}

	public void UpdatePosition(Vector2 position)
	{
		_rectTransform.position = position;
	}
}
