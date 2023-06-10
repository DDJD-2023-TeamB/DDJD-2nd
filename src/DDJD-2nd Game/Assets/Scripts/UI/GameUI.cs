using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private ManaBarController _leftManaBar;

    [SerializeField]
    private ManaBarController _rightManaBar;

    private Element _leftElement;
    private Element _rightElement;

    [SerializeField]
    private SerializedDictionary<Element, ElementUI> _elementsUI =
        new SerializedDictionary<Element, ElementUI>();

    public void UpdateElements(Skill leftSkill, Skill rightSkill, Element mainElement)
    {
        if (leftSkill.Element != _leftElement)
        {
            _leftElement = leftSkill.Element;
            _leftManaBar.ChangeManaBar(_elementsUI[_leftElement]);
        }
        if (rightSkill.Element != _rightElement)
        {
            _rightElement = rightSkill.Element;
            _rightManaBar.ChangeManaBar(_elementsUI[_rightElement]);
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _healthBar.value = (float)currentHealth / (float)maxHealth;
    }

    public void UpdateMana(Element element, int currentMana, int maxMana)
    {
        if (element == _leftElement)
        {
            _leftManaBar.Value = (float)currentMana / (float)maxMana;
        }
        if (element == _rightElement)
        {
            _rightManaBar.Value = (float)currentMana / (float)maxMana;
        }
    }
}
