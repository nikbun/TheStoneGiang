using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
	[SerializeField] private GameObject _connector;
	[SerializeField] private Slot _subSlot; // TODO: ѕомен€ть зависимость на родительский слот
	[SerializeField] private int _layerOrder;
	private Part _part;
	private bool _canConnect;

	public bool IsEmpty => _part == null;
	public bool CanConnect => _canConnect;

	public void OnDrop(PointerEventData eventData)
	{
		if (_canConnect && eventData.pointerDrag.TryGetComponent<Part>(out var part))
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
		_part.SetLayerOrder(_layerOrder);
		part.Deactivate();
		var partTransform = part.transform;
		partTransform.SetParent(this.transform);
		partTransform.localPosition = Vector3.zero;
		partTransform.localRotation = Quaternion.identity;
		UpdateConnector();
	}

	public void Unconnect()
	{
		if (!IsEmpty)
		{
			_part.transform.parent = null;
			_part.Activate();
			_part = null;
			_subSlot?.Unconnect();
			UpdateConnector();
		}
	}

	private bool UpdateConnector()
	{
		if (IsEmpty)
		{
			ShowConnector();
			_subSlot?.HideConnector();
		}
		else
		{
			HideConnector();
			_subSlot?.UpdateConnector();
		}
		return _canConnect;
	}

	private void ShowConnector()
	{
		_connector.SetActive(true);
		_canConnect = true;
	}

	private void HideConnector()
	{
		_connector.SetActive(false);
		_canConnect = false;
	}
}
