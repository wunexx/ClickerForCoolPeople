#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SecretBossSpawner))]
public class SecretBossSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SecretBossSpawner myTarget = (SecretBossSpawner)target;

        if (Application.isPlaying && GUILayout.Button("Start Event"))
        {
            myTarget.SpawnBoss();
        }
    }

}
#endif