using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Points hitPoints;
	[HideInInspector]
	public Player character;
	public Image[] heartsImage;
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

			for (int i = 0; i < heartsImage.Length; i++)
			{
				if (i < hitPoints.value)
					heartsImage[i].sprite = fullHeart;
				else
					heartsImage[i].sprite = emptyHeart;
				
				if (i < maxHitPoints)
					heartsImage[i].enabled = true;
				else
					heartsImage[i].enabled = false;
			}
		}
		
	}
}
