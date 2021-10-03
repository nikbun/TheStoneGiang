using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPartsConnector : MonoBehaviour
{
	// TODO: Выделить отдельный класс для работы со ссылками на все слоты
	[SerializeField] private Slot _headSlot;
	[SerializeField] private Slot _shoulderLSlot;
	[SerializeField] private Slot _shoulderRSlot;
	[SerializeField] private Slot _forearmLSlot;
	[SerializeField] private Slot _forearmRSlot;
	[SerializeField] private Slot _hipLSlot;
	[SerializeField] private Slot _hipRSlot;
	[SerializeField] private Slot _shinLSlot;
	[SerializeField] private Slot _shinRSlot;
	[Space]
	[SerializeField] private Part _headPrefab;
	[SerializeField] private Part _shoulderLPrefab;
	[SerializeField] private Part _shoulderRPrefab;
	[SerializeField] private Part _forearmLPrefab;
	[SerializeField] private Part _forearmRPrefab;
	[SerializeField] private Part _hipLPrefab;
	[SerializeField] private Part _hipRPrefab;
	[SerializeField] private Part _shinLPrefab;
	[SerializeField] private Part _shinRPrefab;

	private void Awake()
	{
		_headSlot.Connect(Instantiate(_headPrefab));
		_shoulderLSlot.Connect(Instantiate(_shoulderLPrefab));
		_shoulderRSlot.Connect(Instantiate(_shoulderRPrefab));
		_forearmLSlot.Connect(Instantiate(_forearmLPrefab));
		_forearmRSlot.Connect(Instantiate(_forearmRPrefab));
		_hipLSlot.Connect(Instantiate(_hipLPrefab));
		_hipRSlot.Connect(Instantiate(_hipRPrefab));
		_shinLSlot.Connect(Instantiate(_shinLPrefab));
		_shinRSlot.Connect(Instantiate(_shinRPrefab));
	}
}
