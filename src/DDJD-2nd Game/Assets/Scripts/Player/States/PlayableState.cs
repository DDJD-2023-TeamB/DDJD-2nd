using UnityEngine;
using System.Collections;
using System;
public class PlayableState : GenericState
{
    private Player _context;

    public PlayableState(StateContext context) : base(context, null)
    { 
        _context = (Player)context;
        
    }

    public override void Enter()
    {
        ChangeSubState(_context.Factory.Grounded(this));
    }

    public override void Exit()
    {
        
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        CheckAirbone();
        LookRotation();
    }

    private void CheckAirbone(){
        if(!MovementUtils.IsGrounded(_context.Rigidbody) && !(_substate is AirborneState)){
            ChangeSubState(_context.Factory.Airborne(_substate));
        } else if(MovementUtils.IsGrounded(_context.Rigidbody) && _substate is AirborneState){
            ChangeSubState(_context.Factory.Grounded(_substate));
        }
    }

    private void LookRotation(){
        Vector2 lookInput = _context.Input.LookInput;
        if(lookInput.magnitude > 1000f){
            return;
        }
        
        float minAngle = _context.MinAngle;
        float maxAngle = _context.MaxAngle;

        if(lookInput.x != 0f && Math.Abs(lookInput.x) > 0.5){

            float amountToRotate = lookInput.x * _context.CameraRotationSpeed * Time.deltaTime;
            _context.Rigidbody.MoveRotation(_context.Rigidbody.rotation * Quaternion.Euler(new Vector3(0f, amountToRotate, 0f)));
        }

        if(lookInput.y != 0f && Math.Abs(lookInput.x) > 0.5){
            float amountToRotate = -lookInput.y * _context.CameraRotationSpeed * Time.deltaTime;
            float finalAngle = _context.CameraTarget.transform.localEulerAngles.x + amountToRotate;
            if(finalAngle <= minAngle || finalAngle >= maxAngle){
                _context.CameraTarget.transform.Rotate(new Vector3(amountToRotate, 0f, 0f), Space.Self);    
            }
            
        }

        //Fix camera angle if broken
        float cameraAngle = _context.CameraTarget.transform.localEulerAngles.x;
        if(cameraAngle > minAngle && cameraAngle < maxAngle){
            float amountToRotate = minAngle - cameraAngle;
            _context.CameraTarget.transform.Rotate(new Vector3(amountToRotate, 0f, 0f), Space.Self);    
        }




        //Raycast to find the correct aim height
        Vector3 targetPosition = _context.AimCamera.transform.position + _context.AimCamera.transform.forward * 10f;
        _context.AimComponent.SetAimPosition(targetPosition);

        
    }
}