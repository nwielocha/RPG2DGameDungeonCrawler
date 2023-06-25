using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemController : MonoBehaviour
{
    private Inventory _playerInventory;
    public GameObject Item;
    public string Name;
    public int Price;
    public GameObject Player;
    public TMP_Text TextComponent;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        _playerInventory = Player.GetComponent<Player>().inventory;
        TextComponent.text = "<uppercase>" + Name + "\n(" + Price.ToString() + ")</uppercase>";
    }

    void Update()
    {
        Color newColor;
        int playerGold = _playerInventory.PlayerGold();

        if (playerGold < Price)
        {
            ColorUtility.TryParseHtmlString("#828282", out newColor);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#ffffff", out newColor);
        }

        TextComponent.color = newColor;
    }

    public void BuyItem()
    {
        int playerGold = _playerInventory.PlayerGold();

        if (playerGold >= Price)
        {
            _playerInventory.RemoveGold(Price);
            Instantiate(Item, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
