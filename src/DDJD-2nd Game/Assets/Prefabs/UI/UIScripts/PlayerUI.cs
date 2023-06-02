using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    public InventoryUI inventoryUI;

    [SerializeField]
    public GameObject menuUI,
        missionsUI,
        playingUI;

    [SerializeField]
    public WheelController leftSpellWheel,
        rightSpellWheel;
}
