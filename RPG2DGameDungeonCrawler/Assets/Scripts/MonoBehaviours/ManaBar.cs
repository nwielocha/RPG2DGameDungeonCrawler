using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
	public Points manaPoints;
	[HideInInspector]
	public Player character;
	public Image meterImage;
	public Text manaText;
	float maxManaPoints;

	void Start()
	{
		maxManaPoints = character.maxManaPoints;
	}

	void Update()
	{
		if (character != null)
		{
			meterImage.fillAmount = manaPoints.value / maxManaPoints;
			manaText.text = "MANA:" + (meterImage.fillAmount * 100);
		}
	}
}
