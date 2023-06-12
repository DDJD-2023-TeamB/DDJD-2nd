using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeCombat : MonoBehaviour
{
    [Header("Hitboxes")]
    [SerializeField]
    private Hitbox leftHandHitBox;

    [SerializeField]
    private Hitbox rightHandHitBox;

    [SerializeField]
    private Hitbox rightFootHitBox;

    [SerializeField]
    private Hitbox leftFootHitBox;

    [Header("Combat settings")]
    [SerializeField]
    private float _damage = 40f;

    [SerializeField]
    private float _attackPushForce = 10f; // How much force to apply to the player when starting to attack
    public float AttackPushForce
    {
        get { return _attackPushForce; }
    }

    private Player _player;
    private PlayerSkills _playerSkills;

    public Action OnAttackStart;
    public Action OnAttackEnd;

    // Start is called before the first frame update
    void Awake()
    {
        leftFootHitBox.gameObject.SetActive(false);
        rightFootHitBox.gameObject.SetActive(false);
        leftHandHitBox.gameObject.SetActive(false);
        rightHandHitBox.gameObject.SetActive(false);

        leftFootHitBox.Parent = gameObject;
        rightFootHitBox.Parent = gameObject;
        leftHandHitBox.Parent = gameObject;
        rightHandHitBox.Parent = gameObject;

        _player = GetComponent<Player>();
        _playerSkills = _player.PlayerSkills;
    }

    // Update is called once per frame
    void Update() { }

    // Gets called in the beginning of each combat animation
    public void StartAttacking()
    {
        OnAttackStart?.Invoke();
    }

    // Gets called in the end of each combat animation
    public void StopAttacking()
    {
        OnAttackEnd?.Invoke();
    }

    // Force [0, 1]
    private void StartHit(Hitbox hitbox, float force)
    {
        float velocity = 1.0f + _player.Rigidbody.velocity.magnitude / 5.0f;
        hitbox.Activate(
            _playerSkills.CurrentElement,
            force * _damage * velocity,
            force * _damage * velocity
        );
    }

    private void StopHit(Hitbox hitbox)
    {
        hitbox.Deactivate();
    }

    //Animation events
    public void StartHitLeftHand(float force)
    {
        StartHit(leftHandHitBox, force);
    }

    public void StartHitRightHand(float force)
    {
        StartHit(rightHandHitBox, force);
    }

    public void StartHitRightFoot(float force)
    {
        StartHit(rightFootHitBox, force);
    }

    public void StartHitLeftFoot(float force)
    {
        StartHit(leftFootHitBox, force);
    }

    //Stop functions
    public void StopHitLeftHand()
    {
        StopHit(leftHandHitBox);
    }

    public void StopHitRightHand()
    {
        StopHit(rightHandHitBox);
    }

    public void StopHitRightFoot()
    {
        StopHit(rightFootHitBox);
    }

    public void StopHitLeftFoot()
    {
        StopHit(leftFootHitBox);
    }
}
