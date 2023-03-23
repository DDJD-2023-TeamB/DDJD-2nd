using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateContext : MonoBehaviour
{
    protected GenericState _state;
    
    public void ChangeState(GenericState state){
        if(_state != null){
            if(!_state.CanChangeState(state)){
                return;
            }
            _state.Exit();
        }
        _state = state;
        _state.Enter();
    }   

    void Update(){
        _state.StateUpdate();
    }

}
