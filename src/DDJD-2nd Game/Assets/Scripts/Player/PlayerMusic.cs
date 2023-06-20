using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusic : MonoBehaviour
{
    private SoundEmitter _soundEmitter;
    private Player _player;

    private FMOD.Studio.PARAMETER_ID _musicHealthParameterId;
    private FMOD.Studio.PARAMETER_ID _musicMenuParameterId;
    private FMOD.Studio.PARAMETER_ID _musicFadeParameterId;

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
        _musicFadeParameterId = _soundEmitter.GetParameterId("battle_music", "Fade");
        StartCoroutine(MusicUpdate());

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCombatStart += StartCombat;
            GameManager.Instance.OnCombatEnd += EndCombat;
        }
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

    public void StartCombat()
    {
        UpdateMusicParameters();
        _soundEmitter.Play("battle_music");
        _soundEmitter.CallWithDelay(
            () => _soundEmitter.SetParameter("battle_music", _musicFadeParameterId, 1.0f),
            0.1f
        );

        _updateCoroutine = StartCoroutine(MusicUpdate());
    }

    public void EndCombat()
    {
        Debug.Log("End combat in player");
        _soundEmitter.SetParameter("battle_music", _musicFadeParameterId, 0.0f);
        _soundEmitter.CallWithDelay(() => _soundEmitter.Stop("battle_music"), 2.0f);
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }
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
        float health = (float)_player.Status.Health / _player.Status.MaxHealth;
        if (health == 0.0f)
        {
            health = 0.01f;
        }
        _soundEmitter.SetParameter("battle_music", _musicHealthParameterId, health);
        _soundEmitter.SetParameter("battle_music", _musicMenuParameterId, _menu_parameter_value);
    }
}
