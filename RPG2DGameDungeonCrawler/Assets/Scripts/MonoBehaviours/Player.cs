using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Points hitPoints;
    public Points manaPoints;
    public HealthBar healthBarPrefab;
    public ManaBar manaBarPrefab;
    public Inventory inventoryPrefab;
    HealthBar healthBar;
    ManaBar manaBar;
    public Inventory inventory;

    private void OnEnable()
    {
        ResetCharacter();
    }

    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            LevelController.Pause();
            LockControlls = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                print("Kolizja: " + hitObject.name);
                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case ItemType.Coin:
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;
                    case ItemType.Health:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    case ItemType.Mana:
                        shouldDisappear = AdjustManaPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < MaxHitPoints)
        {
            hitPoints.value += amount;
            print("Nowe punkty: " + amount + ". Razem: " + hitPoints.value);

            return true;
        }

        return false;
    }

    public bool AdjustManaPoints(int amount)
    {
        if (manaPoints.value < MaxManaPoints)
        {
            manaPoints.value += amount;
            print("Nowe punkty: " + amount + ". Razem: " + hitPoints.value);

            return true;
        }

        return false;
    }

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        //manaBar = Instantiate(manaBarPrefab);

        healthBar.character = this;
        //manaBar.character = this;

        hitPoints.value = StartingHitPoints;
        manaPoints.value = StartingManaPoints;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());

            hitPoints.value -= damage;
            if (hitPoints.value < float.Epsilon)
            {
                KillCharacter();

                break;
            }

            if (interval > float.Epsilon)
                yield return new WaitForSeconds(interval);
            else
                break;
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        //Destroy(manaBar.gameObject);
        Destroy(inventory.gameObject);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
