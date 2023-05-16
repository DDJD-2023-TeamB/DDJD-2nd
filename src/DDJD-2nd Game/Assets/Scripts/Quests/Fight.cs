using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

// [CreateAssetMenu(fileName = "Quest", menuName = "DDJD-2nd Game/Quest", order = 0)]
public class Fight : Quest
{

    private int enemyDeathCount;
    private int currentEnemyDeathCount;

    public override bool CheckEnd() {
        if (currentEnemyDeathCount >= enemyDeathCount) {
            Finish();
            return true;
        }
        return false;
    }

    public void EnemyDeath() {
        currentEnemyDeathCount++;
    }
}