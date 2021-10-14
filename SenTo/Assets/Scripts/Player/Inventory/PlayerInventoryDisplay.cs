using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventoryDisplay : MonoBehaviour 
{
    public Text coinText;
    public Text keyText;

    void Start()
    {
        coinText.text = "0";
        keyText.text = "0";
    }

    public void onChangeInventory(Dictionary<PickUp.PickUpType, int> inventory)
    {
        int numItems = inventory.Count;

        foreach (var item in inventory)
        {
            int itemTotal = item.Value;

            if (item.Key.ToString() == "Coin")
            {
                coinText.text = itemTotal.ToString();
            }

            if (item.Key.ToString() == "Key")
            {
                keyText.text = itemTotal.ToString();
            }
        }
    }
}
