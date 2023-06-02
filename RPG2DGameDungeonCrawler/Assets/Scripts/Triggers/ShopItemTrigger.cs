using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTrigger : MonoBehaviour
{
    private ShopItemController _controller;
    void Start()
    {
        _controller = gameObject.GetComponent<ShopItemController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _controller.BuyItem();
        }
    }

}
