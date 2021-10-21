using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerInventoryDisplay playerInventoryDisplay;
    private Dictionary<PickUp.PickUpType, int> items = new Dictionary<PickUp.PickUpType, int>();

    private SpriteRenderer spriteRDoor;
    public GameObject door;
    public Sprite openDoorSprite;

    void Start()
    {
        playerInventoryDisplay = GetComponent<PlayerInventoryDisplay>();
        spriteRDoor = door.GetComponent<SpriteRenderer>();
    }

    public void Add(PickUp pickup)
    {
        PickUp.PickUpType type = pickup.type;

        int oldTotal = 0;

        if (items.TryGetValue(type, out oldTotal))
            items[type] = oldTotal + 1;
        else
            items.Add(type, 1);

        playerInventoryDisplay.onChangeInventory(items);
        checkDoor(items);
    }

    void checkDoor(Dictionary<PickUp.PickUpType, int> inventory)
    {
        foreach (var item in inventory)
        {
            if (item.Key.ToString() == "Key" && item.Value > 0)
            {
                spriteRDoor.sprite = openDoorSprite;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Pickup"))
        {
            PickUp item = hit.GetComponent<PickUp>();
            Add(item);
            Destroy(hit.gameObject);
        }
    }

}
