using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksForPlaying : MonoBehaviour
{
	[SerializeField] private Giant _giant;
	[SerializeField] private GameObject _thanks;

	private void Update()
	{
		if (_giant.transform.position.x > transform.position.x)
		{
			_thanks.SetActive(true);
		}
	}
}
