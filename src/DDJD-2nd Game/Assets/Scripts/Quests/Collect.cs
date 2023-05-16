using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// [CreateAssetMenu(fileName = "Quest", menuName = "DDJD-2nd Game/Quest", order = 0)]
public class Collect : Quest
{

    private GameObject[] itemsToFind;
    private GameObject[] currentItems;

    public override bool CheckEnd() {
        bool areEqual = itemsToFind.OrderBy(x => x.GetInstanceID()).SequenceEqual(currentItems.OrderBy(x => x.GetInstanceID()));

        if (areEqual) {
            Finish();
            return true;
        }
        return false;
    }

    public void CollectItem(GameObject item) {
        if (itemsToFind.Contains(item)) {
            AddItem(item);
        }
    }

    private void AddItem(GameObject item) {
        List<GameObject> list = new List<GameObject>(currentItems);
        list.Add(item);
        currentItems = list.ToArray();
    }


}