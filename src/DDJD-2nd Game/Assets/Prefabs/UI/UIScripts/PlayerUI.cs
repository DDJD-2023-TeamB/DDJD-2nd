using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    public InventoryUI inventoryUI;

    [SerializeField]
    public GameObject menuUI,
        missionsUI;

    [SerializeField]
    public WheelController leftSpellWheel,
        rightSpellWheel;

    [SerializeField]
    public GameUI playingUI;
}
