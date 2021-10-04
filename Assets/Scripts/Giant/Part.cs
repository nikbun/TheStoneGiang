using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Part : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDamagable
{
	private const int STANDARD_LAYER_ORDER = 100;

	[SerializeField] private Health _health;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Transform _connector;
	private Collider2D _colider;
	private Rigidbody2D _rigidbody;

	public event Action OnDestoyEvent;

	public Transform ConnectorPosition => _connector;

	private void Awake()
	{
		_colider = GetComponent<Collider2D>();
		_rigidbody = GetComponent<Rigidbody2D>();

	}

	private void OnEnable()
	{
		_health.HealthOutEvent += DestroySelf;
	}

	private void OnDisable()
	{
		_health.HealthOutEvent -= DestroySelf;
	}

	public void OnDrag(PointerEventData eventData)
	{
		foreach(var item in eventData.hovered)
		{
			if (item.TryGetComponent<Slot>(out var slot))
			{
				if (slot.CanConnect)
				{
					transform.rotation = item.transform.rotation;
					transform.position = item.transform.position - ConnectorPosition.localPosition.magnitude * transform.localScale.x * transform.up;
					return;
				}
			}
		}
		var newPosition = Camera.main.ScreenToWorldPoint(eventData.position);
		var position = transform.position;
		position.x = newPosition.x;
		position.y = newPosition.y;
		transform.position = position;
	}

	public void SetLayerOrder(int order)
	{
		_spriteRenderer.sortingOrder = order;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Deactivate();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		foreach (var hovered in eventData.hovered)
		{
			if (hovered.TryGetComponent<Slot>(out _))
			{
				return;
			}
		}
		Activate();
	}

	public void Activate()
	{
		SetLayerOrder(STANDARD_LAYER_ORDER);
		_rigidbody.isKinematic = false;
		_colider.enabled = true;

	}

	public void Deactivate()
	{
		_rigidbody.isKinematic = true;
		_colider.enabled = false;
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.angularVelocity = 0f;
	}

	public void GetDamage(int damage)
	{
		_health.GetDamage(damage);
	}

	private void DestroySelf()
	{
		OnDestoyEvent?.Invoke();
		Destroy(this.gameObject);
	}

	public bool CanDamage()
	{
		return true;
	}
}
