using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Sprites;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor._2D.Sprite.Editor
{
    [RequireSpriteDataProvider(typeof(ISecondaryTextureDataProvider), typeof(ITextureDataProvider))]
    internal class SpriteSecondaryTexturesModule : SpriteEditorModuleBase
    {
        private static class Styles
        {
            public static readonly string invalidEntriesWarning = L10n.Tr("Invalid secondary Texture entries (without names or Textures) have been removed.");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
            public static readonly string invalidSourcesWarning = L10n.Tr("Source texture used as secondary Texture. This is invalid and removed.");
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            public static readonly string nameUniquenessWarning = L10n.Tr("Every secondary Texture attached to the Sprite must have a unique name.");
            public static readonly string builtInNameCollisionWarning = L10n.Tr("The names _MainTex and _AlphaTex are reserved for internal use.");
            public static readonly GUIContent panelTitle = EditorGUIUtility.TrTextContent("Secondary Textures");
            public static readonly GUIContent name = EditorGUIUtility.TrTextContent("Name");
            public static readonly GUIContent texture = EditorGUIUtility.TrTextContent("Texture");
            public const float textFieldDropDownWidth = 18.0f;
        }


        ReorderableList m_ReorderableList;
        Vector2 m_ReorderableListScrollPosition;
        string[] m_SuggestedNames;
<<<<<<< HEAD

=======
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private IMGUIContainer m_SecondaryTextureInspectorContainer;
        internal List<SecondarySpriteTexture> secondaryTextureList { get; private set; }

        public override string moduleName
        {
            get { return Styles.panelTitle.text; }
        }

        public override bool ApplyRevert(bool apply)
        {
            if (apply)
            {
<<<<<<< HEAD
                var secondaryTextureDataProvider = spriteEditor.GetDataProvider<ISecondaryTextureDataProvider>();

=======
<<<<<<< HEAD
                var secondaryTextureDataProvider = spriteEditor.GetDataProvider<ISecondaryTextureDataProvider>();

=======
                var spriteAssetPath = "";
                var secondaryTextureDataProvider = spriteEditor.GetDataProvider<ISecondaryTextureDataProvider>();
                var spriteDataProvider = spriteEditor.GetDataProvider<ISpriteEditorDataProvider>();
                if (spriteDataProvider != null)
                {
                    var assetImporter = spriteDataProvider.targetObject as AssetImporter;
                    spriteAssetPath = assetImporter != null ? assetImporter.assetPath : spriteAssetPath;
                }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

                // Remove invalid entries.
                var validEntries = secondaryTextureList.FindAll(x => (x.name != null && x.name != "" && x.texture != null));
                if (validEntries.Count < secondaryTextureList.Count)
                    Debug.Log(Styles.invalidEntriesWarning);

<<<<<<< HEAD
                secondaryTextureDataProvider.textures = validEntries.ToArray();
=======
<<<<<<< HEAD
                secondaryTextureDataProvider.textures = validEntries.ToArray();
=======
                // Remove entries with Sprite's source as secondary textures.
                var finalEntries = validEntries.FindAll(x => (AssetDatabase.GetAssetPath(x.texture) != spriteAssetPath));
                if (finalEntries.Count < validEntries.Count)
                    Debug.Log(Styles.invalidSourcesWarning);

                secondaryTextureDataProvider.textures = finalEntries.ToArray();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }

            return true;
        }

        public override bool CanBeActivated()
        {
            var dataProvider = spriteEditor.GetDataProvider<ISpriteEditorDataProvider>();
            return dataProvider != null && dataProvider.spriteImportMode != SpriteImportMode.None;
        }

<<<<<<< HEAD
        public override void DoMainGUI()
=======
        public override void DoPostGUI()
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
        }

        public void SecondaryTextureReorderableListUI()
        {
            using (new EditorGUI.DisabledScope(spriteEditor.editingDisabled))
            {
                var windowDimension = spriteEditor.windowDimension;

                GUILayout.BeginArea(new Rect(0, 0, 290, 290), Styles.panelTitle, GUI.skin.window);
                m_ReorderableListScrollPosition = GUILayout.BeginScrollView(m_ReorderableListScrollPosition);
                m_ReorderableList.DoLayoutList();
                GUILayout.EndScrollView();
                GUILayout.EndArea();

                // Deselect the list item if left click outside the panel area.
                UnityEngine.Event e = UnityEngine.Event.current;
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    m_ReorderableList.index = -1;
<<<<<<< HEAD
                    spriteEditor.RequestRepaint();
                }
            }

            // Preview the current selected secondary texture.
            Texture2D previewTexture = null;
            int width = 0, height = 0;

            var textureDataProvider = spriteEditor.GetDataProvider<ITextureDataProvider>();
            if (textureDataProvider != null)
            {
                previewTexture = textureDataProvider.previewTexture;
                textureDataProvider.GetTextureActualWidthAndHeight(out width, out height);
            }

            if (m_ReorderableList.index >= 0 && m_ReorderableList.index < secondaryTextureList.Count)
                previewTexture = secondaryTextureList[m_ReorderableList.index].texture;

            if (previewTexture != null)
                spriteEditor.SetPreviewTexture(previewTexture, width, height);
        }

        public override void DoPostGUI()
=======
                    OnSelectCallback(m_ReorderableList);
                    spriteEditor.RequestRepaint();
                }
            }
        }

        public override void DoMainGUI()
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
        }

        public override void DoToolbarGUI(Rect drawArea)
        {
        }

        public override void OnModuleActivate()
        {
            var secondaryTextureDataProvider = spriteEditor.GetDataProvider<ISecondaryTextureDataProvider>();
            secondaryTextureList = secondaryTextureDataProvider.textures == null ? new List<SecondarySpriteTexture>() : secondaryTextureDataProvider.textures.ToList();

            m_ReorderableListScrollPosition = Vector2.zero;
            m_ReorderableList = new ReorderableList(secondaryTextureList, typeof(SecondarySpriteTexture), false, false, true, true);
            m_ReorderableList.drawElementCallback = DrawSpriteSecondaryTextureElement;
            m_ReorderableList.onAddCallback = AddSpriteSecondaryTextureElement;
            m_ReorderableList.onRemoveCallback = RemoveSpriteSecondaryTextureElement;
            m_ReorderableList.onCanAddCallback = CanAddSpriteSecondaryTextureElement;
            m_ReorderableList.elementHeightCallback = (int index) => (EditorGUIUtility.singleLineHeight * 3) + 5;
<<<<<<< HEAD
=======
            m_ReorderableList.onSelectCallback = OnSelectCallback;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            spriteEditor.selectedSpriteRect = null;

            string suggestedNamesPrefs = EditorPrefs.GetString("SecondarySpriteTexturePropertyNames");
            if (!string.IsNullOrEmpty(suggestedNamesPrefs))
            {
                m_SuggestedNames = suggestedNamesPrefs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < m_SuggestedNames.Length; ++i)
                    m_SuggestedNames[i] = m_SuggestedNames[i].Trim();

                Array.Sort(m_SuggestedNames);
            }
            else
                m_SuggestedNames = null;

            m_SecondaryTextureInspectorContainer = new IMGUIContainer(SecondaryTextureReorderableListUI)
            {
                style =
                {
                    flexGrow = 0,
                    flexBasis = 1,
                    flexShrink = 0,
                    minWidth = 290,
                    minHeight = 290,
                    bottom = 24,
                    right = 24,
                    position = new StyleEnum<Position>(Position.Absolute)
                },
                name = "SecondaryTextureInspector"
            };
            spriteEditor.GetMainVisualContainer().Add(m_SecondaryTextureInspectorContainer);
        }

<<<<<<< HEAD
        public override void OnModuleDeactivate()
        {
            // Reset to display the main texture.
            ITextureDataProvider textureDataProvider = spriteEditor.GetDataProvider<ITextureDataProvider>();
            if (textureDataProvider != null && textureDataProvider.previewTexture != null)
            {
                Texture2D mainTexture = textureDataProvider.previewTexture;
                int width = 0, height = 0;
                textureDataProvider.GetTextureActualWidthAndHeight(out width, out height);
                spriteEditor.SetPreviewTexture(mainTexture, width, height);
            }
=======
        void OnSelectCallback(ReorderableList list)
        {
            // Preview the current selected secondary texture.
            Texture2D previewTexture = null;
            int width = 0, height = 0;

            var textureDataProvider = spriteEditor.GetDataProvider<ITextureDataProvider>();
            if (textureDataProvider != null)
            {
                previewTexture = textureDataProvider.previewTexture;
                textureDataProvider.GetTextureActualWidthAndHeight(out width, out height);
            }

            if (m_ReorderableList.index >= 0 && m_ReorderableList.index < secondaryTextureList.Count)
                previewTexture = secondaryTextureList[m_ReorderableList.index].texture;

            if (previewTexture != null)
                spriteEditor.SetPreviewTexture(previewTexture, width, height);
        }

        public override void OnModuleDeactivate()
        {
            DisplayMainTexture();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (spriteEditor.GetMainVisualContainer().Contains(m_SecondaryTextureInspectorContainer))
                spriteEditor.GetMainVisualContainer().Remove(m_SecondaryTextureInspectorContainer);
        }

        void DrawSpriteSecondaryTextureElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            bool dataModified = false;
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 70.0f;
            SecondarySpriteTexture secondaryTexture = secondaryTextureList[index];

            // "Name" text field
            EditorGUI.BeginChangeCheck();
            var r = new Rect(rect.x, rect.y + 5, rect.width - Styles.textFieldDropDownWidth, EditorGUIUtility.singleLineHeight);
<<<<<<< HEAD
            string newName = EditorGUI.DelayedTextField(r, Styles.name, secondaryTexture.name);
=======
            string newName = EditorGUI.TextField(r, Styles.name, secondaryTexture.name);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            dataModified = EditorGUI.EndChangeCheck();

            // Suggested names
            if (m_SuggestedNames != null)
            {
                var popupRect = new Rect(r.x + r.width, r.y, Styles.textFieldDropDownWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.BeginChangeCheck();
                int selected = EditorGUI.Popup(popupRect, -1, m_SuggestedNames, EditorStyles.textFieldDropDown);
                if (EditorGUI.EndChangeCheck())
                {
                    newName = m_SuggestedNames[selected];
                    dataModified = true;
                }
            }

            if (dataModified)
            {
                if (!string.IsNullOrEmpty(newName) && newName != secondaryTexture.name && secondaryTextureList.Exists(x => x.name == newName))
                    Debug.LogWarning(Styles.nameUniquenessWarning);
                else if (newName == "_MainTex" || newName == "_AlphaTex")
                    Debug.LogWarning(Styles.builtInNameCollisionWarning);
                else
                    secondaryTexture.name = newName;
            }

            // "Texture" object field
            EditorGUI.BeginChangeCheck();
            r.width = rect.width;
<<<<<<< HEAD
            r.y += EditorGUIUtility.singleLineHeight;
=======
            r.y += EditorGUIUtility.singleLineHeight + 2.0f;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            secondaryTexture.texture = EditorGUI.ObjectField(r, Styles.texture, secondaryTexture.texture, typeof(Texture2D), false) as Texture2D;
            dataModified = dataModified || EditorGUI.EndChangeCheck();

            if (dataModified)
            {
                secondaryTextureList[index] = secondaryTexture;
                spriteEditor.SetDataModified();
            }

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }

        void AddSpriteSecondaryTextureElement(ReorderableList list)
        {
            m_ReorderableListScrollPosition += new Vector2(0.0f, list.elementHeightCallback(0));
            secondaryTextureList.Add(new SecondarySpriteTexture());
            spriteEditor.SetDataModified();
        }

        void RemoveSpriteSecondaryTextureElement(ReorderableList list)
        {
<<<<<<< HEAD
=======
            DisplayMainTexture();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            secondaryTextureList.RemoveAt(list.index);
            spriteEditor.SetDataModified();
        }

        bool CanAddSpriteSecondaryTextureElement(ReorderableList list)
        {
            return list.count < 8;
        }
<<<<<<< HEAD
=======

        void DisplayMainTexture()
        {
            ITextureDataProvider textureDataProvider = spriteEditor.GetDataProvider<ITextureDataProvider>();
            if (textureDataProvider != null && textureDataProvider.previewTexture != null)
            {
                Texture2D mainTexture = textureDataProvider.previewTexture;
                int width = 0, height = 0;
                textureDataProvider.GetTextureActualWidthAndHeight(out width, out height);
                spriteEditor.SetPreviewTexture(mainTexture, width, height);
            }
        }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
