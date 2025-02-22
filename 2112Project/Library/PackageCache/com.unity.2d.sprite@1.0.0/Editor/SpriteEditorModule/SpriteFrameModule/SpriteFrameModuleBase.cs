using System;
<<<<<<< HEAD
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
=======
using System.Collections;
using System.Linq;
using System.Collections.Generic;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using UnityEngine;
using UnityEditorInternal;

namespace UnityEditor.U2D.Sprites
{
    internal class SpriteRectModel : ScriptableObject, ISerializationCallbackReceiver
    {
<<<<<<< HEAD
        [SerializeField]
        private List<SpriteRect> m_SpriteRects;
        private HashSet<string> m_Names;
        private HashSet<long> m_InternalIds;

        private IReadOnlyList<SpriteRect> m_SpriteReadOnlyList;
        public IReadOnlyList<SpriteRect> spriteRects
        {
            get { return m_SpriteReadOnlyList; }
        }

        private SpriteRectModel()
        {
            m_Names = new HashSet<string>();
            m_InternalIds = new HashSet<long>();
=======
        [Serializable]
        struct StringGUID
        {
            [SerializeField]
            string m_StringGUID;

            public StringGUID(GUID guid)
            {
                m_StringGUID = guid.ToString();
            }

            public static implicit operator GUID(StringGUID d) => new GUID(d.m_StringGUID);
            public static implicit operator StringGUID(GUID d) => new StringGUID(d);
        }

        [Serializable]
<<<<<<< HEAD
        class StringGUIDList : List<StringGUID>, IReadOnlyList<GUID>
        {
            GUID IReadOnlyList<GUID>.this[int index]
            {
                get => this[index];
=======
        class StringGUIDList : IReadOnlyList<GUID>
        {
            [SerializeField]
            List<StringGUID> m_List = new List<StringGUID>();

            GUID IReadOnlyList<GUID>.this[int index]
            {
                get => m_List[index];
            }

            public StringGUID this[int index]
            {
                get => m_List[index];
                set => m_List[index] = value;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }

            IEnumerator<GUID> IEnumerable<GUID>.GetEnumerator()
            {
                // Not used for now
                throw new NotImplementedException();
            }
<<<<<<< HEAD
=======

            public int Count => m_List.Count;

            public IEnumerator GetEnumerator()
            {
                return m_List.GetEnumerator();
            }

            public void Clear()
            {
                m_List.Clear();
            }

            public void RemoveAt(int i)
            {
                m_List.RemoveAt(i);
            }

            public void Add(StringGUID value)
            {
                m_List.Add(value);
            }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        }

        /// <summary>
        /// List of all SpriteRects
        /// </summary>
        [SerializeField] private List<SpriteRect> m_SpriteRects;
        /// <summary>
        /// List of all names in the Name-FileId Table
        /// </summary>
        [SerializeField] private List<string> m_SpriteNames;
        /// <summary>
        /// List of all FileIds in the Name-FileId Table
        /// </summary>
        [SerializeField] private StringGUIDList m_SpriteFileIds;
        /// <summary>
        /// HashSet of all names currently in use by SpriteRects
        /// </summary>
        private HashSet<string> m_NamesInUse;
        private HashSet<GUID> m_InternalIdsInUse;


        public IReadOnlyList<SpriteRect> spriteRects => m_SpriteRects;
        public IReadOnlyList<string> spriteNames => m_SpriteNames;
        public IReadOnlyList<GUID> spriteFileIds => m_SpriteFileIds;

        private SpriteRectModel()
        {
            m_SpriteNames = new List<string>();
            m_SpriteFileIds = new StringGUIDList();
            Clear();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public void SetSpriteRects(List<SpriteRect> newSpriteRects)
        {
            m_SpriteRects = newSpriteRects;
<<<<<<< HEAD
            foreach (var spriteRect in m_SpriteRects)
            {
                m_Names.Add(spriteRect.name);
                m_InternalIds.Add(spriteRect.internalID);
            }
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
=======

            m_NamesInUse = new HashSet<string>();
            m_InternalIdsInUse = new HashSet<GUID>();
            for (var i = 0; i < m_SpriteRects.Count; ++i)
            {
                m_NamesInUse.Add(m_SpriteRects[i].name);
                m_InternalIdsInUse.Add(m_SpriteRects[i].spriteID);
            }
        }

        public void SetNameFileIdPairs(IEnumerable<SpriteNameFileIdPair> pairs)
        {
            m_SpriteNames.Clear();
            m_SpriteFileIds.Clear();

            foreach (var pair in pairs)
                AddNameFileIdPair(pair.name, pair.GetFileGUID());
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public int FindIndex(Predicate<SpriteRect> match)
        {
            int i = 0;
            foreach (var spriteRect in m_SpriteRects)
            {
                if (match.Invoke(spriteRect))
                    return i;
                i++;
            }
            return -1;
        }

        public void Clear()
        {
            m_SpriteRects = new List<SpriteRect>();
<<<<<<< HEAD
            m_InternalIds.Clear();
            m_Names.Clear();
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
        }

        public bool Add(SpriteRect spriteRect)
        {
            if (m_Names.Contains(spriteRect.name))
                return false;
            if (spriteRect.internalID != 0 && m_InternalIds.Contains(spriteRect.internalID))
                return false;
            m_Names.Add(spriteRect.name);
            if (spriteRect.internalID != 0)
                m_InternalIds.Add(spriteRect.internalID);
            m_SpriteRects.Add(spriteRect);
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
=======
            m_NamesInUse = new HashSet<string>();
            m_InternalIdsInUse = new HashSet<GUID>();
        }

        public bool Add(SpriteRect spriteRect, bool shouldReplaceInTable = false)
        {
            if (spriteRect.spriteID.Empty())
            {
                spriteRect.spriteID = GUID.Generate();
            }
            else
            {
                if (IsInternalIdInUsed(spriteRect.spriteID))
                    return false;
            }

            if (shouldReplaceInTable)
            {
                // replace id from sprite to file id table
                if (!UpdateIdInNameIdPair(spriteRect.name, spriteRect.spriteID))
                {
                    // add it into file id table if update wasn't successful i.e. it doesn't exist yet
                    AddNameFileIdPair(spriteRect.name, spriteRect.spriteID);
                }
            }
            else
            {
                // Since we are not replacing the file id table,
                // look for any existing id and set it to the SpriteRect
                var index = m_SpriteNames.FindIndex(x => x == spriteRect.name);
                if (index >= 0)
                {
                    if (IsInternalIdInUsed(m_SpriteFileIds[index]))
                        return false;
                    spriteRect.spriteID = m_SpriteFileIds[index];
                }
                else
                    AddNameFileIdPair(spriteRect.name, spriteRect.spriteID);
            }

            m_SpriteRects.Add(spriteRect);
            m_NamesInUse.Add(spriteRect.name);
            m_InternalIdsInUse.Add(spriteRect.spriteID);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            return true;
        }

        public void Remove(SpriteRect spriteRect)
        {
<<<<<<< HEAD
            m_Names.Remove(spriteRect.name);
            if (spriteRect.internalID != 0)
                m_InternalIds.Remove(spriteRect.internalID);
            m_SpriteRects.Remove(spriteRect);
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
        }

        public bool HasName(string rectName)
        {
            return m_Names.Contains(rectName);
        }

        public bool HasInternalID(long internalID)
        {
            return m_InternalIds.Contains(internalID);
=======
            m_SpriteRects.Remove(spriteRect);
            m_NamesInUse.Remove(spriteRect.name);
            m_InternalIdsInUse.Remove(spriteRect.spriteID);
        }

        /// <summary>
        /// Checks whether or not the name is currently in use by any of the SpriteRects in the texture.
        /// </summary>
        /// <param name="rectName">The name to check for</param>
        /// <returns>True if the name is currently in use</returns>
        public bool IsNameUsed(string rectName)
        {
            return m_NamesInUse.Contains(rectName);
        }

        /// <summary>
        /// Checks whether or not the id is currently in use by any of the SpriteRects in the texture.
        /// </summary>
        /// <param name="rectName">The id to check for</param>
        /// <returns>True if the name is currently in use</returns>
        public bool IsInternalIdInUsed(GUID internalId)
        {
            return m_InternalIdsInUse.Contains(internalId);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public List<SpriteRect> GetSpriteRects()
        {
            return m_SpriteRects;
        }

<<<<<<< HEAD
        public void Rename(string oldName, string newName)
        {
            m_Names.Remove(oldName);
            m_Names.Add(newName);
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
=======
        public bool Rename(string oldName, string newName, GUID fileId)
        {
            if (!IsNameUsed(oldName))
                return false;
            if (IsNameUsed(newName))
                return false;

            var index = m_SpriteNames.FindIndex(x => x == oldName);
            if (index >= 0)
            {
                m_SpriteNames.RemoveAt(index);
                m_SpriteFileIds.RemoveAt(index);
            }

            index = m_SpriteNames.FindIndex(x => x == newName);
            if (index >= 0)
                m_SpriteFileIds[index] = fileId;
            else
                AddNameFileIdPair(newName, fileId);

            m_NamesInUse.Remove(oldName);
            m_NamesInUse.Add(newName);
            return true;
        }

        void AddNameFileIdPair(string spriteName, GUID fileId)
        {
            m_SpriteNames.Add(spriteName);
            m_SpriteFileIds.Add(fileId);
        }

        bool UpdateIdInNameIdPair(string spriteName, GUID newFileId)
        {
            var index = m_SpriteNames.FindIndex(x => x == spriteName);
            if (index >= 0)
            {
                m_SpriteFileIds[index] = newFileId;
                return true;
            }

            return false;
        }

        public void ClearUnusedFileID()
        {
            m_SpriteNames.Clear();
            m_SpriteFileIds.Clear();
            foreach (var sprite in m_SpriteRects)
            {
                m_SpriteNames.Add(sprite.name);
                m_SpriteFileIds.Add(sprite.spriteID);
            }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {}

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
<<<<<<< HEAD
            m_SpriteReadOnlyList = m_SpriteRects.AsReadOnly();
            m_Names.Clear();
            m_InternalIds.Clear();
            foreach (var sprite in m_SpriteReadOnlyList)
            {
                m_Names.Add(sprite.name);
                m_InternalIds.Add(sprite.internalID);
            }
=======
            SetSpriteRects(m_SpriteRects);
        }
    }

    internal class OutlineSpriteRect : SpriteRect
    {
        public List<Vector2[]> outlines;

        public OutlineSpriteRect(SpriteRect rect)
        {
            this.name = rect.name;
            this.originalName = rect.originalName;
            this.pivot = rect.pivot;
            this.alignment = rect.alignment;
            this.border = rect.border;
            this.rect = rect.rect;
            this.spriteID = rect.spriteID;
            outlines = new List<Vector2[]>();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }
    }

    internal abstract partial class SpriteFrameModuleBase : SpriteEditorModuleBase
    {
<<<<<<< HEAD
        protected static UnityType spriteType = UnityType.FindTypeByName("Sprite");

        protected SpriteRectModel m_RectsCache;
        protected ITextureDataProvider m_TextureDataProvider;
        protected ISpriteEditorDataProvider m_SpriteDataProvider;
=======
<<<<<<< HEAD
=======
        [Serializable]
        internal class SpriteFrameModulePersistentState : ScriptableSingleton<SpriteFrameModulePersistentState>
        {
            public PivotUnitMode pivotUnitMode = PivotUnitMode.Normalized;
        }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        protected SpriteRectModel m_RectsCache;
        protected ITextureDataProvider m_TextureDataProvider;
        protected ISpriteEditorDataProvider m_SpriteDataProvider;
        protected ISpriteNameFileIdDataProvider m_NameFileIdDataProvider;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        string m_ModuleName;

        internal enum PivotUnitMode
        {
            Normalized,
            Pixels
        }

<<<<<<< HEAD
        private PivotUnitMode m_PivotUnitMode = PivotUnitMode.Normalized;
=======
<<<<<<< HEAD
        private PivotUnitMode m_PivotUnitMode = PivotUnitMode.Normalized;
=======
        static PivotUnitMode pivotUnitMode
        {
            get => SpriteFrameModulePersistentState.instance.pivotUnitMode;
            set => SpriteFrameModulePersistentState.instance.pivotUnitMode = value;
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        protected SpriteFrameModuleBase(string name, ISpriteEditor sw, IEventSystem es, IUndoSystem us, IAssetDatabase ad)
        {
            spriteEditor = sw;
            eventSystem = es;
            undoSystem = us;
            assetDatabase = ad;
            m_ModuleName = name;
        }

        // implements ISpriteEditorModule

        public override void OnModuleActivate()
        {
            spriteImportMode = SpriteFrameModule.GetSpriteImportMode(spriteEditor.GetDataProvider<ISpriteEditorDataProvider>());
            m_TextureDataProvider = spriteEditor.GetDataProvider<ITextureDataProvider>();
<<<<<<< HEAD
            m_SpriteDataProvider = spriteEditor.GetDataProvider<ISpriteEditorDataProvider>();
=======
            m_NameFileIdDataProvider = spriteEditor.GetDataProvider<ISpriteNameFileIdDataProvider>();
            m_SpriteDataProvider = spriteEditor.GetDataProvider<ISpriteEditorDataProvider>();

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            int width, height;
            m_TextureDataProvider.GetTextureActualWidthAndHeight(out width, out height);
            textureActualWidth = width;
            textureActualHeight = height;
<<<<<<< HEAD
            m_RectsCache = ScriptableObject.CreateInstance<SpriteRectModel>();
            m_RectsCache.hideFlags = HideFlags.HideAndDontSave;
            var spriteList = m_SpriteDataProvider.GetSpriteRects().ToList();
            m_RectsCache.SetSpriteRects(spriteList);
            spriteEditor.spriteRects = spriteList;
            if (spriteEditor.selectedSpriteRect != null)
                spriteEditor.selectedSpriteRect = m_RectsCache.spriteRects.FirstOrDefault(x => x.spriteID == spriteEditor.selectedSpriteRect.spriteID);
=======

            m_RectsCache = ScriptableObject.CreateInstance<SpriteRectModel>();
            m_RectsCache.hideFlags = HideFlags.HideAndDontSave;

            var spriteList = m_SpriteDataProvider.GetSpriteRects().ToList();
            m_RectsCache.SetSpriteRects(spriteList);
            spriteEditor.spriteRects = spriteList;

            if (m_NameFileIdDataProvider == null)
                m_NameFileIdDataProvider = new DefaultSpriteNameFileIdDataProvider(spriteList);
            var nameFileIdPairs = m_NameFileIdDataProvider.GetNameFileIdPairs();
            m_RectsCache.SetNameFileIdPairs(nameFileIdPairs);

            if (spriteEditor.selectedSpriteRect != null)
                spriteEditor.selectedSpriteRect = m_RectsCache.spriteRects.FirstOrDefault(x => x.spriteID == spriteEditor.selectedSpriteRect.spriteID);

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            AddMainUI(spriteEditor.GetMainVisualContainer());
            undoSystem.RegisterUndoCallback(UndoCallback);
        }

        public override void OnModuleDeactivate()
        {
            if (m_RectsCache != null)
            {
                undoSystem.ClearUndo(m_RectsCache);
                ScriptableObject.DestroyImmediate(m_RectsCache);
                m_RectsCache = null;
            }
            undoSystem.UnregisterUndoCallback(UndoCallback);
            RemoveMainUI(spriteEditor.GetMainVisualContainer());
        }

        public override bool ApplyRevert(bool apply)
        {
            if (apply)
            {
<<<<<<< HEAD
                if (containsMultipleSprites)
                {
                    var oldNames = new List<string>();
                    var newNames = new List<string>();
                    var ids = new List<long>();
                    var names = new List<string>();

                    foreach (var spriteRect in m_RectsCache.spriteRects)
                    {
                        if (string.IsNullOrEmpty(spriteRect.name))
                            spriteRect.name = "Empty";

                        if (!string.IsNullOrEmpty(spriteRect.originalName))
                        {
                            oldNames.Add(spriteRect.originalName);
                            newNames.Add(spriteRect.name);
                        }

                        if (spriteRect.m_RegisterInternalID)
                        {
                            ids.Add(spriteRect.internalID);
                            names.Add(spriteRect.name);
                        }
                        spriteRect.m_RegisterInternalID = false;
                    }
                    var so = new SerializedObject(m_SpriteDataProvider.targetObject);
                    if (so.isValid && ids.Count > 0)
                    {
                        ImportSettingInternalID.RegisterInternalID(so, spriteType, ids, names);
                        so.ApplyModifiedPropertiesWithoutUndo();
                    }

                    AssetImporter assetImporter = m_SpriteDataProvider.targetObject as AssetImporter;
                    if (oldNames.Count > 0 && assetImporter != null)
                    {
                        assetImporter.RenameSubAssets(spriteType.persistentTypeID, oldNames.ToArray(), newNames.ToArray());
                        so.ApplyModifiedPropertiesWithoutUndo();
                    }
                }
                var array = m_RectsCache != null ? m_RectsCache.spriteRects.ToArray() : null;
                m_SpriteDataProvider.SetSpriteRects(array);
=======
                var array = m_RectsCache != null ? m_RectsCache.spriteRects.ToArray() : null;
                m_SpriteDataProvider.SetSpriteRects(array);

                var spriteNames = m_RectsCache?.spriteNames;
                var spriteFileIds = m_RectsCache?.spriteFileIds;
                if (spriteNames != null && spriteFileIds != null)
                {
                    var pairList = new List<SpriteNameFileIdPair>(spriteNames.Count);
                    for (var i = 0; i < spriteNames.Count; ++i)
                        pairList.Add(new SpriteNameFileIdPair(spriteNames[i], spriteFileIds[i]));
                    m_NameFileIdDataProvider.SetNameFileIdPairs(pairList.ToArray());
                }

                var outlineDataProvider = m_SpriteDataProvider.GetDataProvider<ISpriteOutlineDataProvider>();
                var physicsDataProvider = m_SpriteDataProvider.GetDataProvider<ISpritePhysicsOutlineDataProvider>();
                foreach (var rect in array)
                {
                    if (rect is OutlineSpriteRect outlineRect)
                    {
                        if (outlineRect.outlines.Count > 0)
                        {
                            outlineDataProvider.SetOutlines(outlineRect.spriteID, outlineRect.outlines);
                            physicsDataProvider.SetOutlines(outlineRect.spriteID, outlineRect.outlines);
                        }
                    }
                }

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                if (m_RectsCache != null)
                    undoSystem.ClearUndo(m_RectsCache);
            }
            else
            {
                if (m_RectsCache != null)
                {
                    undoSystem.ClearUndo(m_RectsCache);
<<<<<<< HEAD
                    var spriteList = m_SpriteDataProvider.GetSpriteRects().ToList();
                    m_RectsCache.SetSpriteRects(spriteList);
=======

                    var spriteList = m_SpriteDataProvider.GetSpriteRects().ToList();
                    m_RectsCache.SetSpriteRects(spriteList);

                    var nameFileIdPairs = m_NameFileIdDataProvider.GetNameFileIdPairs();
                    m_RectsCache.SetNameFileIdPairs(nameFileIdPairs);

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    spriteEditor.spriteRects = spriteList;
                    if (spriteEditor.selectedSpriteRect != null)
                        spriteEditor.selectedSpriteRect = m_RectsCache.spriteRects.FirstOrDefault(x => x.spriteID == spriteEditor.selectedSpriteRect.spriteID);
                }
            }

            return true;
        }

        public override string moduleName
        {
            get { return m_ModuleName; }
        }

        // injected interfaces
        protected IEventSystem eventSystem
        {
            get;
            private set;
        }

        protected IUndoSystem undoSystem
        {
            get;
            private set;
        }

        protected IAssetDatabase assetDatabase
        {
            get;
            private set;
        }

        protected SpriteRect selected
        {
            get { return spriteEditor.selectedSpriteRect; }
            set { spriteEditor.selectedSpriteRect = value; }
        }

        protected SpriteImportMode spriteImportMode
        {
            get; private set;
        }

        protected string spriteAssetPath
        {
            get { return assetDatabase.GetAssetPath(m_TextureDataProvider.texture); }
        }

        public bool hasSelected
        {
            get { return spriteEditor.selectedSpriteRect != null; }
        }

        public SpriteAlignment selectedSpriteAlignment
        {
            get { return selected.alignment; }
        }

        public Vector2 selectedSpritePivot
        {
            get { return selected.pivot; }
        }

        private Vector2 selectedSpritePivotInCurUnitMode
        {
            get
            {
<<<<<<< HEAD
                return m_PivotUnitMode == PivotUnitMode.Pixels
=======
<<<<<<< HEAD
                return m_PivotUnitMode == PivotUnitMode.Pixels
=======
                return pivotUnitMode == PivotUnitMode.Pixels
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    ? ConvertFromNormalizedToRectSpace(selectedSpritePivot, selectedSpriteRect)
                    : selectedSpritePivot;
            }
        }

        public int CurrentSelectedSpriteIndex()
        {
            if (m_RectsCache != null && selected != null)
                return m_RectsCache.FindIndex(x => x.spriteID == selected.spriteID);
            return -1;
        }

        public Vector4 selectedSpriteBorder
        {
            get { return ClampSpriteBorderToRect(selected.border, selected.rect); }
            set
            {
                undoSystem.RegisterCompleteObjectUndo(m_RectsCache, "Change Sprite Border");
                spriteEditor.SetDataModified();
                selected.border = ClampSpriteBorderToRect(value, selected.rect);
            }
        }

        public Rect selectedSpriteRect
        {
            get { return selected.rect; }
            set
            {
                undoSystem.RegisterCompleteObjectUndo(m_RectsCache, "Change Sprite rect");
                spriteEditor.SetDataModified();
                selected.rect = ClampSpriteRect(value, textureActualWidth, textureActualHeight);
            }
        }

        public string selectedSpriteName
        {
            get { return selected.name; }
            set
            {
                if (selected.name == value)
                    return;
<<<<<<< HEAD
                if (m_RectsCache.HasName(value))
                    return;

=======
                if (m_RectsCache.IsNameUsed(value))
                    return;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                undoSystem.RegisterCompleteObjectUndo(m_RectsCache, "Change Sprite Name");
                spriteEditor.SetDataModified();

                string oldName = selected.name;
                string newName = InternalEditorUtility.RemoveInvalidCharsFromFileName(value, true);

                // These can only be changed in sprite multiple mode
                if (string.IsNullOrEmpty(selected.originalName) && (newName != oldName))
                    selected.originalName = oldName;

                // Is the name empty?
                if (string.IsNullOrEmpty(newName))
                    newName = oldName;

<<<<<<< HEAD
                m_RectsCache.Rename(oldName, newName);
                selected.name = newName;
=======
                // Did the rename succeed?
                if (m_RectsCache.Rename(oldName, newName, selected.spriteID))
                    selected.name = newName;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        public int spriteCount
        {
            get { return m_RectsCache.spriteRects.Count; }
        }

        public Vector4 GetSpriteBorderAt(int i)
        {
            return m_RectsCache.spriteRects[i].border;
        }

        public Rect GetSpriteRectAt(int i)
        {
            return m_RectsCache.spriteRects[i].rect;
        }

        public int textureActualWidth { get; private set; }
        public int textureActualHeight { get; private set; }

        public void SetSpritePivotAndAlignment(Vector2 pivot, SpriteAlignment alignment)
        {
            undoSystem.RegisterCompleteObjectUndo(m_RectsCache, "Change Sprite Pivot");
            spriteEditor.SetDataModified();
            selected.alignment = alignment;
            selected.pivot = SpriteEditorUtility.GetPivotValue(alignment, pivot);
        }

        public bool containsMultipleSprites
        {
            get { return spriteImportMode == SpriteImportMode.Multiple; }
        }

        protected void SnapPivotToSnapPoints(Vector2 pivot, out Vector2 outPivot, out SpriteAlignment outAlignment)
        {
            Rect rect = selectedSpriteRect;

            // Convert from normalized space to texture space
            Vector2 texturePos = new Vector2(rect.xMin + rect.width * pivot.x, rect.yMin + rect.height * pivot.y);

            Vector2[] snapPoints = GetSnapPointsArray(rect);

            // Snapping is now a firm action, it will always snap to one of the snapping points.
            SpriteAlignment snappedAlignment = SpriteAlignment.Custom;
            float nearestDistance = float.MaxValue;
            for (int alignment = 0; alignment < snapPoints.Length; alignment++)
            {
                float distance = (texturePos - snapPoints[alignment]).magnitude * m_Zoom;
                if (distance < nearestDistance)
                {
                    snappedAlignment = (SpriteAlignment)alignment;
                    nearestDistance = distance;
                }
            }

            outAlignment = snappedAlignment;
            outPivot = ConvertFromTextureToNormalizedSpace(snapPoints[(int)snappedAlignment], rect);
        }

        protected void SnapPivotToPixels(Vector2 pivot, out Vector2 outPivot, out SpriteAlignment outAlignment)
        {
            outAlignment = SpriteAlignment.Custom;

            Rect rect = selectedSpriteRect;
            float unitsPerPixelX = 1.0f / rect.width;
            float unitsPerPixelY = 1.0f / rect.height;
            outPivot.x = Mathf.Round(pivot.x / unitsPerPixelX) * unitsPerPixelX;
            outPivot.y = Mathf.Round(pivot.y / unitsPerPixelY) * unitsPerPixelY;
        }

        private void UndoCallback()
        {
            UIUndoCallback();
        }

        protected static Rect ClampSpriteRect(Rect rect, float maxX, float maxY)
        {
            // Clamp rect to width height
            rect = FlipNegativeRect(rect);
            Rect newRect = new Rect();

            newRect.xMin = Mathf.Clamp(rect.xMin, 0, maxX - 1);
            newRect.yMin = Mathf.Clamp(rect.yMin, 0, maxY - 1);
            newRect.xMax = Mathf.Clamp(rect.xMax, 1, maxX);
            newRect.yMax = Mathf.Clamp(rect.yMax, 1, maxY);

            // Prevent width and height to be 0 value after clamping.
            if (Mathf.RoundToInt(newRect.width) == 0)
                newRect.width = 1;
            if (Mathf.RoundToInt(newRect.height) == 0)
                newRect.height = 1;

            return SpriteEditorUtility.RoundedRect(newRect);
        }

        protected static Rect FlipNegativeRect(Rect rect)
        {
            Rect newRect = new Rect();

            newRect.xMin = Mathf.Min(rect.xMin, rect.xMax);
            newRect.yMin = Mathf.Min(rect.yMin, rect.yMax);
            newRect.xMax = Mathf.Max(rect.xMin, rect.xMax);
            newRect.yMax = Mathf.Max(rect.yMin, rect.yMax);

            return newRect;
        }

        protected static Vector4 ClampSpriteBorderToRect(Vector4 border, Rect rect)
        {
            Rect flipRect = FlipNegativeRect(rect);
            float w = flipRect.width;
            float h = flipRect.height;

            Vector4 newBorder = new Vector4();

            // Make sure borders are within the width/height and left < right and top < bottom
            newBorder.x = Mathf.RoundToInt(Mathf.Clamp(border.x, 0, Mathf.Min(Mathf.Abs(w - border.z), w))); // Left
            newBorder.z = Mathf.RoundToInt(Mathf.Clamp(border.z, 0, Mathf.Min(Mathf.Abs(w - newBorder.x), w))); // Right

            newBorder.y = Mathf.RoundToInt(Mathf.Clamp(border.y, 0, Mathf.Min(Mathf.Abs(h - border.w), h))); // Bottom
            newBorder.w = Mathf.RoundToInt(Mathf.Clamp(border.w, 0, Mathf.Min(Mathf.Abs(h - newBorder.y), h))); // Top

            return newBorder;
        }
    }
}
