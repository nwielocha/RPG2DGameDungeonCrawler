using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 3;
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    public void Start()
    {
        CreateSlots();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (LevelController.Instance.IsPaused && items[i] != null)
            {
                if (items[i].itemType == ItemType.SpeedPotion)
                {
                    itemImages[i].transform.localScale = new Vector3(0f, 0f, 0f);
                }
                else if (items[i].itemType == ItemType.Coin)
                {
                    itemImages[i].transform.localScale = new Vector3(0f, 0f, 0f);
                }
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.transform.localScale = new Vector3(0f, 0f, 0f);
            }
            else
            {
                if (items[i] != null)
                {
                    if (items[i].itemType == ItemType.SpeedPotion)
                    {
                        itemImages[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    }
                    else if (items[i].itemType == ItemType.Coin)
                    {
                        itemImages[i].transform.localScale = new Vector3(1.5f, 1.5f, 0.0f);
                    }

                    Slot slotScript = slots[i].GetComponent<Slot>();
                    Text quantityText = slotScript.qtyText;
                    quantityText.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                    if (items[i].quantity > 0)
                    {
                        itemImages[i].enabled = true;
                        quantityText.enabled = true;
                        quantityText.text = items[i].quantity.ToString();
                    }
                    else
                    {
                        quantityText.enabled = false;
                        itemImages[i].enabled = false;
                    }
                }
            }
        }
    }

    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform); // obiekt podrzedny: InventoryBackground
                slots[i] = newSlot;
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.transform.position += new Vector3(25f, 0f, 0f);
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>(); // obiekt podrzedny: ItemImage
            }
        }
    }

    public int PlayerGold()
    {
        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.itemType == ItemType.Coin)
                {
                    return item.quantity;
                }
            }
        }
        return 0;
    }

    public void RemoveGold(int amount)
    {
        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.itemType == ItemType.Coin)
                {
                    Debug.Log(amount);
                    Debug.Log(item.quantity);

                    item.quantity -= amount;
                }
            }
        }
    }

    public bool RemoveSpeedPotion(int amount)
    {
        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.itemType == ItemType.SpeedPotion && item.quantity > 0)
                {
                    item.quantity -= amount;

                    return true;
                }
            }
        }

        return false;
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (
                items[i] != null
                && items[i].itemType == itemToAdd.itemType
                && itemToAdd.stackable == true
            )
            {
                // Dodanie rzeczy do istniejacego pojemnikaitems
                items[i].quantity += 1;
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();

                return true;
            }

            if (items[i] == null)
            {
                // Dodanie rzeczy do pustego pojemnika
                // Skopowiowanie rzeczy i dodanie do inwentarza
                items[i] = Instantiate(itemToAdd);
                items[i].quantity = 1;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;

                return true;
            }
        }

        return false;
    }
}
