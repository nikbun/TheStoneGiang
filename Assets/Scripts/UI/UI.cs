using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
	[SerializeField] private HealthPresenter _healthPresenter;
	[SerializeField] private ManaView _manaView;
	[SerializeField] private ButtonCooldown _buttonCooldownHeal;
	[SerializeField] private ButtonCooldown _buttonCooldownHealAll;
	[SerializeField] private ButtonCooldown _buttonCooldownPortal;

	public HealthPresenter Health => _healthPresenter;
	public ManaView Mana => _manaView;
	public ButtonCooldown HealButton => _buttonCooldownHeal;
	public ButtonCooldown HealAllButton => _buttonCooldownHealAll;
	public ButtonCooldown PortalButton => _buttonCooldownPortal;
}
