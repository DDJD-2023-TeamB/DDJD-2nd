using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Slider _leftManaBar;

    [SerializeField]
    private Slider _rightManaBar;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _healthBar.value = (float)currentHealth / (float)maxHealth;
    }

    public void UpdateMana(int currentMana, int maxMana, bool isLeft)
    {
        if (isLeft)
        {
            _leftManaBar.value = (float)currentMana / (float)maxMana;
        }
        else
        {
            _rightManaBar.value = (float)currentMana / (float)maxMana;
        }
    }
}
