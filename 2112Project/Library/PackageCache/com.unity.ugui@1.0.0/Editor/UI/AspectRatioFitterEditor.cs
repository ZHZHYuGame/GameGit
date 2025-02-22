using UnityEngine;
<<<<<<< HEAD
=======
using UnityEditor.AnimatedValues;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(AspectRatioFitter), true)]
    [CanEditMultipleObjects]
    /// <summary>
    ///   Custom Editor for the AspectRatioFitter component.
    ///   Extend this class to write a custom editor for a component derived from AspectRatioFitter.
    /// </summary>
    public class AspectRatioFitterEditor : SelfControllerEditor
    {
        SerializedProperty m_AspectMode;
        SerializedProperty m_AspectRatio;

<<<<<<< HEAD
=======

        AnimBool m_ModeBool;
        private AspectRatioFitter aspectRatioFitter;

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        protected virtual void OnEnable()
        {
            m_AspectMode = serializedObject.FindProperty("m_AspectMode");
            m_AspectRatio = serializedObject.FindProperty("m_AspectRatio");
<<<<<<< HEAD
=======
            aspectRatioFitter = target as AspectRatioFitter;

            m_ModeBool = new AnimBool(m_AspectMode.intValue != 0);
            m_ModeBool.valueChanged.AddListener(Repaint);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_AspectMode);
<<<<<<< HEAD
            EditorGUILayout.PropertyField(m_AspectRatio);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
=======

            m_ModeBool.target = m_AspectMode.intValue != 0;

            if (EditorGUILayout.BeginFadeGroup(m_ModeBool.faded))
                EditorGUILayout.PropertyField(m_AspectRatio);
            EditorGUILayout.EndFadeGroup();

            serializedObject.ApplyModifiedProperties();

            if (aspectRatioFitter)
            {
                if (!aspectRatioFitter.IsAspectModeValid())
                    ShowNoParentWarning();
                if (!aspectRatioFitter.IsComponentValidOnObject())
                    ShowCanvasRenderModeInvalidWarning();
            }

            base.OnInspectorGUI();
        }

        protected virtual void OnDisable()
        {
            aspectRatioFitter = null;
            m_ModeBool.valueChanged.RemoveListener(Repaint);
        }

        private static void ShowNoParentWarning()
        {
            var text = L10n.Tr("You cannot use this Aspect Mode because this Component's GameObject does not have a parent object.");
            EditorGUILayout.HelpBox(text, MessageType.Warning, true);
        }

        private static void ShowCanvasRenderModeInvalidWarning()
        {
            var text = L10n.Tr("You cannot use this Aspect Mode because this Component is attached to a Canvas with a fixed width and height.");
            EditorGUILayout.HelpBox(text, MessageType.Warning, true);
        }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
