using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    public InventoryUI inventoryUI;

    [SerializeField]
    public GameObject menuUI,
        missionsUI,
        activeElementWheel,
        tutorialUI;

    [SerializeField]
    public WheelController leftSpellWheel,
        rightSpellWheel;

    [SerializeField]
    private OptionsController _optionsUI;

    [SerializeField]
    public GameUI playingUI;

    [SerializeField]
    public Dialogue Dialogue;

    [SerializeField]
    public TextMeshProUGUI HelpText;
    private Player _player;

    public Player Player
    {
        get
        {
            if (_player == null)
            {
                //FInd by tag
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            return _player;
        }
    }

    public OptionsController OptionsUI
    {
        get { return _optionsUI; }
    }
}
