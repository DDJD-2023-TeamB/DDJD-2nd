using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private Mission _bossMission;

    [SerializeField]
    private GameState _gameState;

    [SerializeField]
    private MissionsUIController _missionsUIController;

    private void Start()
    {
        StartCoroutine(CheckBossMission());
    }

    private IEnumerator CheckBossMission()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!_missionsUIController.IsReady)
            {
                continue;
            }
            bool unblockBoss = true;
            foreach (var mission in _gameState.UnblockedMissions)
            {
                if (mission.Type == MissionType.Story && mission.Status != MissionState.Completed)
                {
                    unblockBoss = false;
                    break;
                }
            }

            if (unblockBoss)
            {
                _bossMission.Unblock();
                _gameState.UnblockedMissions.Add(_bossMission);
                _missionsUIController.UpdateMissionsUI();
                yield break;
            }
        }
    }
}
