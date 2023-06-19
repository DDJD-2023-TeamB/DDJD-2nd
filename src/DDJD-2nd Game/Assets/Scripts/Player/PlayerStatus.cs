using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    private Player _player;

    [SerializeField]
    private float _cameraShakeIntensity = 5.0f;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    protected void Start() { }

    public override void TakeDamage(
        GameObject damager,
        int damage,
        Vector3 hitPoint = default(Vector3),
        Vector3 hitDirection = default(Vector3)
    )
    {
        base.TakeDamage(damager, damage, hitPoint, hitDirection);
        float shakeIntensity =
            Mathf.Clamp01((float)damage / (float)_maxHealth) * _cameraShakeIntensity;
        _player.CameraController.ShakeCamera(shakeIntensity, 0.2f);

        //Update ui
        _player.UIController.UpdateHealth(_health, _maxHealth);
    }

    public void UpdateMana(Element element)
    {
        int mana = _elementMana[element];
        _player.UIController.UpdateMana(element, mana, _maxMana);
    }

    public override bool ConsumeMana(Element element, int manaCost)
    {
        bool success = base.ConsumeMana(element, manaCost);
        if (!success)
        {
            return false;
        }
        UpdateMana(element);

        return success;
    }

    public override void RestoreMana(Element element, int manaQuantity)
    {
        base.RestoreMana(element, manaQuantity);
        UpdateMana(element);
    }

    public override void RestoreHealth(int healthQuantity)
    {
        base.RestoreHealth(healthQuantity);
        _player.UIController.UpdateHealth(_health, _maxHealth);
    }
}
