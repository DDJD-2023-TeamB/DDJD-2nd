using UnityEngine;
using UnityEngine.Events;

public class EventUtils
{
    public static bool IsEventSet(UnityEvent unityEvent)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            if (unityEvent.GetPersistentTarget(i) != null)
            {
                return true;
            }
        }
        return false;
    }
}
