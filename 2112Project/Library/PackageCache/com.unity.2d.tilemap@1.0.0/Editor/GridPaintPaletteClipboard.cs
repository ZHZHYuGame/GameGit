using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using UnityEditorInternal;
=======
<<<<<<< HEAD
using System.Linq;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using UnityEngine;
using UnityEngine.Tilemaps;
using Event = UnityEngine.Event;
using Object = UnityEngine.Object;

namespace UnityEditor.Tilemaps
{
<<<<<<< HEAD
=======
=======
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Event = UnityEngine.Event;

namespace UnityEditor.Tilemaps
{
    [Serializable]
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    internal class GridPaintPaletteClipboard : PaintableGrid
    {
        static class Styles
        {
<<<<<<< HEAD
            public static readonly GUIStyle background = "CurveEditorBackground";
        }

=======
<<<<<<< HEAD
            public static readonly GUIStyle background = "CurveEditorBackground";
        }

=======
            public static readonly GUIContent emptyProjectInfo = EditorGUIUtility.TrTextContent("Create a new palette in the dropdown above.");
            public static readonly GUIContent emptyPaletteInfo = EditorGUIUtility.TrTextContent("Drag Tile, Sprite or Sprite Texture assets here.");
            public static readonly GUIContent invalidPaletteInfo = EditorGUIUtility.TrTextContent("This is an invalid palette. Did you delete the palette asset?");
            public static readonly GUIContent invalidGridInfo = EditorGUIUtility.TrTextContent("The palette has an invalid Grid. Did you add a Grid to the palette asset?");
        }



        private static List<GridPaintPaletteClipboard> s_Instances;
        public static List<GridPaintPaletteClipboard> instances
        {
            get
            {
                if (s_Instances == null)
                    s_Instances = new List<GridPaintPaletteClipboard>();
                return s_Instances;
            }
        }

        private bool disableOnBrushPicked;
        public event Action onBrushPicked;

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        private static readonly string paletteSavedOutsideClipboard = L10n.Tr("Palette Asset {0} was changed outside of the Tile Palette. All changes in the Tile Palette made will be reverted.");

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private bool m_PaletteNeedsSave;
        private const float k_ZoomSpeed = 7f;
        private const float k_MinZoom = 10f; // How many pixels per cell at minimum
        private const float k_MaxZoom = 100f; // How many pixels per cell at maximum
        private const float k_Padding = 0.75f; // How many percentages of window size is the empty padding around the palette content

        private int m_KeyboardPanningID;
        private int m_MousePanningID;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        private Vector2 m_MouseZoomInitialPosition;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        private float k_KeyboardPanningSpeed = 3.0f;

        private Vector3 m_KeyboardPanning;

        private Rect m_GUIRect = new Rect(0, 0, 200, 200);

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private bool m_OldFog;

        public Rect guiRect
        {
            get { return m_GUIRect; }
            set
            {
                if (m_GUIRect != value)
                {
                    Rect oldValue = m_GUIRect;
                    m_GUIRect = value;
                    OnViewSizeChanged(oldValue, m_GUIRect);
                }
            }
        }

        [SerializeField] private GridPaintPaletteWindow m_Owner;
<<<<<<< HEAD
=======
=======
        public Rect guiRect
        {
            get => m_GUIRect;
            set
            {
                if (m_GUIRect == value)
                    return;
                var oldValue = m_GUIRect;
                m_GUIRect = value;
                OnViewSizeChanged(oldValue, m_GUIRect);
            }
        }

        private VisualElement m_VisualElement;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        public bool activeDragAndDrop { get { return DragAndDrop.objectReferences.Length > 0 && guiRect.Contains(Event.current.mousePosition); } }

        [SerializeField] private bool m_CameraInitializedToBounds;
        [SerializeField] public bool m_CameraPositionSaved;
        [SerializeField] public Vector3 m_CameraPosition;
        [SerializeField] public float m_CameraOrthographicSize;
<<<<<<< HEAD

        private RectInt? m_ActivePick;
        private Dictionary<Vector2Int, Object> m_HoverData;
=======
<<<<<<< HEAD

        private RectInt? m_ActivePick;
        private Dictionary<Vector2Int, TileDragAndDropHoverData> m_HoverData;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private bool m_Unlocked;
        private bool m_PingTileAsset;

        public GameObject palette { get { return m_Owner.palette; } }
        public GameObject paletteInstance { get { return m_Owner.paletteInstance; } }
        public Tilemap tilemap { get { return paletteInstance != null ? paletteInstance.GetComponentInChildren<Tilemap>() : null; } }
        private Grid grid { get { return paletteInstance != null ? paletteInstance.GetComponent<Grid>() : null; } }
        private Grid prefabGrid { get { return palette != null ? palette.GetComponent<Grid>() : null; } }
        public PreviewRenderUtility previewUtility { get { return m_Owner.previewUtility; } }

        private GridBrushBase gridBrush { get { return GridPaintingState.gridBrush; } }
<<<<<<< HEAD
=======
=======
        [SerializeField] public GridLayout.CellSwizzle m_CameraSwizzleView;

        private BoundsInt? m_ActivePick;
        private Vector3Int m_ActivePivot;
        private Dictionary<Vector2Int, TileDragAndDropHoverData> m_HoverData;
        private bool m_Unlocked;

        private GameObject palette => GridPaintingState.palette;
        private GridBrushBase gridBrush => GridPaintingState.gridBrush;

        private PreviewRenderUtility m_PreviewUtility;

        internal Vector3 cameraPosition
        {
            get => m_PreviewUtility.camera.transform.position;
            set
            {
                m_PreviewUtility.camera.transform.position = value;
                ClampZoomAndPan();
            }
        }

        internal float cameraSize
        {
            get => m_PreviewUtility.camera.orthographicSize;
            set
            {
                m_PreviewUtility.camera.orthographicSize = value;
                ClampZoomAndPan();
            }
        }

        internal TransparencySortMode cameraTransparencySortMode
        {
            get => m_PreviewUtility.camera.transparencySortMode;
            set => m_PreviewUtility.camera.transparencySortMode = value;
        }

        internal Vector3 cameraTransparencySortAxis
        {
            get => m_PreviewUtility.camera.transparencySortAxis;
            set => m_PreviewUtility.camera.transparencySortAxis = value;
        }

        [SerializeField] private GameObject m_PaletteInstance;

        internal GameObject paletteInstance
        {
            get
            {
                if (m_PaletteInstance == null && palette != null && m_PreviewUtility != null)
                    ResetPreviewInstance();
                return m_PaletteInstance;
            }
        }

        private Tilemap tilemap { get { return paletteInstance != null ? paletteInstance.GetComponentInChildren<Tilemap>() : null; } }
        private Grid grid { get { return paletteInstance != null ? paletteInstance.GetComponent<Grid>() : null; } }
        private Grid prefabGrid { get { return palette != null ? palette.GetComponent<Grid>() : null; } }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        private Mesh m_GridMesh;
        private int m_LastGridHash;
        private Material m_GridMaterial;
        private static readonly Color k_GridColor = Color.white.AlphaMultiplied(0.1f);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        private static readonly PrefColor tilePaletteBackgroundColor = new PrefColor("2D/Tile Palette Background"
            , 118.0f / 255.0f // Light
            , 118.0f / 255.0f
            , 118.0f / 255.0f
            , 127.0f / 255.0f
            , 31.0f / 255.0f // Dark
            , 31.0f / 255.0f
            , 31.0f / 255.0f
            , 127.0f / 255.0f);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        private bool m_PaletteUsed; // We mark palette used, when it has been changed in any way during being actively open.
        private Vector2? m_PreviousMousePosition;

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        private bool m_DelayedResetPaletteInstance;
        internal void DelayedResetPreviewInstance()
        {
            m_DelayedResetPaletteInstance = true;
        }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public TileBase activeTile
        {
            get
            {
<<<<<<< HEAD
                if (m_ActivePick.HasValue && m_ActivePick.Value.size == Vector2Int.one && GridPaintingState.defaultBrush != null && GridPaintingState.defaultBrush.cellCount > 0)
=======
<<<<<<< HEAD
                if (m_ActivePick.HasValue && m_ActivePick.Value.size == Vector2Int.one && GridPaintingState.defaultBrush != null && GridPaintingState.defaultBrush.cellCount > 0)
=======
                if (m_ActivePick.HasValue && m_ActivePick.Value.size == Vector3Int.one && GridPaintingState.defaultBrush != null && GridPaintingState.defaultBrush.cellCount > 0)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    return GridPaintingState.defaultBrush.cells[0].tile;
                return null;
            }
        }

<<<<<<< HEAD
        // TODO: Faster codepath for this
=======
<<<<<<< HEAD
        // TODO: Faster codepath for this
=======
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private RectInt bounds
        {
            get
            {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                if (tilemap == null)
                    return new RectInt();

                var origin = tilemap.origin;
                var size = tilemap.size;

                RectInt r = new RectInt(origin.x, origin.y, size.x, size.y);
                if (TilemapIsEmpty(tilemap))
                    return r;

                int minX = origin.x + size.x;
                int minY = origin.y + size.y;
                int maxX = origin.x;
                int maxY = origin.y;

                foreach (Vector2Int pos in r.allPositionsWithin)
                {
                    if (tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0)) != null)
                    {
                        minX = Math.Min(minX, pos.x);
                        minY = Math.Min(minY, pos.y);
                        maxX = Math.Max(maxX, pos.x);
                        maxY = Math.Max(maxY, pos.y);
                    }
                }
                return new RectInt(minX, minY, maxX - minX + 1, maxY - minY + 1);
<<<<<<< HEAD
=======
=======
                RectInt r = default;
                if (tilemap == null || TilemapIsEmpty(tilemap))
                    return r;

                tilemap.CompressBounds();
                var origin = tilemap.origin;
                var size = tilemap.size;
                r = new RectInt(origin.x, origin.y, size.x, size.y);
                return r;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        // Max area we are ever showing. Depends on the zoom level and content of palette.
        private Rect paddedBounds
        {
            get
            {
<<<<<<< HEAD
                float GUIAspect = m_GUIRect.width / m_GUIRect.height;
                float paddingW = previewUtility.camera.orthographicSize * GUIAspect * k_Padding * 2f;
                float paddingH = previewUtility.camera.orthographicSize * k_Padding * 2f;
=======
                var GUIAspect = m_GUIRect.width / m_GUIRect.height;
<<<<<<< HEAD
                var orthographicSize = previewUtility.camera.orthographicSize;
=======
                var orthographicSize = m_PreviewUtility.camera.orthographicSize;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                var paddingW = orthographicSize * GUIAspect * k_Padding * 2f;
                var paddingH = orthographicSize * k_Padding * 2f;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

                Bounds localBounds = grid.GetBoundsLocal(
                    new Vector3(bounds.xMin, bounds.yMin, 0.0f),
                    new Vector3(bounds.size.x, bounds.size.y, 0.0f));
                Rect result = new Rect(
                    new Vector2(localBounds.min.x - paddingW, localBounds.min.y - paddingH),
                    new Vector2(localBounds.size.x + paddingW * 2f, localBounds.size.y + paddingH * 2f));

                return result;
            }
        }

        private RectInt paddedBoundsInt
        {
            get
            {
<<<<<<< HEAD
                Vector3Int min = grid.LocalToCell(paddedBounds.min);
                Vector3Int max = grid.LocalToCell(paddedBounds.max) + Vector3Int.one;
=======
<<<<<<< HEAD
                Vector3Int min = grid.LocalToCell(paddedBounds.min);
                Vector3Int max = grid.LocalToCell(paddedBounds.max) + Vector3Int.one;
=======
                var min = Vector3Int.FloorToInt(paddedBounds.min);
                var max = Vector3Int.CeilToInt(paddedBounds.max);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                return new RectInt(min.x, min.y, max.x - min.x, max.y - min.y);
            }
        }

        private GameObject brushTarget
        {
            get
            {
<<<<<<< HEAD
                return (tilemap != null) ? tilemap.gameObject : null;
=======
                return (tilemap != null) ? tilemap.gameObject : (grid != null) ? grid.gameObject : null;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        public bool unlocked
        {
            get { return m_Unlocked; }
            set
            {
<<<<<<< HEAD
                if (value == false && m_Unlocked && tilemap != null)
                {
                    tilemap.ClearAllEditorPreviewTiles();
                    SavePaletteIfNecessary();
                }
                m_Unlocked = value;
            }
        }
=======
                if (value == false && m_Unlocked)
                {
                    if (tilemap != null)
                        tilemap.ClearAllEditorPreviewTiles();
                    SavePaletteIfNecessary();
                }
                m_Unlocked = value;
<<<<<<< HEAD
            }
        }

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public bool pingTileAsset
        {
            get { return m_PingTileAsset; }
            set
            {
                if (value && !m_PingTileAsset && m_ActivePick.HasValue) { PingTileAsset(m_ActivePick.Value); }
                m_PingTileAsset = value;
            }
        }
<<<<<<< HEAD
        public bool invalidClipboard { get { return m_Owner.palette == null; } }
=======

        public bool invalidClipboard { get { return m_Owner.palette == null; } }
=======
                unlockedChanged?.Invoke(m_Unlocked);
            }
        }
        public event Action<bool> unlockedChanged;

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public bool isReceivingDragAndDrop { get { return m_HoverData != null && m_HoverData.Count > 0; } }

        public bool showNewEmptyClipboardInfo
        {
            get
            {
                if (paletteInstance == null)
                    return false;

                if (tilemap == null)
                    return false;

<<<<<<< HEAD
                if (!TilemapIsEmpty(tilemap))
                    return false;

=======
                if (unlocked && inEditMode)
                    return false;

                if (!TilemapIsEmpty(tilemap))
                    return false;

                if (tilemap.transform.childCount > 0)
                    return false;

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                if (isReceivingDragAndDrop)
                    return false;

                // If user happens to erase the last content of used palette, we don't want to show the new palette info anymore
                if (m_PaletteUsed)
                    return false;

                return true;
            }
        }

        public bool isModified { get { return m_PaletteNeedsSave; } }

<<<<<<< HEAD
        public GridPaintPaletteWindow owner
        {
            set { m_Owner = value; }
=======
<<<<<<< HEAD
        public GridPaintPaletteWindow owner
        {
            set { m_Owner = value; }
=======
        public VisualElement attachedVisualElement
        {
            set { m_VisualElement = value; }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public void OnBeforePaletteSelectionChanged()
        {
            SavePaletteIfNecessary();
            DestroyPreviewInstance();
            FlushHoverData();
        }

        private void FlushHoverData()
        {
            if (m_HoverData != null)
            {
                m_HoverData.Clear();
                m_HoverData = null;
            }
        }

        public void OnAfterPaletteSelectionChanged()
        {
            m_PaletteUsed = false;
            ResetPreviewInstance();

            if (palette != null)
                ResetPreviewCamera();
        }

        public void SetupPreviewCameraOnInit()
        {
            if (m_CameraPositionSaved)
                LoadSavedCameraPosition();
            else
                ResetPreviewCamera();
        }

        private void LoadSavedCameraPosition()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            previewUtility.camera.transform.position = m_CameraPosition;
            previewUtility.camera.orthographicSize = m_CameraOrthographicSize;
            previewUtility.camera.nearClipPlane = 0.01f;
            previewUtility.camera.farClipPlane = 100f;
<<<<<<< HEAD
=======
=======
            m_PreviewUtility.camera.transform.position = m_CameraPosition;
            m_PreviewUtility.camera.orthographicSize = m_CameraOrthographicSize;
            m_PreviewUtility.camera.nearClipPlane = 0.01f;
            m_PreviewUtility.camera.farClipPlane = 100f;
        }

        private Vector3 GetCameraPositionFromXYZ(Vector3 xyzPosition)
        {
            var position = Grid.Swizzle(m_CameraSwizzleView, xyzPosition);
            position = GetCameraPosition(position);
            return position;
        }

        private Vector3 GetCameraPosition(Vector3 xyzPosition)
        {
            var position = xyzPosition;
            switch (m_CameraSwizzleView)
            {
                case GridLayout.CellSwizzle.XZY:
                    {
                        position.y = 10f;
                    }
                    break;
                case GridLayout.CellSwizzle.YZX:
                    {
                        position.y = -10f;
                    }
                    break;
                case GridLayout.CellSwizzle.ZYX:
                    {
                        position.x = 10f;
                    }
                    break;
                case GridLayout.CellSwizzle.ZXY:
                    {
                        position.x = -10f;
                    }
                    break;
                case GridLayout.CellSwizzle.YXZ:
                    {
                        position.z = 10f;
                    }
                    break;
                case GridLayout.CellSwizzle.XYZ:
                default:
                    {
                        position.z = -10f;
                    }
                    break;
            }
            return position;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private void ResetPreviewCamera()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            var transform = previewUtility.camera.transform;
            transform.position = new Vector3(0, 0, -10f);
            transform.rotation = Quaternion.identity;
            previewUtility.camera.nearClipPlane = 0.01f;
            previewUtility.camera.farClipPlane = 100f;
            FrameEntirePalette();
        }

        private void DestroyPreviewInstance()
        {
            if (m_Owner != null)
                m_Owner.DestroyPreviewInstance();
        }

        private void ResetPreviewInstance()
        {
            m_Owner.ResetPreviewInstance();
        }

        public void ResetPreviewMesh()
<<<<<<< HEAD
=======
=======
            var transform = m_PreviewUtility.camera.transform;

            transform.position = GetCameraPositionFromXYZ(Vector3.zero);
            switch (m_CameraSwizzleView)
            {
                case GridLayout.CellSwizzle.XZY:
                    {
                        transform.rotation = Quaternion.LookRotation(new Vector3(0, -1, 0), new Vector3(0, 0, 1));
                    }
                    break;
                case GridLayout.CellSwizzle.YZX:
                    {
                        transform.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0), new Vector3(1, 0, 0));
                    }
                    break;
                case GridLayout.CellSwizzle.ZXY:
                    {
                        transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0), new Vector3(0, 0, 1));
                    }
                    break;
                case GridLayout.CellSwizzle.ZYX:
                    {
                        transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0), new Vector3(0, 1, 0));
                    }
                    break;
                case GridLayout.CellSwizzle.YXZ:
                    {
                        transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1), new Vector3(1, 0, 0));
                    }
                    break;
                case GridLayout.CellSwizzle.XYZ:
                default:
                    {
                        transform.rotation = Quaternion.identity;
                    }
                    break;
            }

            m_PreviewUtility.camera.nearClipPlane = 0.01f;
            m_PreviewUtility.camera.farClipPlane = 100f;

            FrameEntirePalette();
        }

        public void InitPreviewUtility()
        {
            m_PreviewUtility = new PreviewRenderUtility(true, true);
            m_PreviewUtility.camera.orthographic = true;
            m_PreviewUtility.camera.orthographicSize = 5f;
            m_PreviewUtility.camera.transform.position = new Vector3(0f, 0f, -10f);
            m_PreviewUtility.ambientColor = new Color(1f, 1f, 1f, 0);
        }

        public void ResetPreviewInstance()
        {
            // Store GridSelection for current Palette Instance
            Stack<int> childPositions = null;
            BoundsInt previousGridSelectionPosition = default;
            if (m_PaletteInstance != null && GridSelection.active && GridSelection.target.transform.IsChildOf(m_PaletteInstance.transform))
            {
                childPositions = new Stack<int>();
                var transform = GridSelection.target.transform;
                while (transform != null && transform != m_PaletteInstance.transform)
                {
                    childPositions.Push(transform.GetSiblingIndex());
                    transform = transform.parent;
                }
                previousGridSelectionPosition = GridSelection.position;
                ClearGridSelection();
            }

            DestroyPreviewInstance();
            if (palette != null)
            {
                m_PaletteInstance = m_PreviewUtility.InstantiatePrefabInScene(palette);

                // Disconnecting prefabs is no longer possible.
                // If performance of overrides on palette palette instance turns out to be a problem.
                // unpack the prefab instance here, and overwrite the prefab later instead of reconnecting.
                PrefabUtility.UnpackPrefabInstance(m_PaletteInstance, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

                EditorUtility.InitInstantiatedPreviewRecursive(m_PaletteInstance);
                m_PaletteInstance.transform.position = new Vector3(0, 0, 0);
                m_PaletteInstance.transform.rotation = Quaternion.identity;
                m_PaletteInstance.transform.localScale = Vector3.one;

                var paletteAsset = GridPaletteUtility.GetGridPaletteFromPaletteAsset(palette);
                if (paletteAsset != null)
                {
                    // Handle Cell Sizing for Palette
                    if (paletteAsset.cellSizing == GridPalette.CellSizing.Automatic)
                    {
                        var paletteGrid = m_PaletteInstance.GetComponent<Grid>();
                        if (paletteGrid != null)
                        {
                            paletteGrid.cellSize = GridPaletteUtility.CalculateAutoCellSize(paletteGrid, paletteGrid.cellSize);
                        }
                        else
                        {
                            Debug.LogWarning("Grid component not found from: " + palette.name);
                        }
                    }

                    // Handle Transparency Sort Settings
                    m_PreviewUtility.camera.transparencySortMode = paletteAsset.transparencySortMode;
                    m_PreviewUtility.camera.transparencySortAxis = paletteAsset.transparencySortAxis;

                    // Handle Camera View for Grid
                    m_CameraSwizzleView = GridLayout.CellSwizzle.XYZ;
                    var instanceGrid = m_PaletteInstance.GetComponent<Grid>();
                    if (instanceGrid != null)
                        m_CameraSwizzleView = instanceGrid.cellSwizzle;
                }
                else
                {
                    Debug.LogWarning("GridPalette subasset not found from: " + palette.name);
                    m_PreviewUtility.camera.transparencySortMode = TransparencySortMode.Default;
                    m_PreviewUtility.camera.transparencySortAxis = new Vector3(0f, 0f, 1f);
                }

                foreach (var transform in m_PaletteInstance.GetComponentsInChildren<Transform>())
                    transform.gameObject.hideFlags = HideFlags.HideAndDontSave;

                // Show all renderers from Palettes from previous versions
                PreviewRenderUtility.SetEnabledRecursive(m_PaletteInstance, true);

                // Update preview Grid Mesh for new palette instance
                ResetPreviewGridMesh();

                // Restore GridSelection for new palette instance
                if (childPositions != null)
                {
                    var transform = m_PaletteInstance.transform;
                    while (childPositions.Count > 0)
                    {
                        var siblingIndex = childPositions.Pop();
                        if (siblingIndex < transform.childCount)
                            transform = transform.GetChild(siblingIndex);
                    }
                    GridSelection.Select(transform.gameObject, previousGridSelectionPosition);
                }
            }

            m_DelayedResetPaletteInstance = false;
        }

        public void DestroyPreviewInstance()
        {
            if (m_PaletteInstance != null)
            {
                Undo.ClearUndo(m_PaletteInstance);
                if (GridSelection.active && GridSelection.target == tilemap.gameObject)
                {
                    GridSelection.TransferToStandalone(palette);
                }
                else
                {
                    DestroyImmediate(m_PaletteInstance);
                }
                m_PaletteInstance = null;
            }
        }

        private void ResetPreviewGridMesh()
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (m_GridMesh != null)
            {
                DestroyImmediate(m_GridMesh);
                m_GridMesh = null;
            }
            m_GridMaterial = null;
        }

        public void FrameEntirePalette()
        {
            Frame(bounds);
        }

<<<<<<< HEAD
        void Frame(RectInt rect)
=======
<<<<<<< HEAD
        void Frame(RectInt rect)
=======
        private void Frame(RectInt rect)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (grid == null)
                return;

<<<<<<< HEAD
            previewUtility.camera.transform.position = grid.CellToLocalInterpolated(new Vector3(rect.center.x, rect.center.y, 0));
            previewUtility.camera.transform.position.Set(previewUtility.camera.transform.position.x, previewUtility.camera.transform.position.y, -10f);
=======
            var position = grid.CellToLocalInterpolated(new Vector3(rect.center.x, rect.center.y, 0));
<<<<<<< HEAD
            position.z = -10f;
            previewUtility.camera.transform.position = position;
=======
            position = GetCameraPosition(position);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            var height = (grid.CellToLocal(new Vector3Int(0, rect.yMax, 0)) - grid.CellToLocal(new Vector3Int(0, rect.yMin, 0))).magnitude;
            var width = (grid.CellToLocal(new Vector3Int(rect.xMax, 0, 0)) - grid.CellToLocal(new Vector3Int(rect.xMin, 0, 0))).magnitude;

            var cellSize = grid.cellSize;
            width += cellSize.x;
            height += cellSize.y;

            var GUIAspect = m_GUIRect.width / m_GUIRect.height;
            var contentAspect = width / height;
<<<<<<< HEAD
            previewUtility.camera.orthographicSize = (GUIAspect > contentAspect ? height : width / GUIAspect) / 2f;
=======
<<<<<<< HEAD
            previewUtility.camera.orthographicSize = (GUIAspect > contentAspect ? height : width / GUIAspect) / 2f;
=======

            m_PreviewUtility.camera.transform.position = position;
            m_PreviewUtility.camera.orthographicSize = (GUIAspect > contentAspect ? height : width / GUIAspect) / 2f;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            ClampZoomAndPan();
        }

        private void RefreshAllTiles()
        {
            if (tilemap != null)
                tilemap.RefreshAllTiles();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
<<<<<<< HEAD
            EditorApplication.editorApplicationQuit += EditorApplicationQuit;
            Undo.undoRedoPerformed += UndoRedoPerformed;
            m_KeyboardPanningID = GUIUtility.GetPermanentControlID();
            m_MousePanningID = GUIUtility.GetPermanentControlID();
=======
<<<<<<< HEAD
            EditorApplication.editorApplicationQuit += EditorApplicationQuit;
            Undo.undoRedoPerformed += UndoRedoPerformed;
            m_KeyboardPanningID = GUIUtility.GetPermanentControlID();
            m_MousePanningID = GUIUtility.GetPermanentControlID();
=======

            instances.Add(this);

            EditorApplication.editorApplicationQuit += EditorApplicationQuit;
            PrefabUtility.prefabInstanceUpdated += PrefabInstanceUpdated;
            Undo.undoRedoPerformed += UndoRedoPerformed;

            m_KeyboardPanningID = GUIUtility.GetPermanentControlID();
            m_MousePanningID = GUIUtility.GetPermanentControlID();

            InitPreviewUtility();
            ResetPreviewInstance();
            SetupPreviewCameraOnInit();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void OnDisable()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (m_Owner && previewUtility != null && previewUtility.camera != null)
            {
                // Save Preview camera coordinates
                m_CameraPosition = previewUtility.camera.transform.position;
                m_CameraOrthographicSize = previewUtility.camera.orthographicSize;
                m_CameraPositionSaved = true;
            }

            SavePaletteIfNecessary();
            DestroyPreviewInstance();
            Undo.undoRedoPerformed -= UndoRedoPerformed;
            EditorApplication.editorApplicationQuit -= EditorApplicationQuit;
            base.OnDisable();
        }

<<<<<<< HEAD
=======
=======
            SavePaletteIfNecessary();
            unlocked = false;
            DestroyPreviewInstance();

            if (m_PreviewUtility != null && m_PreviewUtility.camera != null)
            {
                // Save Preview camera coordinates
                m_CameraPosition = m_PreviewUtility.camera.transform.position;
                m_CameraOrthographicSize = m_PreviewUtility.camera.orthographicSize;
                m_CameraPositionSaved = true;
            }
            if (m_PreviewUtility != null)
                m_PreviewUtility.Cleanup();
            m_PreviewUtility = null;

            Undo.undoRedoPerformed -= UndoRedoPerformed;
            PrefabUtility.prefabInstanceUpdated -= PrefabInstanceUpdated;
            EditorApplication.editorApplicationQuit -= EditorApplicationQuit;

            instances.Remove(this);

            base.OnDisable();
        }

        private void DisplayClipboardText(GUIContent clipboardText, Rect textPosition)
        {
            Color old = GUI.color;
            GUI.color = Color.gray;
            var infoSize = GUI.skin.label.CalcSize(clipboardText);
            Rect rect = new Rect(textPosition.center.x - infoSize.x * .5f, textPosition.center.y - infoSize.y * .5f, infoSize.x, infoSize.y);
            GUI.Label(rect, clipboardText);
            GUI.color = old;
        }

        public void OnClipboardGUI(Rect clipboardPosition)
        {
            if (Event.current.type != EventType.Layout && clipboardPosition.Contains(Event.current.mousePosition) && GridPaintingState.activeGrid != this && unlocked)
            {
                GridPaintingState.activeGrid = this;
                SceneView.RepaintAll();
            }

            // Validate palette (case 1017965)
            GUIContent paletteError = null;
            if (palette == null)
            {
                if (GridPaintingState.palettes.Count == 0)
                    paletteError = Styles.emptyProjectInfo;
                else
                    paletteError = Styles.invalidPaletteInfo;
            }
            else if (palette.GetComponent<Grid>() == null)
            {
                paletteError = Styles.invalidGridInfo;
            }

            if (paletteError != null)
            {
                DisplayClipboardText(paletteError, clipboardPosition);
                return;
            }

            bool oldEnabled = GUI.enabled;
            GUI.enabled = !showNewEmptyClipboardInfo || DragAndDrop.objectReferences.Length > 0;
            if (Event.current.type == EventType.Repaint)
                guiRect = clipboardPosition;

            EditorGUI.BeginChangeCheck();
            OnGUI();
            if (EditorGUI.EndChangeCheck())
                Repaint();

            GUI.enabled = oldEnabled;

            if (showNewEmptyClipboardInfo)
            {
                DisplayClipboardText(Styles.emptyPaletteInfo, clipboardPosition);
            }
        }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public override void OnGUI()
        {
            if (Mathf.Approximately(guiRect.width, 0f) || Mathf.Approximately(guiRect.height, 0f))
                return;

            UpdateMouseGridPosition();

            HandleDragAndDrop();

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
            if (m_DelayedResetPaletteInstance)
            {
                var originalSwizzleView = m_CameraSwizzleView;
                ResetPreviewInstance();
                if (palette != null && originalSwizzleView != m_CameraSwizzleView)
                    ResetPreviewCamera();
            }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (palette == null)
                return;

            HandlePanAndZoom();
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
            HandleKeyboardMousePick();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            if (showNewEmptyClipboardInfo)
                return;

            if (Event.current.type == EventType.Repaint && !m_CameraInitializedToBounds)
            {
                Frame(bounds);
                m_CameraInitializedToBounds = true;
            }

            HandleMouseEnterLeave();

            if (guiRect.Contains(Event.current.mousePosition) || Event.current.type != EventType.MouseDown)
                base.OnGUI();

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (Event.current.type == EventType.Repaint)
                Render();
            else
                DoBrush();

            m_PreviousMousePosition = Event.current.mousePosition;
        }

        public void OnViewSizeChanged(Rect oldSize, Rect newSize)
<<<<<<< HEAD
=======
=======
            if (Event.current.type == EventType.Repaint
                || (unlocked && (inEditMode || GridSelectionTool.IsActive())))
            {
                Render();
            }
            else
            {
                RenderSelectedBrushMarquee();
                CallOnPaintSceneGUI(mouseGridPosition);
            }
            m_PreviousMousePosition = Event.current.mousePosition;
        }

        private void OnViewSizeChanged(Rect oldSize, Rect newSize)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (Mathf.Approximately(oldSize.height * oldSize.width * newSize.height * newSize.width, 0f))
                return;

<<<<<<< HEAD
            Camera cam = previewUtility.camera;
=======
<<<<<<< HEAD
            Camera cam = previewUtility.camera;
=======
            Camera cam = m_PreviewUtility.camera;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            Vector2 sizeDelta = new Vector2(
                newSize.width / LocalToScreenRatio(newSize.height) - oldSize.width / LocalToScreenRatio(oldSize.height),
                newSize.height / LocalToScreenRatio(newSize.height) - oldSize.height / LocalToScreenRatio(oldSize.height));

            cam.transform.Translate(sizeDelta / 2f);

            ClampZoomAndPan();
        }

        private void EditorApplicationQuit()
        {
            SavePaletteIfNecessary();
        }

        private void UndoRedoPerformed()
        {
<<<<<<< HEAD
            if (unlocked)
=======
            if (!unlocked)
                return;

            m_PaletteNeedsSave = true;
            RefreshAllTiles();
            Repaint();
        }

<<<<<<< HEAD
=======
        private void PrefabInstanceUpdated(GameObject updatedPrefab)
        {
            // case 947462: Reset the palette instance after its prefab has been updated as it could have been changed
            if (m_PaletteInstance != null && PrefabUtility.GetCorrespondingObjectFromSource(updatedPrefab) == palette && !GridPaintingState.savingPalette)
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            {
                m_PaletteNeedsSave = true;
                RefreshAllTiles();
                Repaint();
            }
        }

<<<<<<< HEAD
=======
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private void HandlePanAndZoom()
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    if (MousePanningEvent() && guiRect.Contains(Event.current.mousePosition) && GUIUtility.hotControl == 0)
                    {
                        GUIUtility.hotControl = m_MousePanningID;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
                        m_MouseZoomInitialPosition = Event.current.mousePosition;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        Event.current.Use();
                    }
                    break;
                case EventType.ValidateCommand:
                    if (Event.current.commandName == EventCommandNames.FrameSelected)
                    {
                        Event.current.Use();
                    }
                    break;
                case EventType.ExecuteCommand:
                    if (Event.current.commandName == EventCommandNames.FrameSelected)
                    {
                        if (m_ActivePick.HasValue)
<<<<<<< HEAD
                            Frame(m_ActivePick.Value);
=======
<<<<<<< HEAD
                            Frame(m_ActivePick.Value);
=======
                        {
                            var rect = new RectInt(m_ActivePick.Value.x, m_ActivePick.Value.y,
                                m_ActivePick.Value.size.x, m_ActivePick.Value.size.y);
                            Frame(rect);
                        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        else
                            FrameEntirePalette();
                        Event.current.Use();
                    }
                    break;
                case EventType.ScrollWheel:
                    if (guiRect.Contains(Event.current.mousePosition))
                    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        float zoomDelta = HandleUtility.niceMouseDeltaZoom * (Event.current.shift ? -9 : -3) * k_ZoomSpeed;
                        Camera camera = previewUtility.camera;
                        Vector3 oldLocalPos = ScreenToLocal(Event.current.mousePosition);
                        camera.orthographicSize = Mathf.Max(.0001f, camera.orthographicSize * (1 + zoomDelta * .001f));
                        ClampZoomAndPan();
                        Vector3 newLocalPos = ScreenToLocal(Event.current.mousePosition);
                        Vector3 localDelta = newLocalPos - oldLocalPos;
<<<<<<< HEAD
                        camera.transform.position = camera.transform.position - localDelta;
                        ClampZoomAndPan();
=======
                        camera.transform.position -= localDelta;
                        ClampZoomAndPan();
=======
                        HandleMouseZoom(Event.current.mousePosition);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        Event.current.Use();
                    }
                    break;
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == m_MousePanningID)
                    {
<<<<<<< HEAD
                        Vector3 delta = new Vector3(-Event.current.delta.x, Event.current.delta.y, 0f) / LocalToScreenRatio();
                        previewUtility.camera.transform.Translate(delta);
                        ClampZoomAndPan();
                        Event.current.Use();
                    }
                    break;
                case EventType.MouseMove: // Fix mousecursor being stuck when panning ended outside our window
=======
<<<<<<< HEAD
                        Vector3 delta = new Vector3(-Event.current.delta.x, Event.current.delta.y, 0f) / LocalToScreenRatio();
                        previewUtility.camera.transform.Translate(delta);
                        ClampZoomAndPan();
=======
                        if (Event.current.alt && Event.current.button == 1)
                        {
                            HandleMouseZoom(m_MouseZoomInitialPosition);
                        }
                        else
                        {
                            Vector3 delta = new Vector3(-Event.current.delta.x, Event.current.delta.y, 0f) / LocalToScreenRatio();
                            m_PreviewUtility.camera.transform.Translate(delta);
                            ClampZoomAndPan();
                        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                        Event.current.Use();
                    }
                    break;
                case EventType.MouseMove: // Fix mouse cursor being stuck when panning ended outside our window
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    if (GUIUtility.hotControl == m_MousePanningID && !MousePanningEvent())
                        GUIUtility.hotControl = 0;
                    break;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == m_MousePanningID)
                    {
                        ClampZoomAndPan();
                        GUIUtility.hotControl = 0;
                        Event.current.Use();
                    }
                    break;
                case EventType.KeyDown:
<<<<<<< HEAD
                    if (GUIUtility.hotControl == 0)
=======
<<<<<<< HEAD
                    if (GUIUtility.hotControl == 0)
=======
                    if ((GUIUtility.hotControl == 0 || GUIUtility.hotControl == m_KeyboardPanningID) && !Event.current.shift)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    {
                        switch (Event.current.keyCode)
                        {
                            case KeyCode.LeftArrow:
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(-k_KeyboardPanningSpeed, 0f) / LocalToScreenRatio();
=======
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(-k_KeyboardPanningSpeed, 0f) / LocalToScreenRatio();
=======
                                m_KeyboardPanning.x = -k_KeyboardPanningSpeed / LocalToScreenRatio();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                                GUIUtility.hotControl = m_KeyboardPanningID;
                                Event.current.Use();
                                break;
                            case KeyCode.RightArrow:
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(k_KeyboardPanningSpeed, 0f) / LocalToScreenRatio();
=======
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(k_KeyboardPanningSpeed, 0f) / LocalToScreenRatio();
=======
                                m_KeyboardPanning.x = k_KeyboardPanningSpeed / LocalToScreenRatio();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                                GUIUtility.hotControl = m_KeyboardPanningID;
                                Event.current.Use();
                                break;
                            case KeyCode.UpArrow:
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(0f, k_KeyboardPanningSpeed) / LocalToScreenRatio();
=======
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(0f, k_KeyboardPanningSpeed) / LocalToScreenRatio();
=======
                                m_KeyboardPanning.y = k_KeyboardPanningSpeed / LocalToScreenRatio();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                                GUIUtility.hotControl = m_KeyboardPanningID;
                                Event.current.Use();
                                break;
                            case KeyCode.DownArrow:
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(0f, -k_KeyboardPanningSpeed) / LocalToScreenRatio();
=======
<<<<<<< HEAD
                                m_KeyboardPanning = new Vector3(0f, -k_KeyboardPanningSpeed) / LocalToScreenRatio();
=======
                                m_KeyboardPanning.y = -k_KeyboardPanningSpeed / LocalToScreenRatio();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                                GUIUtility.hotControl = m_KeyboardPanningID;
                                Event.current.Use();
                                break;
                        }
                    }
                    break;
                case EventType.KeyUp:
                    if (GUIUtility.hotControl == m_KeyboardPanningID)
                    {
                        m_KeyboardPanning = Vector3.zero;
                        GUIUtility.hotControl = 0;
                        Event.current.Use();
                    }
                    break;
                case EventType.Repaint:
                    if (GUIUtility.hotControl == m_KeyboardPanningID)
                    {
<<<<<<< HEAD
                        previewUtility.camera.transform.Translate(m_KeyboardPanning);
=======
<<<<<<< HEAD
                        previewUtility.camera.transform.Translate(m_KeyboardPanning);
=======
                        m_PreviewUtility.camera.transform.Translate(m_KeyboardPanning);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        ClampZoomAndPan();
                        Repaint();
                    }

                    if (GUIUtility.hotControl == m_MousePanningID)
                        EditorGUIUtility.AddCursorRect(guiRect, MouseCursor.Pan);

                    break;
            }
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        private void HandleMouseZoom(Vector2 currentMousePosition)
        {
            float zoomDelta = HandleUtility.niceMouseDeltaZoom * (Event.current.shift ? -9 : -3) * k_ZoomSpeed;
            Camera camera = m_PreviewUtility.camera;
            Vector3 oldLocalPos = ScreenToLocal(currentMousePosition);
            camera.orthographicSize = Mathf.Max(.0001f, camera.orthographicSize * (1 + zoomDelta * .001f));
            ClampZoomAndPan();
            Vector3 newLocalPos = ScreenToLocal(currentMousePosition);
            Vector3 localDelta = newLocalPos - oldLocalPos;
            camera.transform.position -= localDelta;
            ClampZoomAndPan();
        }

        private void HandleKeyboardMousePick()
        {
            if (GUIUtility.hotControl == 0 || GUIUtility.hotControl == m_KeyboardPanningID)
            {
                if (Event.current.type == EventType.KeyDown && Event.current.shift && m_ActivePick.HasValue)
                {
                    var delta = Vector3Int.zero;
                    switch (Event.current.keyCode)
                    {
                        case KeyCode.LeftArrow:
                            delta = Vector3Int.left;
                            break;
                        case KeyCode.RightArrow:
                            delta = Vector3Int.right;
                            Event.current.Use();
                            break;
                        case KeyCode.UpArrow:
                            delta = Vector3Int.up;
                            Event.current.Use();
                            break;
                        case KeyCode.DownArrow:
                            delta = Vector3Int.down;
                            break;
                    }

                    if (delta != Vector3Int.zero)
                    {
                        disableOnBrushPicked = true;
                        PickBrush(new BoundsInt(m_ActivePick.Value.position + delta, m_ActivePick.Value.size),
                            m_ActivePivot);
                        disableOnBrushPicked = false;
                        Event.current.Use();
                    }
                }
            }
        }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private static bool MousePanningEvent()
        {
            return (Event.current.button == 0 && Event.current.alt || Event.current.button > 0);
        }

<<<<<<< HEAD
        public void ClampZoomAndPan()
        {
            float pixelsPerCell = grid.cellSize.y * LocalToScreenRatio();

=======
<<<<<<< HEAD
        public void ClampZoomAndPan()
        {
            float pixelsPerCell = grid.cellSize.y * LocalToScreenRatio();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (pixelsPerCell < k_MinZoom)
                previewUtility.camera.orthographicSize = (grid.cellSize.y * guiRect.height) / (k_MinZoom * 2f);
            else if (pixelsPerCell > k_MaxZoom)
                previewUtility.camera.orthographicSize = (grid.cellSize.y * guiRect.height) / (k_MaxZoom * 2f);

            Camera cam = previewUtility.camera;
<<<<<<< HEAD
            Rect r = paddedBounds;

            Vector3 camPos = cam.transform.position;
            Vector2 camMin = camPos - new Vector3(cam.orthographicSize * (guiRect.width / guiRect.height), cam.orthographicSize);
            Vector2 camMax = camPos + new Vector3(cam.orthographicSize * (guiRect.width / guiRect.height), cam.orthographicSize);
=======
            float cameraOrthographicSize = cam.orthographicSize;
            Rect r = paddedBounds;

            Vector3 camPos = cam.transform.position;
            Vector2 camMin = camPos - new Vector3(cameraOrthographicSize * (guiRect.width / guiRect.height), cameraOrthographicSize);
            Vector2 camMax = camPos + new Vector3(cameraOrthographicSize * (guiRect.width / guiRect.height), cameraOrthographicSize);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            if (camMin.x < r.min.x)
            {
                camPos += new Vector3(r.min.x - camMin.x, 0f, 0f);
            }
            if (camMin.y < r.min.y)
            {
                camPos += new Vector3(0f, r.min.y - camMin.y, 0f);
            }
            if (camMax.x > r.max.x)
            {
                camPos += new Vector3(r.max.x - camMax.x, 0f, 0f);
            }
            if (camMax.y > r.max.y)
            {
                camPos += new Vector3(0f, r.max.y - camMax.y, 0f);
            }

            camPos.Set(camPos.x, camPos.y, -10f);

<<<<<<< HEAD
=======
=======
        private void ClampZoomAndPan()
        {
            float pixelsPerCell = grid.cellSize.y * LocalToScreenRatio();
            if (pixelsPerCell < k_MinZoom)
                m_PreviewUtility.camera.orthographicSize = (grid.cellSize.y * guiRect.height) / (k_MinZoom * 2f);
            else if (pixelsPerCell > k_MaxZoom)
                m_PreviewUtility.camera.orthographicSize = (grid.cellSize.y * guiRect.height) / (k_MaxZoom * 2f);

            Camera cam = m_PreviewUtility.camera;
            float cameraOrthographicSize = cam.orthographicSize;
            Rect r = paddedBounds;

            var camPos = cam.transform.position;
            var camLimit = Grid.Swizzle(m_CameraSwizzleView, new Vector3(cameraOrthographicSize * (guiRect.width / guiRect.height), cameraOrthographicSize));
            var camMin = camPos - camLimit;
            var camMax = camPos + camLimit;
            var rMin = Grid.Swizzle(m_CameraSwizzleView, r.min);
            var rMax = Grid.Swizzle(m_CameraSwizzleView, r.max);

            if (m_CameraSwizzleView != GridLayout.CellSwizzle.ZXY && m_CameraSwizzleView != GridLayout.CellSwizzle.ZYX)
            {
                if (camMin.x < rMin.x)
                {
                    camPos += new Vector3(rMin.x - camMin.x, 0f, 0f);
                }
                if (camMax.x > rMax.x)
                {
                    camPos += new Vector3(rMax.x - camMax.x, 0f, 0f);
                }
            }

            if (m_CameraSwizzleView != GridLayout.CellSwizzle.XZY && m_CameraSwizzleView != GridLayout.CellSwizzle.YZX)
            {
                if (camMin.y < rMin.y)
                {
                    camPos += new Vector3(0f, rMin.y - camMin.y, 0f);
                }

                if (camMax.y > rMax.y)
                {
                    camPos += new Vector3(0f, rMax.y - camMax.y, 0f);
                }
            }

            if (m_CameraSwizzleView != GridLayout.CellSwizzle.XYZ && m_CameraSwizzleView != GridLayout.CellSwizzle.YXZ)
            {
                if (camMin.z < rMin.z)
                {
                    camPos += new Vector3(0f, 0f, rMin.z - camMin.z);
                }
                if (camMax.z > rMax.z)
                {
                    camPos += new Vector3(0f, 0f, rMax.z - camMax.z);
                }
            }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            cam.transform.position = camPos;

            DestroyImmediate(m_GridMesh);
            m_GridMesh = null;
        }

        private void Render()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (m_GridMesh != null && GetGridHash() != m_LastGridHash)
            {
                ResetPreviewInstance();
                ResetPreviewMesh();
            }

<<<<<<< HEAD
            previewUtility.BeginPreview(guiRect, Styles.background);

            BeginPreviewInstance();
            RenderGrid();
            EndPreviewInstance();

            RenderDragAndDropPreview();
            RenderSelectedBrushMarquee();
            DoBrush();

            previewUtility.EndAndDrawPreview(guiRect);
=======
            using (new PreviewInstanceScope(guiRect, previewUtility, paletteInstance, m_Owner.drawGizmos))
            {
                previewUtility.Render(true);
                if (m_Owner.drawGridGizmo)
                    RenderGrid();
                if (m_Owner.drawGizmos)
                {
                    // Set CameraType to SceneView to force Gizmos to be drawn
                    var storedType = previewUtility.camera.cameraType;
                    previewUtility.camera.cameraType = CameraType.SceneView;
                    Handles.Internal_DoDrawGizmos(previewUtility.camera);
                    previewUtility.camera.cameraType = storedType;
=======
            if (guiRect.width <= 0f || guiRect.height <= 0f)
                return;

            if (m_GridMesh != null && GetGridHash() != m_LastGridHash)
            {
                ResetPreviewInstance();
                ResetPreviewGridMesh();
            }

            using (new PreviewInstanceScope(guiRect, m_PreviewUtility, paletteInstance, GridPaintingState.drawGizmos))
            {
                m_PreviewUtility.Render(true);
                if (GridPaintingState.drawGridGizmo)
                    RenderGrid();
                CallOnPaintSceneGUI(mouseGridPosition);
                if (GridPaintingState.drawGizmos)
                {
                    // Set CameraType to SceneView to force Gizmos to be drawn
                    var storedType = m_PreviewUtility.camera.cameraType;
                    m_PreviewUtility.camera.cameraType = CameraType.SceneView;
                    Handles.Internal_DoDrawGizmos(m_PreviewUtility.camera);
                    m_PreviewUtility.camera.cameraType = storedType;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                }
            }

            RenderDragAndDropPreview();
<<<<<<< HEAD
            CallOnSceneGUI();
            DoBrush();

            previewUtility.EndAndDrawPreview(guiRect);
=======
            RenderSelectedBrushMarquee();
            CallOnSceneGUI();

            m_PreviewUtility.EndAndDrawPreview(guiRect);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            m_LastGridHash = GetGridHash();
        }

        private int GetGridHash()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (prefabGrid == null)
                return 0;

            int hash = prefabGrid.GetHashCode();
            unchecked
            {
                hash = hash * 33 + prefabGrid.cellGap.GetHashCode();
                hash = hash * 33 + prefabGrid.cellLayout.GetHashCode();
                hash = hash * 33 + prefabGrid.cellSize.GetHashCode();
                hash = hash * 33 + prefabGrid.cellSwizzle.GetHashCode();
<<<<<<< HEAD
=======
=======
            var gridToHash = prefabGrid;
            if (gridToHash == null)
                return 0;

            int hash = gridToHash.GetHashCode();
            unchecked
            {
                hash = hash * 33 + gridToHash.cellGap.GetHashCode();
                hash = hash * 33 + gridToHash.cellLayout.GetHashCode();
                hash = hash * 33 + gridToHash.cellSize.GetHashCode();
                hash = hash * 33 + gridToHash.cellSwizzle.GetHashCode();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                hash = hash * 33 + SceneViewGridManager.sceneViewGridComponentGizmo.Color.GetHashCode();
            }
            return hash;
        }

        private void RenderDragAndDropPreview()
        {
            if (!activeDragAndDrop || m_HoverData == null || m_HoverData.Count == 0)
                return;

<<<<<<< HEAD
            RectInt rect = TileDragAndDrop.GetMinMaxRect(m_HoverData.Keys.ToList());
=======
<<<<<<< HEAD
            RectInt rect = TileDragAndDrop.GetMinMaxRect(m_HoverData.Keys.ToList());
=======
            var rect = TileDragAndDrop.GetMinMaxRect(m_HoverData.Keys);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            rect.position += mouseGridPosition;
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            GridEditorUtility.DrawGridMarquee(grid, new BoundsInt(new Vector3Int(rect.xMin, rect.yMin, zPosition), new Vector3Int(rect.width, rect.height, 1)), Color.white);
        }

        private void RenderGrid()
        {
            // MeshTopology.Lines doesn't give nice pixel perfect grid so we have to have separate codepath with MeshTopology.Quads specially for palette window here
            if (m_GridMesh == null && grid.cellLayout == GridLayout.CellLayout.Rectangle)
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                m_GridMesh = GridEditorUtility.GenerateCachedGridMesh(grid, k_GridColor, 1f / LocalToScreenRatio(), paddedBoundsInt, MeshTopology.Quads);

            GridEditorUtility.DrawGridGizmo(grid, grid.transform, k_GridColor, ref m_GridMesh, ref m_GridMaterial);
        }

        private void DoBrush()
        {
            if (activeDragAndDrop)
                return;

            RenderSelectedBrushMarquee();
            CallOnPaintSceneGUI(mouseGridPosition);
        }

<<<<<<< HEAD
        private void BeginPreviewInstance()
        {
            m_OldFog = RenderSettings.fog;
            Unsupported.SetRenderSettingsUseFogNoDirty(false);
            Handles.DrawCameraImpl(m_GUIRect, previewUtility.camera, DrawCameraMode.Textured, false, new DrawGridParameters(), true, false);
            PreviewRenderUtility.SetEnabledRecursive(paletteInstance, true);
            previewUtility.AddManagedGO(paletteInstance);
        }

        private void EndPreviewInstance()
        {
            previewUtility.Render();
            PreviewRenderUtility.SetEnabledRecursive(paletteInstance, false);
            Unsupported.SetRenderSettingsUseFogNoDirty(m_OldFog);
        }

        public void HandleDragAndDrop()
=======
        private class PreviewInstanceScope : IDisposable
        {
            private readonly PreviewRenderUtility m_PreviewRenderUtility;
            private readonly bool m_OldFog;
            private readonly bool m_DrawGizmos;
            private readonly GameObject m_PaletteInstance;
            private readonly Transform[] m_PaletteTransforms;
            private readonly Renderer[] m_Renderers;

            public PreviewInstanceScope(Rect guiRect, PreviewRenderUtility previewRenderUtility, GameObject paletteInstance, bool drawGizmos)
            {
                m_PreviewRenderUtility = previewRenderUtility;
                m_PaletteInstance = paletteInstance;
                m_DrawGizmos = drawGizmos;
                m_OldFog = RenderSettings.fog;

                m_PreviewRenderUtility.BeginPreview(guiRect, Styles.background);
                Unsupported.SetRenderSettingsUseFogNoDirty(false);
                if (m_DrawGizmos)
                {
                    m_PaletteTransforms = m_PaletteInstance.GetComponentsInChildren<Transform>();
                    foreach (var transform in m_PaletteTransforms)
                        transform.gameObject.hideFlags = HideFlags.None;
                    // Case 1199516: Set Dirty on palette instance to force a refresh on gizmo drawing
                    EditorUtility.SetDirty(m_PaletteInstance);
                    Unsupported.SceneTrackerFlushDirty();
                }
                m_Renderers = m_PaletteInstance.GetComponentsInChildren<Renderer>();
                foreach (var renderer in m_Renderers)
                {
                    renderer.allowOcclusionWhenDynamic = false;
                }
                m_PreviewRenderUtility.AddManagedGO(m_PaletteInstance);
                Handles.DrawCameraImpl(guiRect, m_PreviewRenderUtility.camera, DrawCameraMode.Textured, false, new DrawGridParameters(), true, false);
=======
            {
                m_GridMesh = GridEditorUtility.GenerateCachedGridMesh(grid, k_GridColor, 1f / LocalToScreenRatio(), paddedBoundsInt, grid.cellSwizzle == GridLayout.CellSwizzle.XYZ ? MeshTopology.Quads : MeshTopology.Lines);
            }
            GridEditorUtility.DrawGridGizmo(grid, grid.transform, k_GridColor, ref m_GridMesh, ref m_GridMaterial);
        }

        private class PreviewInstanceScope : IDisposable
        {
            private readonly bool m_OldFog;
            private readonly bool m_DrawGizmos;
            private readonly Transform[] m_PaletteTransforms;

            public PreviewInstanceScope(Rect guiRect, PreviewRenderUtility previewRenderUtility, GameObject paletteInstance, bool drawGizmos)
            {
                m_DrawGizmos = drawGizmos;
                m_OldFog = RenderSettings.fog;

                previewRenderUtility.BeginPreview(guiRect, null);

                // Draw Background here with user preference color
                Graphics.DrawTexture(new Rect(0.0f, 0.0f
                        , (float) 2 * EditorGUIUtility.pixelsPerPoint * guiRect.width
                        , (float) 2 * EditorGUIUtility.pixelsPerPoint * guiRect.height)
                    , (Texture) Texture2D.grayTexture, new Rect(0.0f, 0.0f, 1f, 1f)
                    , 0, 0, 0, 0
                    , tilePaletteBackgroundColor.Color, (Material) null);

                Unsupported.SetRenderSettingsUseFogNoDirty(false);
                if (m_DrawGizmos)
                {
                    m_PaletteTransforms = paletteInstance.GetComponentsInChildren<Transform>();
                    foreach (var transform in m_PaletteTransforms)
                        transform.gameObject.hideFlags = HideFlags.None;
                    // Case 1199516: Set Dirty on palette instance to force a refresh on gizmo drawing
                    EditorUtility.SetDirty(paletteInstance);
                    Unsupported.SceneTrackerFlushDirty();
                }
                var renderers = paletteInstance.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    renderer.allowOcclusionWhenDynamic = false;
                }
                previewRenderUtility.AddManagedGO(paletteInstance);
                Handles.DrawCameraImpl(guiRect, previewRenderUtility.camera, DrawCameraMode.Textured, false, new DrawGridParameters(), true, false);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }

            public void Dispose()
            {
                if (m_DrawGizmos && m_PaletteTransforms != null)
                {
                    foreach (var transform in m_PaletteTransforms)
                        transform.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
                Unsupported.SetRenderSettingsUseFogNoDirty(m_OldFog);
            }
        }

<<<<<<< HEAD
        public void HandleDragAndDrop()
=======
        private void HandleDragAndDrop()
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (DragAndDrop.objectReferences.Length == 0 || !guiRect.Contains(Event.current.mousePosition))
                return;

            switch (Event.current.type)
            {
                //TODO: Cache this
                case EventType.DragUpdated:
                {
                    List<Texture2D> sheets = TileDragAndDrop.GetValidSpritesheets(DragAndDrop.objectReferences);
                    List<Sprite> sprites = TileDragAndDrop.GetValidSingleSprites(DragAndDrop.objectReferences);
                    List<TileBase> tiles = TileDragAndDrop.GetValidTiles(DragAndDrop.objectReferences);
<<<<<<< HEAD
                    m_HoverData = TileDragAndDrop.CreateHoverData(sheets, sprites, tiles);
=======
                    m_HoverData = TileDragAndDrop.CreateHoverData(sheets, sprites, tiles, tilemap.cellLayout);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

                    if (m_HoverData != null && m_HoverData.Count > 0)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        Event.current.Use();
                        GUI.changed = true;
                    }
                }
<<<<<<< HEAD
                break;
=======
<<<<<<< HEAD
                break;
=======
                    break;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                case EventType.DragPerform:
                {
                    if (m_HoverData == null || m_HoverData.Count == 0)
                        return;

                    RegisterUndo();

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    bool wasEmpty = TilemapIsEmpty(tilemap);

                    Vector2Int targetPosition = mouseGridPosition;
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
<<<<<<< HEAD
                    Dictionary<Vector2Int, TileBase> tileSheet = TileDragAndDrop.ConvertToTileSheet(m_HoverData);
                    foreach (KeyValuePair<Vector2Int, TileBase> item in tileSheet)
                        SetTile(tilemap, targetPosition + item.Key, item.Value, Color.white, Matrix4x4.identity);

=======
                    var tileSheet = TileDragAndDrop.ConvertToTileSheet(m_HoverData);
                    int i = 0;
                    foreach (KeyValuePair<Vector2Int, TileDragAndDropHoverData> item in m_HoverData)
=======
                    var wasEmpty = TilemapIsEmpty(tilemap);

                    var targetPosition = mouseGridPosition;
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    var tileSheet = TileDragAndDrop.ConvertToTileSheet(m_HoverData);
                    var i = 0;
                    foreach (var item in m_HoverData)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                    {
                        if (i >= tileSheet.Count)
                            break;

                        var offset = Vector3.zero;
                        if (item.Value.hasOffset)
                        {
                            offset = item.Value.positionOffset - tilemap.tileAnchor;

                            var cellSize = tilemap.cellSize;
                            if (wasEmpty)
                            {
                                cellSize = item.Value.scaleFactor;
                            }
                            offset.x *= cellSize.x;
                            offset.y *= cellSize.y;
                            offset.z *= cellSize.z;
                        }

                        SetTile(tilemap
                            , targetPosition + item.Key
                            , tileSheet[i++]
                            , Color.white
                            , Matrix4x4.TRS(offset, Quaternion.identity, Vector3.one));
                    }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    OnPaletteChanged();

                    m_PaletteNeedsSave = true;
                    FlushHoverData();
                    GUI.changed = true;
                    SavePaletteIfNecessary();

                    if (wasEmpty)
                    {
                        ResetPreviewInstance();
                        FrameEntirePalette();
                    }

                    Event.current.Use();
                    GUIUtility.ExitGUI();
                }
<<<<<<< HEAD
                break;
=======
<<<<<<< HEAD
                break;
=======
                    break;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                case EventType.Repaint:
                    // Handled in Render()
                    break;
            }

            if (m_HoverData != null && (
<<<<<<< HEAD
                Event.current.type == EventType.DragExited ||
                Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape))
=======
<<<<<<< HEAD
                Event.current.type == EventType.DragExited ||
                Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape))
=======
                    Event.current.type == EventType.DragExited ||
                    Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape))
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.None;
                FlushHoverData();
                Event.current.Use();
            }
        }

<<<<<<< HEAD
        public void SetTile(Tilemap tilemap, Vector2Int position, TileBase tile, Color color, Matrix4x4 matrix)
        {
            Vector3Int pos3 = new Vector3Int(position.x, position.y, zPosition);
            tilemap.SetTile(pos3, tile);
            tilemap.SetColor(pos3, color);
            tilemap.SetTransformMatrix(pos3, matrix);
=======
<<<<<<< HEAD
        public void SetTile(Tilemap tilemapTarget, Vector2Int position, TileBase tile, Color color, Matrix4x4 matrix)
=======
        internal void SetTile(Tilemap tilemapTarget, Vector2Int position, TileBase tile, Color color, Matrix4x4 matrix)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        {
            Vector3Int pos3 = new Vector3Int(position.x, position.y, zPosition);
            tilemapTarget.SetTile(pos3, tile);
            tilemapTarget.SetColor(pos3, color);
<<<<<<< HEAD
            tilemapTarget.SetTransformMatrix(pos3, matrix);
=======
            tilemapTarget.SetTransformMatrix(pos3, tilemapTarget.GetTransformMatrix(pos3) * matrix);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void Paint(Vector3Int position)
        {
            if (gridBrush == null)
                return;

            gridBrush.Paint(grid, brushTarget, position);
            OnPaletteChanged();
        }

        protected override void Erase(Vector3Int position)
        {
            if (gridBrush == null)
                return;

            gridBrush.Erase(grid, brushTarget, position);
            OnPaletteChanged();
        }

        protected override void BoxFill(BoundsInt position)
        {
            if (gridBrush == null)
                return;

            gridBrush.BoxFill(grid, brushTarget, position);
            OnPaletteChanged();
        }

        protected override void BoxErase(BoundsInt position)
        {
            if (gridBrush == null)
                return;

            gridBrush.BoxErase(grid, brushTarget, position);
            OnPaletteChanged();
        }

        protected override void FloodFill(Vector3Int position)
        {
            if (gridBrush == null)
                return;

            gridBrush.FloodFill(grid, brushTarget, position);
            OnPaletteChanged();
        }

        protected override void PickBrush(BoundsInt position, Vector3Int pickingStart)
        {
            if (grid == null || gridBrush == null)
                return;

            gridBrush.Pick(grid, brushTarget, position, pickingStart);

            if (!InGridEditMode())
                TilemapEditorTool.SetActiveEditorTool(typeof(PaintTool));

<<<<<<< HEAD
            m_ActivePick = new RectInt(position.min.x, position.min.y, position.size.x, position.size.y);
=======
<<<<<<< HEAD
            m_ActivePick = new RectInt(position.min.x, position.min.y, position.size.x, position.size.y);
=======
            m_ActivePick = position;
            m_ActivePivot = pickingStart;

            if (!disableOnBrushPicked)
                onBrushPicked?.Invoke();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void Select(BoundsInt position)
        {
            if (grid)
            {
                GridSelection.Select(brushTarget, position);
                gridBrush.Select(grid, brushTarget, position);
            }
        }

        protected override void Move(BoundsInt from, BoundsInt to)
        {
            if (grid)
                gridBrush.Move(grid, brushTarget, from, to);
        }

        protected override void MoveStart(BoundsInt position)
        {
            if (grid)
                gridBrush.MoveStart(grid, brushTarget, position);
        }

        protected override void MoveEnd(BoundsInt position)
        {
            if (grid)
            {
                gridBrush.MoveEnd(grid, brushTarget, position);
                OnPaletteChanged();
            }
        }

<<<<<<< HEAD
        public override void Repaint()
        {
            m_Owner.Repaint();
=======
<<<<<<< HEAD
        protected override bool CustomTool(bool isHotControl, TilemapEditorTool tool, Vector3Int position)
=======
        protected override bool CustomTool(bool isToolHotControl, TilemapEditorTool tool, Vector3Int position)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        {
            var executed = false;
            if (grid)
            {
<<<<<<< HEAD
                executed = tool.HandleTool(isHotControl, grid, brushTarget, position);
=======
                executed = tool.HandleTool(isToolHotControl, grid, brushTarget, position);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                if (executed)
                    OnPaletteChanged();
            }
            return executed;
        }

<<<<<<< HEAD
        public override void Repaint()
        {
            m_Owner.Repaint();
=======
        protected override bool IsMouseUpInWindow()
        {
            return Event.current.type == EventType.MouseUp && guiRect.Contains(Event.current.mousePosition);
        }

        public override void Repaint()
        {
            if (m_VisualElement != null)
                m_VisualElement.MarkDirtyRepaint();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void ClearGridSelection()
        {
            GridSelection.Clear();
        }

<<<<<<< HEAD
=======
        public override bool isActive => grid != null;

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        protected override void OnBrushPickStarted()
        {
        }

        protected override void OnBrushPickDragged(BoundsInt position)
        {
<<<<<<< HEAD
            m_ActivePick = new RectInt(position.min.x, position.min.y, position.size.x, position.size.y);
=======
<<<<<<< HEAD
            m_ActivePick = new RectInt(position.min.x, position.min.y, position.size.x, position.size.y);
=======
            m_ActivePick = position;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void OnBrushPickCancelled()
        {
            m_ActivePick = null;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
            m_ActivePivot = Vector3Int.zero;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private void PingTileAsset(RectInt rect)
        {
            // Only able to ping asset if only one asset is selected
            if (rect.size == Vector2Int.zero && tilemap != null)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(rect.xMin, rect.yMin, zPosition));
                EditorGUIUtility.PingObject(tile);
                Selection.activeObject = tile;
            }
        }

        protected override bool ValidateFloodFillPosition(Vector3Int position)
        {
            return true;
        }

        protected override bool PickingIsDefaultTool()
        {
            return !m_Unlocked;
        }

        protected override bool CanPickOutsideEditMode()
        {
            return true;
        }

        protected override GridLayout.CellLayout CellLayout()
        {
            if (grid != null)
                return grid.cellLayout;
            return GridLayout.CellLayout.Rectangle;
        }

        protected override Vector2Int ScreenToGrid(Vector2 screenPosition)
        {
<<<<<<< HEAD
            Vector2 local = ScreenToLocal(screenPosition);
            Vector3Int result3 = grid.LocalToCell(local);
            Vector2Int result = new Vector2Int(result3.x, result3.y);
=======
<<<<<<< HEAD
            Vector2 local = ScreenToLocal(screenPosition);
            Vector3Int result3 = grid.LocalToCell(local);
            Vector2Int result = new Vector2Int(result3.x, result3.y);
=======
            Vector3 local = ScreenToLocal(screenPosition);
            var localS = Grid.Swizzle(m_CameraSwizzleView, local);
            var result3 = grid.LocalToCell(localS);
            var result = new Vector2Int(result3.x, result3.y);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            return result;
        }

        private void RenderSelectedBrushMarquee()
        {
<<<<<<< HEAD
            if (!unlocked && m_ActivePick.HasValue)
=======
<<<<<<< HEAD
            if (!unlocked && m_ActivePick.HasValue)
=======
            if (!activeDragAndDrop && !unlocked && m_ActivePick.HasValue)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            {
                DrawSelectionGizmo(m_ActivePick.Value);
            }
        }

<<<<<<< HEAD
        protected void DrawSelectionGizmo(RectInt rect)
=======
<<<<<<< HEAD
        protected void DrawSelectionGizmo(RectInt rect)
=======
        private void DrawSelectionGizmo(BoundsInt selectionBounds)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (Event.current.type != EventType.Repaint || !GUI.enabled)
                return;

            Color color = Color.white;
            if (isPicking)
                color = Color.cyan;

<<<<<<< HEAD
            GridEditorUtility.DrawGridMarquee(grid, new BoundsInt(new Vector3Int(rect.xMin, rect.yMin, 0), new Vector3Int(rect.width, rect.height, 1)), color);
=======
<<<<<<< HEAD
            GridEditorUtility.DrawGridMarquee(grid, new BoundsInt(new Vector3Int(rect.xMin, rect.yMin, 0), new Vector3Int(rect.width, rect.height, 1)), color);
=======
            GridEditorUtility.DrawGridMarquee(grid, new BoundsInt(new Vector3Int(selectionBounds.xMin, selectionBounds.yMin, 0), new Vector3Int(selectionBounds.size.x, selectionBounds.size.y, 1)), color);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private void HandleMouseEnterLeave()
        {
            if (guiRect.Contains(Event.current.mousePosition))
            {
                if (m_PreviousMousePosition.HasValue && !guiRect.Contains(m_PreviousMousePosition.Value) || !m_PreviousMousePosition.HasValue)
                {
                    if (GridPaintingState.activeBrushEditor != null)
                    {
                        GridPaintingState.activeBrushEditor.OnMouseEnter();
                    }
                }
            }
            else
            {
                if (m_PreviousMousePosition.HasValue && guiRect.Contains(m_PreviousMousePosition.Value) && !guiRect.Contains(Event.current.mousePosition))
                {
                    if (GridPaintingState.activeBrushEditor != null)
                    {
                        GridPaintingState.activeBrushEditor.OnMouseLeave();
                        Repaint();
                    }
                }
            }
        }

<<<<<<< HEAD
        private void CallOnPaintSceneGUI(Vector2Int position)
        {
=======
        private void CallOnSceneGUI()
        {
            var gridLayout = tilemap != null ? tilemap : grid as GridLayout;
            bool hasSelection = GridSelection.active  && GridSelection.target == brushTarget;
            if (hasSelection)
            {
                var rect = new RectInt(GridSelection.position.xMin, GridSelection.position.yMin, GridSelection.position.size.x, GridSelection.position.size.y);
<<<<<<< HEAD
                BoundsInt brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));
                GridBrushEditorBase.OnSceneGUIInternal(gridLayout, brushTarget, brushBounds, EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
=======
                var brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));
                GridBrushEditorBase.OnSceneGUIInternal(gridLayout, brushTarget, brushBounds, EditTypeToBrushTool(ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }
            if (GridPaintingState.activeBrushEditor != null)
            {
                GridPaintingState.activeBrushEditor.OnSceneGUI(gridLayout, brushTarget);
<<<<<<< HEAD
=======
                if (hasSelection)
                {
                    GridPaintingState.activeBrushEditor.OnSelectionSceneGUI(gridLayout, brushTarget);
                    if (GridSelectionTool.IsActive() && unlocked)
                    {
                        var tool = EditorToolManager.activeTool as GridSelectionTool;
                        tool.OnToolGUI();
                    }
                }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }
        }

        private void CallOnPaintSceneGUI(Vector2Int position)
        {
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (!unlocked && !TilemapEditorTool.IsActive(typeof(SelectTool)) && !TilemapEditorTool.IsActive(typeof(PickingTool)))
                return;

            bool hasSelection = GridSelection.active && GridSelection.target == brushTarget;
            if (!hasSelection && GridPaintingState.activeGrid != this)
                return;

            GridBrushBase brush = GridPaintingState.gridBrush;
<<<<<<< HEAD
=======
=======
            if (!activeDragAndDrop && !unlocked && !TilemapEditorTool.IsActive(typeof(SelectTool)) && !TilemapEditorTool.IsActive(typeof(PickingTool)))
                return;

            var hasSelection = GridSelection.active && GridSelection.target == brushTarget;
            if (!hasSelection && GridPaintingState.activeGrid != this)
                return;

            var brush = GridPaintingState.gridBrush;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (brush == null)
                return;

            var rect = new RectInt(position, new Vector2Int(1, 1));

            if (m_MarqueeStart.HasValue)
                rect = GridEditorUtility.GetMarqueeRect(position, m_MarqueeStart.Value);
            else if (hasSelection)
                rect = new RectInt(GridSelection.position.xMin, GridSelection.position.yMin, GridSelection.position.size.x, GridSelection.position.size.y);

<<<<<<< HEAD
            var gridLayout = tilemap != null ? tilemap : grid as GridLayout;
            BoundsInt brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));

            if (GridPaintingState.activeBrushEditor != null)
                GridPaintingState.activeBrushEditor.OnPaintSceneGUI(gridLayout, brushTarget, brushBounds, EditTypeToBrushTool(EditorTools.EditorTools.activeToolType), m_MarqueeStart.HasValue || executing);
            else // Fallback when user hasn't defined custom editor
                GridBrushEditorBase.OnPaintSceneGUIInternal(gridLayout, Selection.activeGameObject, brushBounds, EditTypeToBrushTool(EditorTools.EditorTools.activeToolType), m_MarqueeStart.HasValue || executing);
=======
            var gridLayout = tilemap != null ? tilemap.layoutGrid : grid as GridLayout;
<<<<<<< HEAD
            BoundsInt brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));

            if (GridPaintingState.activeBrushEditor != null)
                GridPaintingState.activeBrushEditor.OnPaintSceneGUI(gridLayout, brushTarget, brushBounds,
                    EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType),
                    m_MarqueeStart.HasValue || executing);
            else // Fallback when user hasn't defined custom editor
                GridBrushEditorBase.OnPaintSceneGUIInternal(gridLayout, Selection.activeGameObject, brushBounds,
                    EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType),
=======
            var brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));

            if (GridPaintingState.activeBrushEditor != null)
                GridPaintingState.activeBrushEditor.OnPaintSceneGUI(gridLayout, brushTarget, brushBounds,
                    EditTypeToBrushTool(ToolManager.activeToolType),
                    m_MarqueeStart.HasValue || executing);
            else // Fallback when user hasn't defined custom editor
                GridBrushEditorBase.OnPaintSceneGUIInternal(gridLayout, Selection.activeGameObject, brushBounds,
                    EditTypeToBrushTool(ToolManager.activeToolType),
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                    m_MarqueeStart.HasValue || executing);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void RegisterUndo()
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (!invalidClipboard)
            {
                Undo.RegisterFullObjectHierarchyUndo(paletteInstance, "Edit Palette");
            }
<<<<<<< HEAD
=======
=======
            if (palette != null && paletteInstance != null)
                Undo.RegisterFullObjectHierarchyUndo(paletteInstance, "Edit Palette");
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private void OnPaletteChanged()
        {
            m_PaletteUsed = true;
            m_PaletteNeedsSave = true;
            Undo.FlushUndoRecordObjects();
        }

<<<<<<< HEAD
=======
        public void CheckRevertIfChanged(string[] paths)
        {
            if (paths != null && m_PaletteNeedsSave && palette != null)
            {
                var currentPalettePath = AssetDatabase.GetAssetPath(palette);
                foreach (var path in paths)
                {
                    if (currentPalettePath == path)
                    {
                        m_PaletteNeedsSave = false;
                        ResetPreviewInstance();
                        Debug.LogWarningFormat(palette, paletteSavedOutsideClipboard, palette.name);
                        break;
                    }
                }
            }
        }

<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public void SavePaletteIfNecessary()
        {
            if (m_PaletteNeedsSave)
            {
                m_Owner.SavePalette();
                m_PaletteNeedsSave = false;
            }
<<<<<<< HEAD
=======
=======
        public bool SavePaletteIfNecessary()
        {
            bool needsSave = m_PaletteNeedsSave;
            if (needsSave)
            {
                SavePalette();
                m_PaletteNeedsSave = false;
            }
            return needsSave;
        }

        private void SavePalette()
        {
            if (palette != null && paletteInstance != null)
            {
                TilePaletteSaveUtility.SaveTilePalette(palette, paletteInstance);
                ResetPreviewInstance();
                Repaint();
            }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public Vector2 GridToScreen(Vector2 gridPosition)
        {
<<<<<<< HEAD
            Vector3 gridPosition3 = new Vector3(gridPosition.x, gridPosition.y, 0);
=======
<<<<<<< HEAD
            Vector3 gridPosition3 = new Vector3(gridPosition.x, gridPosition.y, 0);
=======
            var gridPosition3 = new Vector3(gridPosition.x, gridPosition.y, 0);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            return LocalToScreen(grid.CellToLocalInterpolated(gridPosition3));
        }

        public Vector2 ScreenToLocal(Vector2 screenPosition)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            Vector2 viewPosition = previewUtility.camera.transform.position;
            screenPosition -= new Vector2(guiRect.xMin, guiRect.yMin);
            Vector2 offsetFromCenter = new Vector2(screenPosition.x - guiRect.width * .5f, guiRect.height * .5f - screenPosition.y);
            return viewPosition + offsetFromCenter / LocalToScreenRatio();
<<<<<<< HEAD
=======
=======
            var viewPosition = m_PreviewUtility.camera.transform.position;
            Vector2 viewXYPosition = Grid.InverseSwizzle(m_CameraSwizzleView, viewPosition);
            screenPosition -= new Vector2(guiRect.xMin, guiRect.yMin);
            var offsetFromCenter = new Vector2(screenPosition.x - guiRect.width * .5f, guiRect.height * .5f - screenPosition.y);
            return viewXYPosition + offsetFromCenter / LocalToScreenRatio();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected Vector2 LocalToScreen(Vector2 localPosition)
        {
<<<<<<< HEAD
            Vector2 viewPosition = previewUtility.camera.transform.position;
            Vector2 offsetFromCenter = new Vector2(localPosition.x - viewPosition.x, viewPosition.y - localPosition.y);
=======
<<<<<<< HEAD
            Vector2 viewPosition = previewUtility.camera.transform.position;
            Vector2 offsetFromCenter = new Vector2(localPosition.x - viewPosition.x, viewPosition.y - localPosition.y);
=======
            var viewPosition = m_PreviewUtility.camera.transform.position;
            Vector2 viewXYPosition = Grid.InverseSwizzle(m_CameraSwizzleView, viewPosition);
            var offsetFromCenter = new Vector2(localPosition.x - viewXYPosition.x, viewXYPosition.y - localPosition.y);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            return offsetFromCenter * LocalToScreenRatio() + new Vector2(guiRect.width * .5f + guiRect.xMin, guiRect.height * .5f + guiRect.yMin);
        }

        private float LocalToScreenRatio()
        {
<<<<<<< HEAD
            return guiRect.height / (previewUtility.camera.orthographicSize * 2f);
=======
<<<<<<< HEAD
            return guiRect.height / (previewUtility.camera.orthographicSize * 2f);
=======
            return guiRect.height / (m_PreviewUtility.camera.orthographicSize * 2f);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private float LocalToScreenRatio(float viewHeight)
        {
<<<<<<< HEAD
            return viewHeight / (previewUtility.camera.orthographicSize * 2f);
=======
<<<<<<< HEAD
            return viewHeight / (previewUtility.camera.orthographicSize * 2f);
=======
            return viewHeight / (m_PreviewUtility.camera.orthographicSize * 2f);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private static bool TilemapIsEmpty(Tilemap tilemap)
        {
            return tilemap.GetUsedTilesCount() == 0;
        }
<<<<<<< HEAD
=======

        public void UnlockAndEdit()
        {
            unlocked = true;
            m_PaletteNeedsSave = true;
        }
<<<<<<< HEAD
=======

        // TODO: Better way of clearing caches than AssetPostprocessor
        public class AssetProcessor : AssetPostprocessor
        {
            public override int GetPostprocessOrder()
            {
                return int.MaxValue;
            }

            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
            {
                foreach (var clipboard in instances)
                {
                    clipboard.DelayedResetPreviewInstance();
                }
            }
        }

        public class PaletteAssetModificationProcessor : AssetModificationProcessor
        {
            static void OnWillCreateAsset(string assetName)
            {
                SavePalettesIfRequired(null);
            }

            static string[] OnWillSaveAssets(string[] paths)
            {
                SavePalettesIfRequired(paths);
                return paths;
            }

            static void SavePalettesIfRequired(string[] paths)
            {
                if (GridPaintingState.savingPalette)
                    return;

                foreach (var clipboard in instances)
                {
                    if (clipboard.isModified)
                    {
                        clipboard.CheckRevertIfChanged(paths);
                        clipboard.SavePaletteIfNecessary();
                        clipboard.Repaint();
                    }
                }
            }
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
