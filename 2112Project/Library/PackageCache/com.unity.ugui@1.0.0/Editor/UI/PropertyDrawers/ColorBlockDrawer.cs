using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
using UnityEditor.UIElements;
using UnityEngine.UIElements;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.UI
{
    [CustomPropertyDrawer(typeof(ColorBlock), true)]
    /// <summary>
    /// This is a PropertyDrawer for ColorBlock. It is implemented using the standard Unity PropertyDrawer framework..
    /// </summary>
    public class ColorBlockDrawer : PropertyDrawer
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        const string kNormalColor = "m_NormalColor";
        const string kHighlightedColor = "m_HighlightedColor";
        const string kPressedColor = "m_PressedColor";
        const string kSelectedColor = "m_SelectedColor";
        const string kDisabledColor = "m_DisabledColor";
        const string kColorMultiplier = "m_ColorMultiplier";
        const string kFadeDuration = "m_FadeDuration";

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
            SerializedProperty normalColor = prop.FindPropertyRelative("m_NormalColor");
            SerializedProperty highlighted = prop.FindPropertyRelative("m_HighlightedColor");
            SerializedProperty pressedColor = prop.FindPropertyRelative("m_PressedColor");
            SerializedProperty selectedColor = prop.FindPropertyRelative("m_SelectedColor");
            SerializedProperty disabledColor = prop.FindPropertyRelative("m_DisabledColor");
            SerializedProperty colorMultiplier = prop.FindPropertyRelative("m_ColorMultiplier");
            SerializedProperty fadeDuration = prop.FindPropertyRelative("m_FadeDuration");
<<<<<<< HEAD
=======
=======
            SerializedProperty normalColor = prop.FindPropertyRelative(kNormalColor);
            SerializedProperty highlighted = prop.FindPropertyRelative(kHighlightedColor);
            SerializedProperty pressedColor = prop.FindPropertyRelative(kPressedColor);
            SerializedProperty selectedColor = prop.FindPropertyRelative(kSelectedColor);
            SerializedProperty disabledColor = prop.FindPropertyRelative(kDisabledColor);
            SerializedProperty colorMultiplier = prop.FindPropertyRelative(kColorMultiplier);
            SerializedProperty fadeDuration = prop.FindPropertyRelative(kFadeDuration);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            EditorGUI.PropertyField(drawRect, normalColor);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, highlighted);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, pressedColor);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, selectedColor);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, disabledColor);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, colorMultiplier);
            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(drawRect, fadeDuration);
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            return 7 * EditorGUIUtility.singleLineHeight + 6 * EditorGUIUtility.standardVerticalSpacing;
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            var properties = new[]
            {
                property.FindPropertyRelative(kNormalColor),
                property.FindPropertyRelative(kHighlightedColor),
                property.FindPropertyRelative(kPressedColor),
                property.FindPropertyRelative(kSelectedColor),
                property.FindPropertyRelative(kDisabledColor),
                property.FindPropertyRelative(kColorMultiplier),
                property.FindPropertyRelative(kFadeDuration)
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
