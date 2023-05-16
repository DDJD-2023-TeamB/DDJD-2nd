using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// [CreateAssetMenu(fileName = "Quest", menuName = "DDJD-2nd Game/Quest", order = 0)]
public class Search : Quest
{
    private GameObject questItem;
    private bool isFound;
    
    public override bool CheckEnd() {
        if (isFound) {
            Finish();
            return true;
        }
        return false;
    }

    public void FindObject(GameObject questItem) {
        if (questItem == this.questItem) {
            isFound = true;
        }
    }
}