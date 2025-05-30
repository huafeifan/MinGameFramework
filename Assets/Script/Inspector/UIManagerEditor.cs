using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomNamespace
{
#if UNITY_EDITOR
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        private SerializedProperty UIRoot;
        private SerializedProperty mUICache;
        private SerializedProperty UICamera;

        private void OnEnable()
        {
            UIRoot = serializedObject.FindProperty("UIRoot");
            mUICache = serializedObject.FindProperty("mUICache");
            UICamera = serializedObject.FindProperty("UICamera");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(UIRoot, new GUIContent("UI根节点"));
            EditorGUILayout.PropertyField(UICamera, new GUIContent("UI相机"));
            EditorGUILayout.PropertyField(mUICache, new GUIContent("已创建Prefab列表"));
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
