using UnityEngine;
using UnityEngine.UI;

public class ManaView : MonoBehaviour
{
	[SerializeField] private GameObject _vision;
	[SerializeField] private Image _image;

	public void Show()
	{
		_vision.SetActive(true);
	}

	public void Hide()
	{
		_vision.SetActive(false);
	}

	public void SetMana(float percent)
	{
		_image.fillAmount = Mathf.Clamp(percent, 0f, 1f);
	}
}
