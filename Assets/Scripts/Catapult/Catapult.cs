using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
	[SerializeField] private Giant _giant;
	[SerializeField] private Projectile _projectileSample;
	[SerializeField] private Transform _launchPosition;
	[SerializeField] private float _launchFrequency = 10f;

	private float _timer = 0f;


	private void Update()
	{
		if (_timer <= 0)
		{
			LaunchProjectile();
			_timer = _launchFrequency;
		}
		else
		{
			_timer -= Time.deltaTime;
		}
	}

	private void LaunchProjectile()
	{
		var projectile = Instantiate(_projectileSample, _launchPosition.position, Quaternion.identity);
		projectile.LaunchToSlot(_giant.Slots.GetRandomSlot());
	}
}
