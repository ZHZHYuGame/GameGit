using System;
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
<<<<<<< HEAD
=======
=======
using UnityEngine;
using UnityEngine.Tilemaps;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

namespace UnityEditor.Tilemaps
{
    internal class PaintableSceneViewGrid : PaintableGrid
    {
        private Transform gridTransform { get { return grid != null ? grid.transform : null; } }
        private Grid grid { get { return brushTarget != null ? brushTarget.GetComponentInParent<Grid>() : (Selection.activeGameObject != null ? Selection.activeGameObject.GetComponentInParent<Grid>() : null); } }
        private GridBrushBase gridBrush { get { return GridPaintingState.gridBrush; } }
        private SceneView activeSceneView;
        private int sceneViewTransformHash;

        private GameObject brushTarget
        {
            get { return GridPaintingState.scenePaintTarget; }
        }

        public Tilemap tilemap
        {
            get
            {
                if (brushTarget != null)
                {
                    return brushTarget.GetComponent<Tilemap>();
                }
                return null;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SceneView.duringSceneGui += OnSceneGUI;
            Undo.undoRedoPerformed += UndoRedoPerformed;
            GridSelection.gridSelectionChanged += OnGridSelectionChanged;
        }

        protected override void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            Undo.undoRedoPerformed -= UndoRedoPerformed;
            GridSelection.gridSelectionChanged -= OnGridSelectionChanged;
            base.OnDisable();
        }

        private void OnGridSelectionChanged()
        {
            SceneView.RepaintAll();
        }

        private Rect GetSceneViewPositionRect(SceneView sceneView)
        {
            return new Rect(0, 0, sceneView.position.width, sceneView.position.height);
        }

        public void OnSceneGUI(SceneView sceneView)
        {
            HandleMouseEnterLeave(sceneView);

<<<<<<< HEAD
=======
            CallOnSceneGUI();

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            // Case 1093801: Handle only the currently active scene view
            if (sceneView != activeSceneView)
                return;

            // Case 1077400: SceneView camera transform changes may update the mouse grid position even though the mouse position has not changed
            var currentSceneViewTransformHash = sceneView.camera.transform.localToWorldMatrix.GetHashCode();
<<<<<<< HEAD
            UpdateMouseGridPosition(currentSceneViewTransformHash == sceneViewTransformHash);
=======
            UpdateMouseGridPosition(currentSceneViewTransformHash != sceneViewTransformHash);
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            sceneViewTransformHash = currentSceneViewTransformHash;

            var dot = 1.0f;
            var gridView = GetGridView();
            if (gridView != null)
            {
                dot = Math.Abs(Vector3.Dot(sceneView.camera.transform.forward, Grid.Swizzle(gridView.cellSwizzle, gridView.transform.forward)));
            }

            // Case 1021655: Validate that grid is not totally parallel to view (+-5 degrees), otherwise tiles could be accidentally painted on large positions
            if (dot > 0.1f)
            {
                base.OnGUI();
                if (InGridEditMode())
                {
<<<<<<< HEAD
                    CallOnSceneGUI();
=======
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    if ((grid != null) && (GridPaintingState.activeGrid == this || GridSelection.active))
                    {
                        CallOnPaintSceneGUI();
                    }
                    if (Event.current.type == EventType.Repaint)
                        EditorGUIUtility.AddCursorRect(GetSceneViewPositionRect(sceneView), MouseCursor.CustomCursor);
                }
            }
        }

        private void HandleMouseEnterLeave(SceneView sceneView)
        {
<<<<<<< HEAD
            if (inEditMode)
=======
<<<<<<< HEAD
            if (inEditMode)
=======
            if (GridPaintingState.isEditing)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            {
                if (Event.current.type == EventType.MouseEnterWindow)
                {
                    OnMouseEnter(sceneView);
                }
                else if (Event.current.type == EventType.MouseLeaveWindow)
                {
<<<<<<< HEAD
                    OnMouseLeave(sceneView);
=======
<<<<<<< HEAD
                    OnMouseLeave(sceneView);
=======
                    OnMouseLeave();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                }
                // Case 1043365: When docked, the docking area is considered part of the window and MouseEnter/LeaveWindow events are not considered when entering the docking area
                else if (sceneView.docked)
                {
                    var guiPoint = Event.current.mousePosition;
                    var sceneViewPosition = GetSceneViewPositionRect(sceneView);
                    if (sceneViewPosition.Contains(guiPoint))
                    {
                        if (GridPaintingState.activeGrid != this)
                        {
                            OnMouseEnter(sceneView);
                        }
                    }
                    else if (activeSceneView == sceneView)
                    {
                        if (GridPaintingState.activeGrid == this)
                        {
<<<<<<< HEAD
                            OnMouseLeave(sceneView);
=======
<<<<<<< HEAD
                            OnMouseLeave(sceneView);
=======
                            OnMouseLeave();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                        }
                    }
                }
            }
        }

        private void OnMouseEnter(SceneView sceneView)
        {
            if (GridPaintingState.activeBrushEditor != null)
                GridPaintingState.activeBrushEditor.OnMouseEnter();
            GridPaintingState.activeGrid = this;
            activeSceneView = sceneView;
<<<<<<< HEAD
            ResetPreviousMousePositionToCurrentPosition();
        }

        private void OnMouseLeave(SceneView sceneView)
=======
            UpdateMouseGridPosition(true);
            ResetPreviousMousePositionToCurrentPosition();
        }

<<<<<<< HEAD
        private void OnMouseLeave(SceneView sceneView)
=======
        private void OnMouseLeave()
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (GridPaintingState.activeBrushEditor != null)
                GridPaintingState.activeBrushEditor.OnMouseLeave();
            GridPaintingState.activeGrid = null;
            activeSceneView = null;
        }

        private void UndoRedoPerformed()
        {
            RefreshAllTiles();
        }

        private void RefreshAllTiles()
        {
            if (tilemap != null)
                tilemap.RefreshAllTiles();
        }

        protected override void RegisterUndo()
        {
            if (GridPaintingState.activeBrushEditor != null)
            {
<<<<<<< HEAD
                GridPaintingState.activeBrushEditor.RegisterUndo(brushTarget, EditTypeToBrushTool(EditorTools.EditorTools.activeToolType));
=======
<<<<<<< HEAD
                GridPaintingState.activeBrushEditor.RegisterUndo(brushTarget, EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType));
=======
                GridPaintingState.activeBrushEditor.RegisterUndo(brushTarget, EditTypeToBrushTool(EditorTools.ToolManager.activeToolType));
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        protected override void Paint(Vector3Int position)
        {
            if (grid != null)
                gridBrush.Paint(grid, brushTarget, position);
        }

        protected override void Erase(Vector3Int position)
        {
            if (grid != null)
                gridBrush.Erase(grid, brushTarget, position);
        }

        protected override void BoxFill(BoundsInt position)
        {
            if (grid != null)
                gridBrush.BoxFill(grid, brushTarget, position);
        }

        protected override void BoxErase(BoundsInt position)
        {
            if (grid != null)
                gridBrush.BoxErase(grid, brushTarget, position);
        }

        protected override void FloodFill(Vector3Int position)
        {
            if (grid != null)
                gridBrush.FloodFill(grid, brushTarget, position);
        }

        protected override void PickBrush(BoundsInt position, Vector3Int pickStart)
        {
            if (grid != null)
                gridBrush.Pick(grid, brushTarget, position, pickStart);
        }

        protected override void Select(BoundsInt position)
        {
            if (grid != null)
            {
                GridSelection.Select(brushTarget, position);
                gridBrush.Select(grid, brushTarget, position);
            }
        }

        protected override void Move(BoundsInt from, BoundsInt to)
        {
            if (grid != null)
                gridBrush.Move(grid, brushTarget, from, to);
        }

        protected override void MoveStart(BoundsInt position)
        {
            if (grid != null)
                gridBrush.MoveStart(grid, brushTarget, position);
        }

        protected override void MoveEnd(BoundsInt position)
        {
            if (grid != null)
                gridBrush.MoveEnd(grid, brushTarget, position);
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
        protected override bool CustomTool(bool isHotControl, TilemapEditorTool tool, Vector3Int position)
=======
        protected override bool CustomTool(bool isToolHotControl, TilemapEditorTool tool, Vector3Int position)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        {
            var executed = false;
            if (grid != null)
            {
<<<<<<< HEAD
                executed = tool.HandleTool(isHotControl, grid, brushTarget, position);
=======
                executed = tool.HandleTool(isToolHotControl, grid, brushTarget, position);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }
            return executed;
        }

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        protected override void OnEditStart()
        {
            if (GridPaintingState.activeBrushEditor != null && grid != null)
                GridPaintingState.activeBrushEditor.OnEditStart(grid, brushTarget);
        }

        protected override void OnEditEnd()
        {
            if (GridPaintingState.activeBrushEditor != null && grid != null)
                GridPaintingState.activeBrushEditor.OnEditEnd(grid, brushTarget);
        }

        protected override void ClearGridSelection()
        {
            GridSelection.Clear();
        }

<<<<<<< HEAD
=======
        public override bool isActive => grid != null;

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public override void Repaint()
        {
            SceneView.RepaintAll();
        }

        protected override bool ValidateFloodFillPosition(Vector3Int position)
        {
            return true;
        }

        protected override Vector2Int ScreenToGrid(Vector2 screenPosition)
        {
            if (tilemap != null)
            {
                var transform = tilemap.transform;
                Plane plane = new Plane(GetGridForward(tilemap), transform.position);
                Vector3Int cell = LocalToGrid(tilemap, GridEditorUtility.ScreenToLocal(transform, screenPosition, plane));
                return new Vector2Int(cell.x, cell.y);
            }
            if (grid != null)
            {
                Vector3Int cell = LocalToGrid(grid, GridEditorUtility.ScreenToLocal(gridTransform, screenPosition, GetGridPlane(grid)));
                return new Vector2Int(cell.x, cell.y);
            }
            return Vector2Int.zero;
        }

        protected override bool PickingIsDefaultTool()
        {
            return false;
        }

        protected override bool CanPickOutsideEditMode()
        {
            return false;
        }

        protected override GridLayout.CellLayout CellLayout()
        {
            return grid.cellLayout;
        }

        Vector3Int LocalToGrid(GridLayout gridLayout, Vector3 local)
        {
            return gridLayout.LocalToCell(local);
        }

        private Vector3 GetGridForward(GridLayout gridLayout)
        {
            switch (gridLayout.cellSwizzle)
            {
                case GridLayout.CellSwizzle.XYZ:
                    return gridLayout.transform.forward * -1f;
                case GridLayout.CellSwizzle.XZY:
                    return gridLayout.transform.up * -1f;
                case GridLayout.CellSwizzle.YXZ:
                    return gridLayout.transform.forward;
                case GridLayout.CellSwizzle.YZX:
                    return gridLayout.transform.up;
                case GridLayout.CellSwizzle.ZXY:
                    return gridLayout.transform.right;
                case GridLayout.CellSwizzle.ZYX:
                    return gridLayout.transform.right * -1f;
            }
            return gridLayout.transform.forward * -1f;
        }

<<<<<<< HEAD
        private Plane GetGridPlane(Grid grid)
        {
            return new Plane(GetGridForward(grid), grid.transform.position);
=======
<<<<<<< HEAD
        private Plane GetGridPlane(Grid grid)
        {
            return new Plane(GetGridForward(grid), grid.transform.position);
=======
        private Plane GetGridPlane(Grid planeForGrid)
        {
            return new Plane(GetGridForward(planeForGrid), planeForGrid.transform.position);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        private GridLayout GetGridView()
        {
            if (tilemap != null)
                return tilemap;
            if (grid != null)
                return grid;
            return null;
        }

        void CallOnPaintSceneGUI()
        {
            bool hasSelection = GridSelection.active && GridSelection.target == brushTarget;
            if (!hasSelection && GridPaintingState.activeGrid != this)
                return;

            RectInt rect = new RectInt(mouseGridPosition, new Vector2Int(1, 1));

            if (m_MarqueeStart.HasValue)
                rect = GridEditorUtility.GetMarqueeRect(mouseGridPosition, m_MarqueeStart.Value);
            else if (hasSelection)
                rect = new RectInt(GridSelection.position.xMin, GridSelection.position.yMin, GridSelection.position.size.x, GridSelection.position.size.y);

<<<<<<< HEAD
            var layoutGrid = tilemap != null ? tilemap : grid as GridLayout;
=======
            var layoutGrid = tilemap != null ? tilemap.layoutGrid : grid as GridLayout;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            BoundsInt brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));
            if (GridPaintingState.activeBrushEditor != null)
            {
                GridPaintingState.activeBrushEditor.OnPaintSceneGUI(layoutGrid, brushTarget, brushBounds
<<<<<<< HEAD
                    , EditTypeToBrushTool(EditorTools.EditorTools.activeToolType), m_MarqueeStart.HasValue || executing);
=======
<<<<<<< HEAD
                    , EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
=======
                    , EditTypeToBrushTool(EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
            else // Fallback when user hasn't defined custom editor
            {
                GridBrushEditorBase.OnPaintSceneGUIInternal(layoutGrid, brushTarget, brushBounds
<<<<<<< HEAD
                    , EditTypeToBrushTool(EditorTools.EditorTools.activeToolType), m_MarqueeStart.HasValue || executing);
=======
<<<<<<< HEAD
                    , EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
=======
                    , EditTypeToBrushTool(EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }

        void CallOnSceneGUI()
        {
<<<<<<< HEAD
            if (GridPaintingState.activeBrushEditor != null)
            {
                MethodInfo methodInfo = GridPaintingState.activeBrushEditor.GetType().GetMethod("OnSceneGUI");
                if (methodInfo != null)
                    methodInfo.Invoke(GridPaintingState.activeBrushEditor, null);
=======
            var gridLayout = tilemap != null ? tilemap : grid as GridLayout;
            bool hasSelection = GridSelection.active  && GridSelection.target == brushTarget;
            if (GridPaintingState.activeBrushEditor != null)
            {
                GridPaintingState.activeBrushEditor.OnSceneGUI(gridLayout, brushTarget);
                if (hasSelection)
                {
                    GridPaintingState.activeBrushEditor.OnSelectionSceneGUI(gridLayout, brushTarget);
                }
            }

            if (hasSelection)
            {
                RectInt rect = new RectInt(GridSelection.position.xMin, GridSelection.position.yMin, GridSelection.position.size.x, GridSelection.position.size.y);
                BoundsInt brushBounds = new BoundsInt(new Vector3Int(rect.x, rect.y, zPosition), new Vector3Int(rect.width, rect.height, 1));
                GridBrushEditorBase.OnSceneGUIInternal(gridLayout, brushTarget, brushBounds
<<<<<<< HEAD
                    , EditTypeToBrushTool(UnityEditor.EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
=======
                    , EditTypeToBrushTool(EditorTools.ToolManager.activeToolType), m_MarqueeStart.HasValue || executing);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            }
        }
    }
}
