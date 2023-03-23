using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimComponent : MonoBehaviour
{
    [SerializeField]
    private Rig aimRig;

    [SerializeField]
    private Transform _aimTarget;
    public Transform AimTarget{get{return _aimTarget;}}

    private Player _player;

    [SerializeField]
    private GameObject _aimUI;



    //private float aimWeight = 0f;
    //private bool isAiming = false;
    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void StartAim(){
        //isAiming = true;
        _aimUI.SetActive(true);
    }

    public void StopAim(){
        //isAiming = false;
        _aimUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(aimWeight < 1f && isAiming){
            aimWeight += 2 * Time.deltaTime;
            if(aimWeight > 1){
                aimWeight = 1;
            }
        }else if(aimWeight > 0f && !isAiming){
            aimWeight -= 2 * Time.deltaTime;
            if(aimWeight < 0){
                aimWeight = 0;
            }
        }
        aimRig.weight = aimWeight;   
        */
    }
    public void SetAimPosition(Vector3 pos){
        _aimTarget.position = pos;
    }

    public Vector3 GetAimDirection(Vector3 origin, bool rayCast = true){
        Vector3 position = _player.AimCamera.transform.position + _player.AimCamera.transform.forward * 10f;;
        if(rayCast){
            RaycastHit hit;
            if(Physics.Raycast(_player.AimCamera.transform.position, _player.AimCamera.transform.forward, out hit)){
                position = hit.point;
            }
        }

        Vector3 direction = position - origin;
        return direction;
    }

}
