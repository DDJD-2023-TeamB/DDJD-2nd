using System.Collections.Generic;
using UnityEngine;

using System;

[System.Serializable]
public class SerializedDictionary<TKey, TValue>
    : Dictionary<TKey, TValue>,
        ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> _keys = new List<TKey>();

    [SerializeField]
    private List<TValue> _values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        //_keys.Clear();
        //_values.Clear();
        /*
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
        */
    }

    public void OnAfterDeserialize()
    {
        Clear();
        for (int i = 0; i < Math.Min(_keys.Count, _values.Count); i++)
        {
            Add(_keys[i], _values[i]);
        }
    }
}
