using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCommunicator : MonoBehaviour
{
    private Dictionary<Type, Action<EnemyMessage>> _messageActions =
        new Dictionary<Type, Action<EnemyMessage>>();

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void ReceiveMessage(EnemyMessage message)
    {
        if (_messageActions.ContainsKey(message.GetType()))
        {
            _messageActions[message.GetType()]?.Invoke(message);
        }
    }

    public void SetMessageAction(Type messageType, Action<EnemyMessage> action)
    {
        if (!_messageActions.ContainsKey(messageType))
            _messageActions.Add(messageType, action);
        else
            _messageActions[messageType] = action;
    }

    public void DeleteAction(Type messageType, Action<EnemyMessage> action)
    {
        if (_messageActions.ContainsKey(messageType))
            _messageActions[messageType] -= action;
    }

    public Action<EnemyMessage> GetMessageAction(Type messageType)
    {
        if (!_messageActions.ContainsKey(messageType))
            _messageActions.Add(messageType, (EnemyMessage msg) => { });
        return _messageActions[messageType];
    }
}
