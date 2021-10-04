using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotFacade : MonoBehaviour
{
	[SerializeField] private Slot _headSlot;
	[SerializeField] private Slot _shoulderLSlot;
	[SerializeField] private Slot _shoulderRSlot;
	[SerializeField] private Slot _forearmLSlot;
	[SerializeField] private Slot _forearmRSlot;
	[SerializeField] private Slot _hipLSlot;
	[SerializeField] private Slot _hipRSlot;
	[SerializeField] private Slot _shinLSlot;
	[SerializeField] private Slot _shinRSlot;
	private List<Slot> _allSlots;

	public Slot Head => _headSlot;
	public Slot ShoulderL => _shoulderLSlot;
	public Slot ShoulderR => _shoulderRSlot;
	public Slot ForearmL => _forearmLSlot;
	public Slot ForearmR => _forearmRSlot;
	public Slot HipL => _hipLSlot; 
	public Slot HipR => _hipRSlot;
	public Slot ShinL => _shinLSlot;
	public Slot ShinR => _shinRSlot;

	public List<Slot> AllSlots 
	{
		get
		{
			if (_allSlots == null)
			{
				_allSlots = new List<Slot>
				{
					_headSlot,
					_shoulderLSlot,
					_shoulderRSlot,
					_forearmLSlot,
					_forearmRSlot,
					_hipLSlot,
					_hipRSlot,
					_shinLSlot,
					_shinRSlot
				};
			}
			return _allSlots;
		}
	}

	public List<Slot> AllFullSlots => AllSlots.Where(s => !s.IsEmpty).ToList();

	public List<Slot> AllEmptySlots => AllSlots.Where(s => s.IsEmpty).ToList();

	public List<Slot> AllDamagedSlots => AllSlots.Where(s => !s.IsEmpty && !s.IsFullHealth).ToList();

	public Slot GetRandomSlot()
	{
		return AllSlots[Random.Range(0, AllSlots.Count)];
	}

	public Slot GetFullRandomSlot()
	{
		var count = AllFullSlots.Count;
		if (count == 0)
		{
			return null;
		}
		return AllFullSlots[Random.Range(0, count)];
	}
}
