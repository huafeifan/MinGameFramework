using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomNamespace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EventManager))]
    public class EventManagerEditor : Editor
    {
        private SerializedProperty mListeners;

        private void OnEnable()
        {
            mListeners = serializedObject.FindProperty("mListeners");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(mListeners, new GUIContent("�¼��б�"));
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
