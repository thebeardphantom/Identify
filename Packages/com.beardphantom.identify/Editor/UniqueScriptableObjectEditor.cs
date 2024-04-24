using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UniqueScriptableObject), true)]
    public class UniqueScriptableObjectEditor : UnityEditor.Editor
    {
        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            if (targets.Length > 1)
            {
                base.OnInspectorGUI();
                return;
            }

            serializedObject.Update();
            var scriptProperty = serializedObject.FindProperty("m_Script");
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(scriptProperty);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                var identifierProperty = serializedObject.FindProperty(UniqueScriptableObject.IDENTIFIER_PROPERTY_NAME);
                if (string.IsNullOrWhiteSpace(identifierProperty.stringValue))
                {
                    var obj = (UniqueScriptableObject)target;
                    identifierProperty.stringValue = obj.RegenerateIdentifier();
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                }

                EditorGUILayout.PrefixLabel("Identifier");
                EditorGUILayout.SelectableLabel(
                    identifierProperty.stringValue,
                    GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }

            DrawPropertiesExcluding(serializedObject, "m_Script", UniqueScriptableObject.IDENTIFIER_PROPERTY_NAME);
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}