using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Part : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDamagable, IPointerEnterHandler, IPointerExitHandler
{
	private const int STANDARD_LAYER_ORDER = 100;

	[SerializeField] private Health _health;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Transform _connector;
	[SerializeField] private DamageViewer _damageViewer;
	private Collider2D _colider;
	private Rigidbody2D _rigidbody;
	private UI _ui;

	public event Action OnDestoyEvent;

	public Transform ConnectorPosition => _connector;

	private void Awake()
	{
		_damageViewer.Initialize(_health, _spriteRenderer);
		_colider = GetComponent<Collider2D>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_ui = FindObjectOfType<UI>();

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

	public void OnPointerEnter(PointerEventData eventData)
	{
		_ui.Health.Show(_health);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_ui.Health.Hide();
	}
}

[Serializable]
public class DamageViewer
{
	[SerializeField] private Color _normalColor;
	[SerializeField] private Color _damagedColor;
	private Health _health;
	private SpriteRenderer _spriteRenderer;

	public void Initialize(Health health, SpriteRenderer spriteRenderer)
	{
		_health = health;
		_spriteRenderer = spriteRenderer;
		_health.HealthUpdatedEvent += UpdateColor;
	}

	private void UpdateColor()
	{
		float percent = (float)_health.HealthCount / (float)_health.MaxHealthCount;
		_spriteRenderer.color = Color.Lerp(_damagedColor, _normalColor, percent);
	}
}