using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusic : MonoBehaviour
{
    private SoundEmitter _soundEmitter;
    private Player _player;

    private FMOD.Studio.PARAMETER_ID _musicHealthParameterId;
    private FMOD.Studio.PARAMETER_ID _musicMenuParameterId;

    private float _menu_parameter_value = 0;

    private Coroutine _updateCoroutine;

    private void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _musicHealthParameterId = _soundEmitter.GetParameterId("battle_music", "Health");
        _musicMenuParameterId = _soundEmitter.GetParameterId("battle_music", "Menu");
        StartCoroutine(MusicUpdate());

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCombatStart += StartCombat;
            GameManager.Instance.OnCombatEnd += EndCombat;
        }
    }

    public void StartAttack()
    {
        UpdateMusicParameters();
        _soundEmitter.Play("battle_music");
        _updateCoroutine = StartCoroutine(MusicUpdate());
    }

    private IEnumerator MusicUpdate()
    {
        while (true)
        {
            //_soundEmitter.SetParameter("Music", "Combat");
            yield return new WaitForSeconds(0.1f);
            UpdateMusicParameters();
        }
    }

    public void StartCombat() { }

    public void EndCombat()
    {
        _soundEmitter.Stop("battle_music");
        StopCoroutine(_updateCoroutine);
    }

    public void OpenMenu()
    {
        _menu_parameter_value = 1;
    }

    public void CloseMenu()
    {
        _menu_parameter_value = 0;
    }

    private void UpdateMusicParameters()
    {
        float health = _player.Status.Health / _player.Status.MaxHealth;
        _soundEmitter.SetParameter("battle_music", _musicHealthParameterId, health);
        _soundEmitter.SetParameter("battle_music", _musicMenuParameterId, _menu_parameter_value);
    }
}
