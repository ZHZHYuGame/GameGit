<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
=======
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
=======
using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.UI
{
    [CustomPropertyDrawer(typeof(Navigation), true)]
    /// <summary>
    /// This is a PropertyDrawer for Navigation. It is implemented using the standard Unity PropertyDrawer framework.
    /// </summary>
    public class NavigationDrawer : PropertyDrawer
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        const string kNavigation = "Navigation";

        const string kModeProp = "m_Mode";
        const string kWrapAroundProp = "m_WrapAround";
        const string kSelectOnUpProp = "m_SelectOnUp";
        const string kSelectOnDownProp = "m_SelectOnDown";
        const string kSelectOnLeftProp = "m_SelectOnLeft";
        const string kSelectOnRightProp = "m_SelectOnRight";

        const string kHiddenClass = "unity-ui-navigation-hidden";

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private class Styles
        {
            readonly public GUIContent navigationContent;

            public Styles()
            {
<<<<<<< HEAD
                navigationContent = EditorGUIUtility.TrTextContent("Navigation");
=======
<<<<<<< HEAD
                navigationContent = EditorGUIUtility.TrTextContent("Navigation");
=======
                navigationContent = EditorGUIUtility.TrTextContent(kNavigation);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        private static Styles s_Styles = null;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            if (s_Styles == null)
                s_Styles = new Styles();

            Rect drawRect = pos;
            drawRect.height = EditorGUIUtility.singleLineHeight;

<<<<<<< HEAD
            SerializedProperty navigation = prop.FindPropertyRelative("m_Mode");
=======
<<<<<<< HEAD
            SerializedProperty navigation = prop.FindPropertyRelative("m_Mode");
            SerializedProperty wrapAround = prop.FindPropertyRelative("m_WrapAround");
=======
            SerializedProperty navigation = prop.FindPropertyRelative(kModeProp);
            SerializedProperty wrapAround = prop.FindPropertyRelative(kWrapAroundProp);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            Navigation.Mode navMode = GetNavigationMode(navigation);

            EditorGUI.PropertyField(drawRect, navigation, s_Styles.navigationContent);

            ++EditorGUI.indentLevel;

            drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            switch (navMode)
            {
<<<<<<< HEAD
                case Navigation.Mode.Explicit:
                {
=======
                case Navigation.Mode.Horizontal:
                case Navigation.Mode.Vertical:
                {
                    EditorGUI.PropertyField(drawRect, wrapAround);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                }
                break;
                case Navigation.Mode.Explicit:
                {
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    SerializedProperty selectOnUp = prop.FindPropertyRelative("m_SelectOnUp");
                    SerializedProperty selectOnDown = prop.FindPropertyRelative("m_SelectOnDown");
                    SerializedProperty selectOnLeft = prop.FindPropertyRelative("m_SelectOnLeft");
                    SerializedProperty selectOnRight = prop.FindPropertyRelative("m_SelectOnRight");
<<<<<<< HEAD
=======
=======
                    SerializedProperty selectOnUp = prop.FindPropertyRelative(kSelectOnUpProp);
                    SerializedProperty selectOnDown = prop.FindPropertyRelative(kSelectOnDownProp);
                    SerializedProperty selectOnLeft = prop.FindPropertyRelative(kSelectOnLeftProp);
                    SerializedProperty selectOnRight = prop.FindPropertyRelative(kSelectOnRightProp);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

                    EditorGUI.PropertyField(drawRect, selectOnUp);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(drawRect, selectOnDown);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(drawRect, selectOnLeft);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(drawRect, selectOnRight);
                    drawRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                }
                break;
            }

            --EditorGUI.indentLevel;
        }

        static Navigation.Mode GetNavigationMode(SerializedProperty navigation)
        {
            return (Navigation.Mode)navigation.enumValueIndex;
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
<<<<<<< HEAD
            SerializedProperty navigation = prop.FindPropertyRelative("m_Mode");
=======
<<<<<<< HEAD
            SerializedProperty navigation = prop.FindPropertyRelative("m_Mode");
=======
            SerializedProperty navigation = prop.FindPropertyRelative(kModeProp);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (navigation == null)
                return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            Navigation.Mode navMode = GetNavigationMode(navigation);

            switch (navMode)
            {
<<<<<<< HEAD
                case Navigation.Mode.None: return EditorGUIUtility.singleLineHeight;
                case Navigation.Mode.Explicit: return 5 * EditorGUIUtility.singleLineHeight + 5 * EditorGUIUtility.standardVerticalSpacing;
                default: return EditorGUIUtility.singleLineHeight + 1 * EditorGUIUtility.standardVerticalSpacing;
            }
        }
=======
                case Navigation.Mode.None:
                    return EditorGUIUtility.singleLineHeight;
                case Navigation.Mode.Horizontal:
                case Navigation.Mode.Vertical:
                    return 2 * EditorGUIUtility.singleLineHeight + 2 * EditorGUIUtility.standardVerticalSpacing;
                case Navigation.Mode.Explicit:
                    return 5 * EditorGUIUtility.singleLineHeight + 5 * EditorGUIUtility.standardVerticalSpacing;
                default:
                    return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }
<<<<<<< HEAD
=======

        PropertyField PrepareField(VisualElement parent, string propertyPath, bool hideable = true, string label = null)
        {
            var field = new PropertyField(null, label) { bindingPath = propertyPath };
            if (hideable) field.AddToClassList(kHiddenClass);
            parent.Add(field);
            return field;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement() { name = kNavigation };
            var indented = new VisualElement() { name = "Indent" };

            indented.AddToClassList("unity-ui-navigation-indent");

            var navigation = PrepareField(container, kModeProp, false, kNavigation);
            var wrapAround = PrepareField(indented, kWrapAroundProp);
            var selectOnUp = PrepareField(indented, kSelectOnUpProp);
            var selectOnDown = PrepareField(indented, kSelectOnDownProp);
            var selectOnLeft = PrepareField(indented, kSelectOnLeftProp);
            var selectOnRight = PrepareField(indented, kSelectOnRightProp);

            Action<Navigation.Mode> callback = (value) =>
            {
                wrapAround.EnableInClassList(kHiddenClass, value != Navigation.Mode.Vertical && value != Navigation.Mode.Horizontal);
                selectOnUp.EnableInClassList(kHiddenClass, value != Navigation.Mode.Explicit);
                selectOnDown.EnableInClassList(kHiddenClass, value != Navigation.Mode.Explicit);
                selectOnLeft.EnableInClassList(kHiddenClass, value != Navigation.Mode.Explicit);
                selectOnRight.EnableInClassList(kHiddenClass, value != Navigation.Mode.Explicit);
            };

            navigation.RegisterValueChangeCallback((e) => callback.Invoke((Navigation.Mode)e.changedProperty.enumValueIndex));
            callback.Invoke((Navigation.Mode)property.FindPropertyRelative(kModeProp).enumValueFlag);

            container.Add(indented);
            return container;
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
