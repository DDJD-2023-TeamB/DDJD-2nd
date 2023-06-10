using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "ElementUI",
    menuName = "Scriptable Objects/Elements/ElementUI",
    order = 1
)]
public class ElementUI : ScriptableObject
{
    [SerializeField]
    private Sprite outline;

    [SerializeField]
    private Sprite fill;

    [SerializeField]
    private Element element;

    public Sprite Outline
    {
        get { return outline; }
    }

    public Sprite Fill
    {
        get { return fill; }
    }

    public Element Element
    {
        get { return element; }
    }
}
