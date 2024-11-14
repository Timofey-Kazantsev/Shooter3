namespace XtremeFPS.Editor
{
    using UnityEditor;
    using UnityEngine;
    using XtremeFPS.WeaponSystem;

    [CustomEditor(typeof(ParabolicBullet)), CanEditMultipleObjects]
    public class ParabolicBulletEditor : UnityEditor.Editor
    {
        SerializedObject serParabolicBullet_UI;
        SerializedProperty bloodPrefabProperty;

        private void OnEnable()
        {
            serParabolicBullet_UI = new SerializedObject(target);
            bloodPrefabProperty = serParabolicBullet_UI.FindProperty("bloodPrefab");
        }

        public override void OnInspectorGUI()
        {
            serParabolicBullet_UI.Update();

            #region Intro
            EditorGUILayout.Space();
            GUI.color = Color.black;
            GUILayout.Label("Xtreme FPS Controller", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUI.color = Color.green;
            GUILayout.Label("Parabolic Bullet Script", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
            EditorGUILayout.Space();
            GUI.color = Color.black;
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            #endregion

            #region Blood Prefab Field
            EditorGUILayout.PropertyField(bloodPrefabProperty, new GUIContent("Blood Prefab"));
            #endregion

            #region Update Changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                serParabolicBullet_UI.ApplyModifiedProperties();
            }
            #endregion
        }
    }
}
