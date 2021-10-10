using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickUpType
    {
        Coin, Key
    }

    public PickUpType type;
}
