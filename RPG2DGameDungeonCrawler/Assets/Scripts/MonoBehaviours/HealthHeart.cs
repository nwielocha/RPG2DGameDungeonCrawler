using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
	public Sprite fullHeart, halfHeart, emptyHeart;
	Image heartImage;

	public void Awake()
	{
		heartImage = GetComponent<Image>();
	}

	public void SetHeartImage(HeartStatus heartStatus)
	{
		switch (heartStatus)
		{
			case HeartStatus.EMPTY:
				heartImage.sprite = emptyHeart;
				break;
			case HeartStatus.HALF:
				heartImage.sprite = halfHeart;
				break;
			case HeartStatus.FULL:
				heartImage.sprite = fullHeart;
				break;
			default:
				break;
		}
	}
}
public enum HeartStatus
{
	EMPTY = 0,
	HALF = 1,
	FULL = 2
}
