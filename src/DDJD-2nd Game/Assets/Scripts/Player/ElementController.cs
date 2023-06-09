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

    [SerializeField]
    private float _offset = 2.0f;
    private Player _player;

    [SerializeField]
    private GameObject _absorbTarget;

    public ElementSource SourceToAbsorb
    {
        get { return _sourceToAbsorb; }
    }

    protected void Awake()
    {
        _player = GetComponent<Player>();
    }

    protected void Start() { }

    public bool CanAbsorb()
    {
        Vector3 position = _player.transform.position;
        position.y += 1f;
        position -= _player.transform.forward * _offset;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(
            position,
            _player.transform.forward,
            _absorbRange + _offset,
            LayerMask.GetMask("ElementSource")
        );
        foreach (RaycastHit hit in hits)
        {
            _sourceToAbsorb = hit.collider.GetComponent<ElementSource>();
            if (_sourceToAbsorb != null)
            {
                return true;
            }
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
        _sourceToAbsorb.Transfer(_player, _absorbTarget.transform);
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
