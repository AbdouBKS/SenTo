using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public enum PickUpType
    {
        Coin, Key
    }

    public PickUpType type;
}
