using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private Giant _giant;
	[SerializeField] private UI _ui;
	[SerializeField] private ManaRandomaizer _manaRandomaizer;
	[SerializeField] private Portal _portal;
	[SerializeField] private float _minHeal = 1f;
	[SerializeField] private float _maxHeal = 100f;

	private void Awake()
	{
		_ui.HealButton.OnClickEvent += HealGiant;
		_ui.HealAllButton.OnClickEvent += HealAllGiant;
		_ui.PortalButton.OnClickEvent += _portal.Appear;
	}

	private void HealGiant()
	{
		var damagedSlots = _giant.Slots.AllDamagedSlots;
		if (damagedSlots.Count > 0)
		{
			int healCount = (int)Mathf.Lerp(_minHeal, _maxHeal, _manaRandomaizer.ManaPercent);
			damagedSlots[Random.Range(0, damagedSlots.Count)].Heal(healCount);
		}
	}

	private void HealAllGiant()
	{
		var damagedSlots = _giant.Slots.AllDamagedSlots;
		if (damagedSlots.Count > 0)
		{
			int healCount = (int)Mathf.Lerp(_minHeal, _maxHeal, _manaRandomaizer.ManaPercent);
			damagedSlots.ForEach(s => s.Heal(healCount));
		}
	}
}
