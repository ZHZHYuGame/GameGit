<<<<<<< HEAD
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
=======
<<<<<<< HEAD
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
=======
using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.UI
{
    [CustomPropertyDrawer(typeof(Dropdown.OptionDataList), true)]
    /// <summary>
    /// This is a PropertyDrawer for Dropdown.OptionDataList. It is implemented using the standard Unity PropertyDrawer framework.
    /// </summary>
    class DropdownOptionListDrawer : PropertyDrawer
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        const string kOptionsPath = "m_Options";
        const string kTextPath = "m_Text";
        const string kImagePath = "m_Image";
        const string kHeader = "Options";
        const string kListViewUssName = "unity-list-view__header";
        const string kVisualElementName = "DropdownOptionDataList";

        // Offset for fixed size list items, so it wouldn't look tight or overlap each other
        const float itemOffset = 4;

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private ReorderableList m_ReorderableList;

        private void Init(SerializedProperty property)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (m_ReorderableList != null)
                return;

            SerializedProperty array = property.FindPropertyRelative("m_Options");
<<<<<<< HEAD
=======
=======
            if (m_ReorderableList != null && m_ReorderableList.serializedProperty.serializedObject.m_NativeObjectPtr != IntPtr.Zero)
            {
                return;
            }

            SerializedProperty array = property.FindPropertyRelative(kOptionsPath);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            m_ReorderableList = new ReorderableList(property.serializedObject, array);
            m_ReorderableList.drawElementCallback = DrawOptionData;
            m_ReorderableList.drawHeaderCallback = DrawHeader;
            m_ReorderableList.elementHeight += 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            m_ReorderableList.DoList(position);
        }

        private void DrawHeader(Rect rect)
        {
<<<<<<< HEAD
            GUI.Label(rect, "Options");
=======
<<<<<<< HEAD
            GUI.Label(rect, "Options");
=======
            GUI.Label(rect, kHeader);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private void DrawOptionData(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty itemData = m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
<<<<<<< HEAD
            SerializedProperty itemText = itemData.FindPropertyRelative("m_Text");
            SerializedProperty itemImage = itemData.FindPropertyRelative("m_Image");
=======
<<<<<<< HEAD
            SerializedProperty itemText = itemData.FindPropertyRelative("m_Text");
            SerializedProperty itemImage = itemData.FindPropertyRelative("m_Image");
=======
            SerializedProperty itemText = itemData.FindPropertyRelative(kTextPath);
            SerializedProperty itemImage = itemData.FindPropertyRelative(kImagePath);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            RectOffset offset = new RectOffset(0, 0, -1, -3);
            rect = offset.Add(rect);
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, itemText, GUIContent.none);
            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, itemImage, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            return m_ReorderableList.GetHeight();
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            root.name = kVisualElementName;

            Init(property);

            var headerElement = new VisualElement();
            headerElement.AddToClassList(kListViewUssName);
            var header = new Label(kHeader);
            headerElement.Add(header);
            root.Add(headerElement);

            var listView = CreateListView(property);
            root.Add(listView);

            return root;
        }

        ListView CreateListView(SerializedProperty property)
        {
            var listView = new ListView
            {
                showAddRemoveFooter = true,
                reorderMode = ListViewReorderMode.Animated,
                showBorder = true,
                showFoldoutHeader = false,
                showBoundCollectionSize = false,
                showAlternatingRowBackgrounds = AlternatingRowBackground.None,
                fixedItemHeight = m_ReorderableList.elementHeight + itemOffset,
                horizontalScrollingEnabled = false,
                name = kHeader
            };


            var propertyRelative = property.FindPropertyRelative(kOptionsPath);
            listView.bindingPath = propertyRelative.propertyPath;

            listView.makeItem += () => new DropdownOptionListItem(kTextPath, kImagePath);

            return listView;
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
