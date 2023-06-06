using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    private Coroutine _absorbCoroutine;
    private ElementSource _sourceToAbsorb;

    [SerializeField]
    private float _absorbTime = 0.1f;

    [SerializeField]
    private int _absorbAmount = 5;

    [SerializeField]
    private float _absorbRange = 3f;
    private Player _player;

    protected void Awake()
    {
        _player = GetComponent<Player>();
    }

    protected void Start() { }

    public bool CanAbsorb()
    {
        Vector3 position = _player.transform.position;
        position.y += 1f;
        RaycastHit hit;
        if (Physics.Raycast(position, _player.transform.forward, out hit, _absorbRange))
        {
            _sourceToAbsorb = hit.collider.GetComponent<ElementSource>();
            return _sourceToAbsorb != null;
        }
        return false;
    }

    public void StartAbsorb()
    {
        if (_sourceToAbsorb == null)
        {
            return;
        }
        _absorbCoroutine = StartCoroutine(AbsorbCoroutine());
    }

    private IEnumerator AbsorbCoroutine()
    {
        Element element = _sourceToAbsorb.Element;
        _sourceToAbsorb.Transfer(_player, _player.RightHand.transform);
        while (true)
        {
            yield return new WaitForSeconds(_absorbTime);
            _player.Status.RestoreMana(element, _absorbAmount);
        }
    }

    public void StopAbsorb()
    {
        _sourceToAbsorb.StopTransfer(_player);
        if (_absorbCoroutine != null)
        {
            StopCoroutine(_absorbCoroutine);
        }
        _sourceToAbsorb = null;
    }
}
