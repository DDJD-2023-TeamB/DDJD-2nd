using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElementSource : MonoBehaviour
{
    [SerializeField]
    private Element _element;

    [SerializeField]
    private GameObject _tranferVfxPrefab;

    private Player _player;

    private VisualEffect _currentVFX;

    protected void Awake() { }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public virtual void Transfer(Player player, Transform handTransform)
    {
        _player = player;
        _currentVFX = Instantiate(_tranferVfxPrefab, transform.position, Quaternion.identity)
            .GetComponent<VisualEffect>();

        _currentVFX.SetVector3("Target", handTransform.position);
    }

    public virtual void StopTransfer(Player player)
    {
        if (_player == player)
        {
            _currentVFX.Stop();
            Destroy(_currentVFX.gameObject, 1f);
        }
    }

    public Element Element
    {
        get { return _element; }
    }
}
