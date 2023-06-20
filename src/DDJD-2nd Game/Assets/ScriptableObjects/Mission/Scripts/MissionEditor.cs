#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Mission mission = (Mission)target;

        //Show goals
        GUILayout.Label("Goals", EditorStyles.boldLabel);
        foreach (GoalObject goal in mission.Goals)
        {
            GUILayout.Label("Goal " + mission.Goals.IndexOf(goal), EditorStyles.largeLabel);
            goal.OnGUI();
            //Space
            GUILayout.Space(5);
            if (GUILayout.Button("Remove goal"))
            {
                AssetDatabase.RemoveObjectFromAsset(goal);
                mission.Goals.Remove(goal);
                break;
            }
            GUILayout.Space(25);
        }
        if (GUILayout.Button("Add Interact Goal"))
        {
            InteractGoal interactGoal = CreateInstance<InteractGoal>();
            AssetDatabase.AddObjectToAsset(interactGoal, mission);
            mission.Goals.Add(interactGoal);
        }
        if (GUILayout.Button("Add Fight Goal"))
        {
            FightGoal fightGoal = CreateInstance<FightGoal>();
            AssetDatabase.AddObjectToAsset(fightGoal, mission);
            mission.Goals.Add(fightGoal);
        }
        if (GUILayout.Button("Add Collect Goal"))
        {
            CollectGoal collectGoal = CreateInstance<CollectGoal>();
            AssetDatabase.AddObjectToAsset(collectGoal, mission);
            mission.Goals.Add(collectGoal);
        }
    }
}

#endif
