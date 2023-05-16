using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// [CreateAssetMenu(fileName = "Quest", menuName = "DDJD-2nd Game/Quest", order = 0)]
public class Collect : Quest
{

    private int numberOfItemsToFind;
    private int currentNumberOfItemsFound;
    private string itemType;

    public override bool CheckEnd() {
        if (currentNumberOfItemsFound == numberOfItemsToFind) {
            Finish();
            return true;
        }
        return false;
    }

    //change for collectible
    public void CollectItem(GameObject item) {
        if (item.tag == itemType) {
            currentNumberOfItemsFound++;
        }
    }
}