using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler, IDamagable, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject _connector;
	[SerializeField] private Slot _parentSlot;
	[SerializeField] private int _layerOrder;
	[Range(0f,1f)]
	[SerializeField] private float _percentOfFallPartAtDamage = 0.2f;
	private Part _part;

	private event Action UnconnectEvent;
	private event Action ConnectEvent;

	public bool IsFullHealth => _part.IsFullHealth;
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

	public void Heal(int heal)
	{
		_part.Heal(heal);
	}

	public void Damage(int damage)
	{
		_part.Damage(damage);
		if (_percentOfFallPartAtDamage >= UnityEngine.Random.Range(0f, 1f))
		{
			Unconnect();
		}
	}

	public bool CanDamage()
	{
		return !IsEmpty;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!IsEmpty && !eventData.dragging)
		{
			_part.OnPointerEnter(eventData);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!IsEmpty)
		{
			_part.OnPointerExit(eventData);
		}
	}
}
