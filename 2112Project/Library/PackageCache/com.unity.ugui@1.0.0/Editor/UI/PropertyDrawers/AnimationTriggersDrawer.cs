<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
=======
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
=======
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.UI
{
    [CustomPropertyDrawer(typeof(AnimationTriggers), true)]
    /// <summary>
    /// This is a PropertyDrawer for AnimationTriggers. It is implemented using the standard Unity PropertyDrawer framework.
    /// </summary>
    public class AnimationTriggersDrawer : PropertyDrawer
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        const string kNormalTrigger = "m_NormalTrigger";
        const string kHighlightedTrigger = "m_HighlightedTrigger";
        const string kPressedTrigger = "m_PressedTrigger";
        const string kSelectedTrigger = "m_SelectedTrigger";
        const string kDisabledTrigger = "m_DisabledTrigger";

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            Rect drawRect = rect;
            drawRect.height = EditorGUIUtility.singleLineHeight;

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            SerializedProperty normalTrigger = prop.FindPropertyRelative("m_NormalTrigger");
            SerializedProperty higlightedTrigger = prop.FindPropertyRelative("m_HighlightedTrigger");
            SerializedProperty pressedTrigger = prop.FindPropertyRelative("m_PressedTrigger");
            SerializedProperty selectedTrigger = prop.FindPropertyRelative("m_SelectedTrigger");
            SerializedProperty disabledTrigger = prop.FindPropertyRelative("m_DisabledTrigger");
<<<<<<< HEAD
=======
=======
            SerializedProperty normalTrigger = prop.FindPropertyRelative(kNormalTrigger);
            SerializedProperty higlightedTrigger = prop.FindPropertyRelative(kHighlightedTrigger);
            SerializedProperty pressedTrigger = prop.FindPropertyRelative(kPressedTrigger);
            SerializedProperty selectedTrigger = prop.FindPropertyRelative(kSelectedTrigger);
            SerializedProperty disabledTrigger = prop.FindPropertyRelative(kDisabledTrigger);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            EditorGUI.PropertyField(drawRect, normalTrigger);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, higlightedTrigger);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, pressedTrigger);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, selectedTrigger);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, disabledTrigger);
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            return 5 * EditorGUIUtility.singleLineHeight + 4 * EditorGUIUtility.standardVerticalSpacing;
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var properties = new[]
            {
                property.FindPropertyRelative(kNormalTrigger),
                property.FindPropertyRelative(kHighlightedTrigger),
                property.FindPropertyRelative(kPressedTrigger),
                property.FindPropertyRelative(kSelectedTrigger),
                property.FindPropertyRelative(kDisabledTrigger),
            };

            foreach (var prop in properties)
            {
                var field = new PropertyField(prop);
                container.Add(field);
            }

            return container;
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
