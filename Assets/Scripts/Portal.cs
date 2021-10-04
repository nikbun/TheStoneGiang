using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private GameObject _visual;
	[SerializeField] private int _minHealthPart = 1;
	[SerializeField] private int _maxHealthPart = 100;
	[SerializeField] private float _appearTime = 1f;
	[SerializeField] private float _openSize = 1f;
	[SerializeField] private float _closedSize = 0f;
	[SerializeField] private ManaRandomaizer _manaRandomaizer;
	[SerializeField] private List<Part> _slotsPrefabs;
	private float _appearTimer;
	private bool _isAppering;

	public void Update()
	{
		if (_isAppering)
		{
			if (_appearTimer > 0)
			{
				_appearTimer -= Time.deltaTime;
				_visual.transform.localScale = Vector3.one * Mathf.Lerp(_openSize, _closedSize, _appearTimer / _appearTime);
			}
			else
			{
				_isAppering = false;
				DropPart();
				_appearTimer = _appearTime;
			}
		}
		else
		{
			if (_appearTimer > 0)
			{
				_appearTimer -= Time.deltaTime;
				_visual.transform.localScale = Vector3.one * Mathf.Lerp(_closedSize, _openSize, _appearTimer / _appearTime);
			}
			else
			{
				_visual.SetActive(false);
			}
		}
	}

	public void Appear()
	{
		_visual.SetActive(true);
		_appearTimer = _appearTime;
		_isAppering = true;

	}

	private void DropPart()
	{
		var partPrefab = _slotsPrefabs[Random.Range(0, _slotsPrefabs.Count)];
		var part = Instantiate(partPrefab, transform.position, Quaternion.identity);
		part.Initialize((int)Mathf.Lerp(_minHealthPart, _maxHealthPart, _manaRandomaizer.ManaPercent));
	}
}
