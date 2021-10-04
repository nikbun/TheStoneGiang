using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPartsConnector : MonoBehaviour
{
	[SerializeField] private SlotFacade _slotFacade;
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
		_slotFacade.Head.Connect(Instantiate(_headPrefab));
		_slotFacade.ShoulderL.Connect(Instantiate(_shoulderLPrefab));
		_slotFacade.ShoulderR.Connect(Instantiate(_shoulderRPrefab));
		_slotFacade.ForearmL.Connect(Instantiate(_forearmLPrefab));
		_slotFacade.ForearmR.Connect(Instantiate(_forearmRPrefab));
		_slotFacade.HipL.Connect(Instantiate(_hipLPrefab));
		_slotFacade.HipR.Connect(Instantiate(_hipRPrefab));
		_slotFacade.ShinL.Connect(Instantiate(_shinLPrefab));
		_slotFacade.ShinR.Connect(Instantiate(_shinRPrefab));
	}
}
