using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventoryDisplay : MonoBehaviour 
{
    public Image iconCoin;
    public Image iconKey;

    public void onChangeInventory(Dictionary<PickUp.PickUpType, int> inventory)
    {

        int numItems = inventory.Count;

        foreach (var item in inventory)
        {
            int itemTotal = item.Value;
            float newWidth = 12 * itemTotal;

            if (item.Key.ToString() == "Coin")
                iconCoin.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            if (item.Key.ToString() == "Key")
                iconKey.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

        }
    }
}
