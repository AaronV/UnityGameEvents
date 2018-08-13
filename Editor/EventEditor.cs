using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class EventEditor : Editor
{
    float floatData;
    int intData;
    string stringData;
    GameType gameTypeData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        /*
         * Debugging
         */
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debugging", EditorStyles.boldLabel);

        GameEvent script = (GameEvent)target;

        switch (script.eventType)
        {
            case GameEventType.Float:
                floatData = EditorGUILayout.FloatField("Float", floatData);
                break;
            case GameEventType.Int:
                intData = EditorGUILayout.IntField("Int", intData);
                break;
            case GameEventType.String:
                stringData = EditorGUILayout.TextField("String", stringData);
                break;
            case GameEventType.GameType:
                gameTypeData = (GameType)EditorGUILayout.EnumPopup("GameType", gameTypeData);
                break;
            default:
                break;
        }

        if (GUILayout.Button("Raise"))
            Raise();
    }

    void Raise() {
        GameEvent script = (GameEvent)target;

        switch (script.eventType)
        {
            case GameEventType.Float:
                script.Raise(floatData);
                break;
            case GameEventType.Int:
                script.Raise(intData);
                break;
            case GameEventType.String:
                script.Raise(stringData);
                break;
            case GameEventType.GameType:
                script.Raise(gameTypeData);
                break;
            default:
                script.Raise();
                break;
        }
    }
}