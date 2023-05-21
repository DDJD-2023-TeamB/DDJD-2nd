using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimComponent : MonoBehaviour, AimComponent
{
    [SerializeField]
    private Rig aimRig;

    [SerializeField]
    private Transform _aimTarget;
    public Transform AimTarget
    {
        get { return _aimTarget; }
    }

    private Player _player;

    [SerializeField]
    private GameObject _aimUI;

    private GameObject _leftRune;
    private GameObject _rightRune;

    private float aimWeight = 0f;
    private bool isAiming = false;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void StartAim()
    {
        isAiming = true;
        _aimUI.SetActive(true);

        if (_leftRune == null)
        {
            if (_player.PlayerSkills.RightSkill != null)
                _leftRune = _player.PlayerSkills.RightSkill.ActivateRune(_player.RightHand);
        }
        if (_rightRune == null)
        {
            if (_player.PlayerSkills.LeftSkill != null)
                _rightRune = _player.PlayerSkills.LeftSkill.ActivateRune(_player.LeftHand);
        }
    }

    public void StopAim()
    {
        isAiming = false;
        _aimUI.SetActive(false);

        GameObject[] runes = new GameObject[] { _leftRune, _rightRune };
        foreach (GameObject rune in runes)
        {
            if (rune != null)
            {
                Destroy(rune);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aimWeight < 1f && isAiming)
        {
            aimWeight += 2 * Time.deltaTime;
            if (aimWeight > 1)
            {
                aimWeight = 1;
            }
        }
        else if (aimWeight > 0f && !isAiming)
        {
            aimWeight -= 2 * Time.deltaTime;
            if (aimWeight < 0)
            {
                aimWeight = 0;
            }
        }
        aimRig.weight = 1.0f;
    }

    public void SetAimPosition(Vector3 pos)
    {
        _aimTarget.position = pos;
    }

    public Vector3 GetAimDirection(Vector3 origin, bool rayCast = true)
    {
        Vector3 position =
            _player.AimCamera.transform.position + _player.AimCamera.transform.forward * 10f;
        ;
        if (rayCast)
        {
            RaycastHit hit;
            if (GetAimRaycastHit(out hit))
            {
                position = hit.point;
            }
        }

        Vector3 direction = position - origin;
        return direction;
    }

    public Quaternion GetAimRotation()
    {
        return _player.CameraTarget.transform.rotation;
    }

    public bool GetAimRaycastHit(out RaycastHit hit)
    {
        return Physics.Raycast(
            _player.AimCamera.transform.position,
            _player.AimCamera.transform.forward,
            out hit,
            100f,
            ~LayerMask.GetMask("PlayerTrigger")
        );
    }
}
