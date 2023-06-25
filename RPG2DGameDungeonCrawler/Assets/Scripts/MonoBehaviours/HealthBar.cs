using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Points hitPoints;

    [HideInInspector]
    public Player character;
    public Image[] heartImages;
    public Sprite fullHeart,
        emptyHeart;
    private float _maxHitPoints;

    void Start()
    {
        _maxHitPoints = character.MaxHitPoints;
    }

    void Update()
    {
        if (LevelController.Instance.IsPaused)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }

        if (character != null)
        {
            if (hitPoints.value > _maxHitPoints)
                hitPoints.value = _maxHitPoints;

            for (int i = 0; i < heartImages.Length; i++)
            {
                if (i < hitPoints.value)
                    heartImages[i].sprite = fullHeart;
                else
                    heartImages[i].sprite = emptyHeart;

                if (i < _maxHitPoints)
                    heartImages[i].enabled = true;
                else
                    heartImages[i].enabled = false;
            }
        }
    }
}
