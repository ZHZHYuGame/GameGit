<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
=======
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.Tilemaps
{
    /// <summary> This class is in charge of drag'n'drops of Tile assets on scene view </summary>
    internal class TileDragAndDropManager : ScriptableSingleton<TileDragAndDropManager>
    {
        private bool m_RegisteredEventHandlers;
<<<<<<< HEAD
        private Dictionary<Vector2Int, Object> m_HoverData;
=======
        private Dictionary<Vector2Int, TileDragAndDropHoverData> m_HoverData;

        [SerializeField]
        private string m_LastUserTileAssetPath;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            instance.RegisterEventHandlers();
        }

        void OnEnable()
        {
            RegisterEventHandlers();
        }

        void RegisterEventHandlers()
        {
            if (m_RegisteredEventHandlers)
                return;

            SceneView.duringSceneGui += DuringSceneGui;
            m_RegisteredEventHandlers = true;
        }

        void OnDisable()
        {
            SceneView.duringSceneGui -= DuringSceneGui;
            m_RegisteredEventHandlers = false;
        }

        private void DuringSceneGui(SceneView sceneView)
        {
            Event evt = Event.current;
            if (evt.type != EventType.DragUpdated && evt.type != EventType.DragPerform && evt.type != EventType.DragExited && evt.type != EventType.Repaint)
                return;

            Grid activeGrid = GetActiveGrid();
            if (activeGrid == null || DragAndDrop.objectReferences.Length == 0)
                return;

            Vector3 localMouse = GridEditorUtility.ScreenToLocal(activeGrid.transform, evt.mousePosition);
            Vector3Int mouseGridPosition = activeGrid.LocalToCell(localMouse);

            switch (evt.type)
            {
                //TODO: Cache this
                case EventType.DragUpdated:
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    List<TileBase> tiles = TileDragAndDrop.GetValidTiles(DragAndDrop.objectReferences);
<<<<<<< HEAD
                    instance.m_HoverData = TileDragAndDrop.CreateHoverData(null, null, tiles);
=======
                    instance.m_HoverData = TileDragAndDrop.CreateHoverData(null, null, tiles, activeGrid.cellLayout);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    if (instance.m_HoverData.Count > 0)
                    {
                        Event.current.Use();
                        GUI.changed = true;
                    }
                    break;
                case EventType.DragPerform:
                    if (instance.m_HoverData.Count > 0)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
<<<<<<< HEAD
                        Dictionary<Vector2Int, TileBase> tileSheet = TileDragAndDrop.ConvertToTileSheet(instance.m_HoverData);
                        Tilemap tilemap = GetOrCreateActiveTilemap();
                        tilemap.ClearAllEditorPreviewTiles();
                        foreach (KeyValuePair<Vector2Int, TileBase> item in tileSheet)
                        {
                            Vector3Int position = new Vector3Int(mouseGridPosition.x + item.Key.x, mouseGridPosition.y + item.Key.y, 0);
                            tilemap.SetTile(position, item.Value);
=======
                        var tileSheet = TileDragAndDrop.ConvertToTileSheet(instance.m_HoverData);
                        Tilemap tilemap = GetOrCreateActiveTilemap();
                        tilemap.ClearAllEditorPreviewTiles();
                        int i = 0;
                        foreach (KeyValuePair<Vector2Int, TileDragAndDropHoverData> item in instance.m_HoverData)
                        {
                            Vector3Int position = new Vector3Int(mouseGridPosition.x + item.Key.x, mouseGridPosition.y + item.Key.y, 0);
                            tilemap.SetTile(position, tileSheet[i++]);
                            tilemap.SetTransformMatrix(position, Matrix4x4.TRS(
                                item.Value.hasOffset ? item.Value.positionOffset - tilemap.tileAnchor : Vector3.zero
                                , Quaternion.identity
                                , Vector3.one));
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        }
                        instance.m_HoverData = null;
                        GUI.changed = true;
                        Event.current.Use();
                    }
                    break;
                case EventType.Repaint:
                    if (instance.m_HoverData != null)
                    {
                        Tilemap map = Selection.activeGameObject.GetComponentInParent<Tilemap>();

                        if (map != null)
                            map.ClearAllEditorPreviewTiles();

                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
<<<<<<< HEAD
                        foreach (KeyValuePair<Vector2Int, Object> item in instance.m_HoverData)
                        {
                            Vector3Int gridPos = mouseGridPosition + new Vector3Int(item.Key.x, item.Key.y, 0);
                            if (item.Value is TileBase)
                            {
                                TileBase tile = item.Value as TileBase;
=======
                        foreach (KeyValuePair<Vector2Int, TileDragAndDropHoverData> item in instance.m_HoverData)
                        {
                            Vector3Int gridPos = mouseGridPosition + new Vector3Int(item.Key.x, item.Key.y, 0);
                            if (item.Value.hoverObject is TileBase)
                            {
                                TileBase tile = item.Value.hoverObject as TileBase;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                                if (map != null)
                                {
                                    map.SetEditorPreviewTile(gridPos, tile);
                                }
                            }
                        }
                    }
                    break;
            }

            if (instance.m_HoverData != null && (
                Event.current.type == EventType.DragExited ||
                Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape))
            {
                if (instance.m_HoverData.Count > 0)
                {
                    Tilemap map = Selection.activeGameObject.GetComponentInParent<Tilemap>();
                    if (map != null)
                        map.ClearAllEditorPreviewTiles();

                    Event.current.Use();
                }

                instance.m_HoverData = null;
            }
        }

<<<<<<< HEAD
=======
        internal static string GetDefaultTileAssetDirectoryPath()
        {
            var path = instance.m_LastUserTileAssetPath;
            if (String.IsNullOrEmpty(path))
            {
                path = ProjectBrowser.s_LastInteractedProjectBrowser != null
                    ? ProjectBrowser.s_LastInteractedProjectBrowser.GetActiveFolderPath()
                    : "Assets";
            }
            return path;
        }

        internal static void SetUserTileAssetDirectoryPath(string path)
        {
            var directoryPath = String.Empty;
            if (!String.IsNullOrEmpty(path))
            {
                // UUM-29240: UnityGetDirectoryName clips off last directory if path is not a file path
                if (String.IsNullOrEmpty(FileUtil.GetPathExtension(path))
                    && !path.EndsWith("/") // MacOS
                    && !File.Exists(path))
                {
                    path = FileUtil.CombinePaths(path, "");
                }
                directoryPath = FileUtil.UnityGetDirectoryName(path);
            }
            instance.m_LastUserTileAssetPath = directoryPath;
        }

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        static Tilemap GetOrCreateActiveTilemap()
        {
            if (Selection.activeGameObject != null)
            {
                Tilemap tilemap = Selection.activeGameObject.GetComponentInParent<Tilemap>();
                if (tilemap == null)
                {
                    Grid grid = Selection.activeGameObject.GetComponentInParent<Grid>();
                    tilemap = CreateNewTilemap(grid);
                }
                return tilemap;
            }
            return null;
        }

        static Tilemap CreateNewTilemap(Grid grid)
        {
            GameObject go = new GameObject("Tilemap");
            go.transform.SetParent(grid.gameObject.transform);
            Tilemap map = go.AddComponent<Tilemap>();
            go.AddComponent<TilemapRenderer>();
            return map;
        }

<<<<<<< HEAD
        public static Grid GetActiveGrid()
=======
        static Grid GetActiveGrid()
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (Selection.activeGameObject != null)
            {
                return Selection.activeGameObject.GetComponentInParent<Grid>();
            }
            return null;
        }
    }
}
