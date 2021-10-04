using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler, IDamagable
{
	[SerializeField] private GameObject _connector;
	[SerializeField] private Slot _parentSlot;
	[SerializeField] private int _layerOrder;
	private Part _part;

	private event Action UnconnectEvent;
	private event Action ConnectEvent;

	public bool IsEmpty => _part == null;
	public bool CanConnect => IsEmpty && _connector.activeSelf;

	private void OnEnable()
	{
		if (_parentSlot != null)
		{
			_parentSlot.UnconnectEvent += Unconnect;
			_parentSlot.ConnectEvent += () => UpdateConnector();
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (CanConnect && eventData.pointerDrag.TryGetComponent<Part>(out var part))
		{
			Connect(part);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Unconnect();
		}
	}

	public void Connect(Part part)
	{
		_part = part;
		_part.OnDestoyEvent += ClearPart;
		_part.SetLayerOrder(_layerOrder);
		_part.Deactivate();
		var partTransform = _part.transform;
		partTransform.SetParent(this.transform);
		partTransform.localRotation = Quaternion.identity;
		partTransform.localPosition = _part.ConnectorPosition.localPosition * -1;
		UpdateConnector();
		ConnectEvent?.Invoke();
	}

	public void Unconnect()
	{
		if (!IsEmpty)
		{
			_part.OnDestoyEvent -= ClearPart;
			_part.transform.parent = null;
			_part.Activate();
			_part = null;
			UpdateConnector();
			UnconnectEvent?.Invoke();
		}
		else
		{
			UpdateConnector();
		}
	}

	private void ClearPart()
	{
		_part = null;
		UpdateConnector();
		UnconnectEvent?.Invoke();
	}

	private bool UpdateConnector()
	{
		if (_parentSlot != null && _parentSlot.IsEmpty)
		{
			_connector.SetActive(false);
		}
		else
		{
			_connector.SetActive(true);
		}
		return _connector.activeSelf;
	}

	public void GetDamage(int damage)
	{
		_part.GetDamage(damage);
	}

	public bool CanDamage()
	{
		return !IsEmpty;
	}
}
