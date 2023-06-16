using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCommunicator : MonoBehaviour
{
    private Dictionary<Type, Action<EnemyMessage>> _messageActions =
        new Dictionary<Type, Action<EnemyMessage>>();
    private Dictionary<Type, EnemyMessage> _messagesUnhandled =
        new Dictionary<Type, EnemyMessage>();

    [SerializeField]
    private float _messageRange = 50.0f;

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
        else
        {
            //Store message so when action is setted, it will be called
            if (_messagesUnhandled.ContainsKey(message.GetType()))
            {
                _messagesUnhandled[message.GetType()] = message;
            }
            else
            {
                _messagesUnhandled.Add(message.GetType(), message);
            }
        }
    }

    public void SetMessageAction(Type messageType, Action<EnemyMessage> action)
    {
        if (!_messageActions.ContainsKey(messageType))
            _messageActions.Add(messageType, action);
        else
            _messageActions[messageType] = action;

        if (_messagesUnhandled.ContainsKey(messageType))
        {
            _messageActions[messageType]?.Invoke(_messagesUnhandled[messageType]);
            _messagesUnhandled.Remove(messageType);
        }
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

    public IEnumerator SendMessageToEnemies(EnemyMessage message)
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            _messageRange,
            LayerMask.GetMask("Enemy") | LayerMask.GetMask("PlayerTrigger")
        );
        foreach (Collider collider in colliders)
        {
            if (collider == null)
            {
                break;
            }
            EnemyCommunicator communicator = collider.GetComponent<EnemyCommunicator>();
            float timeToWait =
                Vector3.Distance(transform.position, collider.transform.position) / 10.0f;
            yield return new WaitForSeconds(timeToWait);
            communicator?.ReceiveMessage(message);
        }
    }

    public void SendMessage(GameObject enemy, EnemyMessage message)
    {
        enemy.GetComponent<EnemyCommunicator>()?.ReceiveMessage(message);
    }
}
