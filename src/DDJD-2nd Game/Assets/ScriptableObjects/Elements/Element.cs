using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Scriptable Objects/Elements/Element", order = 1)]
public class Element : ScriptableObject
{
    [SerializeField]
    private GameObject _hitVfx;
    public GameObject HitVfx
    {
        get => _hitVfx;
    }

    [SerializeField]
    private GameObject _attackVfx;

    public GameObject AttackVfx
    {
        get => _attackVfx;
    }



    
    
}
