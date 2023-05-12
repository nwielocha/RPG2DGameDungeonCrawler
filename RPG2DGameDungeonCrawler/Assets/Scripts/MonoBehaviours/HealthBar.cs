using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Points hitPoints;
	[HideInInspector]
	public Player character;
	public Image[] heartImages;
	public Sprite fullHeart, emptyHeart;
	float maxHitPoints;

	void Start()
	{
		maxHitPoints = character.maxHitPoints;
	}

	void Update()
	{
		if (character != null)
		{
			if (hitPoints.value > maxHitPoints)
				hitPoints.value = maxHitPoints;

			for (int i = 0; i < heartImages.Length; i++)
			{
				if (i < hitPoints.value)
					heartImages[i].sprite = fullHeart;
				else
					heartImages[i].sprite = emptyHeart;
				
				if (i < maxHitPoints)
					heartImages[i].enabled = true;
				else
					heartImages[i].enabled = false;
			}
		}
		
	}
}
