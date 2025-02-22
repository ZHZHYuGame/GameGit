using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using UnityEditor.UIElements;
using UnityEngine.UIElements;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611

namespace UnityEditor.UI
{
    [CustomPropertyDrawer(typeof(SpriteState), true)]
    /// <summary>
    /// This is a PropertyDrawer for SpriteState. It is implemented using the standard Unity PropertyDrawer framework.
    /// </summary>
    public class SpriteStateDrawer : PropertyDrawer
    {
<<<<<<< HEAD
=======
        const string kHighlightedSprite = "m_HighlightedSprite";
        const string kPressedSprite = "m_PressedSprite";
        const string kSelectedSprite = "m_SelectedSprite";
        const string kDisabledSprite = "m_DisabledSprite";
        const string kVisualElementName = "SpriteState";

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            Rect drawRect = rect;
            drawRect.height = EditorGUIUtility.singleLineHeight;
<<<<<<< HEAD
            SerializedProperty highlightedSprite = prop.FindPropertyRelative("m_HighlightedSprite");
            SerializedProperty pressedSprite = prop.FindPropertyRelative("m_PressedSprite");
            SerializedProperty selectedSprite = prop.FindPropertyRelative("m_SelectedSprite");
            SerializedProperty disabledSprite = prop.FindPropertyRelative("m_DisabledSprite");
=======
            SerializedProperty highlightedSprite = prop.FindPropertyRelative(kHighlightedSprite);
            SerializedProperty pressedSprite = prop.FindPropertyRelative(kPressedSprite);
            SerializedProperty selectedSprite = prop.FindPropertyRelative(kSelectedSprite);
            SerializedProperty disabledSprite = prop.FindPropertyRelative(kDisabledSprite);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611

            EditorGUI.PropertyField(drawRect, highlightedSprite);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, pressedSprite);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, selectedSprite);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, disabledSprite);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            return 4 * EditorGUIUtility.singleLineHeight + 3 * EditorGUIUtility.standardVerticalSpacing;
        }
<<<<<<< HEAD
=======

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            container.name = kVisualElementName;

            var properties = new[]
            {
                property.FindPropertyRelative(kHighlightedSprite),
                property.FindPropertyRelative(kPressedSprite),
                property.FindPropertyRelative(kSelectedSprite),
                property.FindPropertyRelative(kDisabledSprite)
            };

            foreach (var prop in properties)
            {
                container.Add(new PropertyField(prop));
            }

            return container;
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
    }
}
