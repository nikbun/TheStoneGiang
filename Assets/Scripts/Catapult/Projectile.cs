using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private int _damageCount = 25;
	[SerializeField] private float _destroyTime = 10;
	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		Destroy(this.gameObject, _destroyTime);
	}

	public void Launch(Vector2 target)
	{
		var lowAngle = Vector3.zero;
		int force = 0;
		do
		{
			force++;
			fts.solve_ballistic_arc((Vector2)transform.position, force, target, Physics2D.gravity.magnitude, out lowAngle, out _);
		} while (lowAngle.magnitude == 0 && force < 1000);

		_rigidbody.AddForce(lowAngle, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IDamagable>(out var damagable) && damagable.CanDamage())
		{
			damagable.GetDamage(_damageCount);
			Destroy(this.gameObject);
		}
	}
}
