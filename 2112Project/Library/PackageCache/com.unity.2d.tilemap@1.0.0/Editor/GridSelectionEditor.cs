<<<<<<< HEAD
=======
using System;
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    [CustomEditor(typeof(GridSelection))]
    internal class GridSelectionEditor : Editor
    {
        private const float iconSize = 32f;

        static class Styles
        {
            public static readonly GUIContent gridSelectionLabel = EditorGUIUtility.TrTextContent("Grid Selection");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

            public static readonly string iconPath = "Packages/com.unity.2d.tilemap/Editor/Icons/GridSelection.png";
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        }

        private void OnValidate()
        {
            var position = GridSelection.position;
            GridSelection.position = new BoundsInt(position.min, position.max - position.min);
        }

        private void OnEnable()
        {
            // Give focus to Inspector window for keyboard actions
<<<<<<< HEAD
            EditorWindow.FocusWindowIfItsOpen<InspectorWindow>();
=======
            EditorApplication.delayCall += () => EditorWindow.FocusWindowIfItsOpen<InspectorWindow>();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        public override void OnInspectorGUI()
        {
<<<<<<< HEAD
=======
            EditorGUI.BeginChangeCheck();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (GridPaintingState.activeBrushEditor && GridSelection.active)
            {
                GridPaintingState.activeBrushEditor.OnSelectionInspectorGUI();
            }
<<<<<<< HEAD
=======
            if (EditorGUI.EndChangeCheck())
            {
                if (GridPaintingState.IsPartOfActivePalette(GridSelection.target))
                {
                    GridPaintingState.UnlockGridPaintPaletteClipboardForEditing();
                    GridPaintingState.RepaintGridPaintPaletteWindow();
                }
<<<<<<< HEAD
=======
                else
                {
                    GridSelection.SaveStandalone();
                }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        }

        protected override void OnHeaderGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.inspectorBig);
<<<<<<< HEAD
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(Grid));
            GUILayout.Label(icon, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            EditorGUILayout.BeginVertical();
            GUILayout.Label(Styles.gridSelectionLabel);
            GridSelection.position = EditorGUILayout.BoundsIntField(GUIContent.none, GridSelection.position);
=======
<<<<<<< HEAD
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(Grid));
=======
            Texture2D icon = EditorGUIUtility.LoadIcon(Styles.iconPath);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            GUILayout.Label(icon, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            EditorGUILayout.BeginVertical();
            GUILayout.Label(Styles.gridSelectionLabel);
            EditorGUI.BeginChangeCheck();
            GridSelection.position = EditorGUILayout.BoundsIntField(GUIContent.none, GridSelection.position);
            if (EditorGUI.EndChangeCheck())
            {
                OnValidate();
            }
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            DrawHeaderHelpAndSettingsGUI(GUILayoutUtility.GetLastRect());
        }

        public bool HasFrameBounds()
        {
            return GridSelection.active;
        }

        public Bounds OnGetFrameBounds()
        {
            Bounds bounds = new Bounds();
            if (GridSelection.active)
            {
                Vector3Int gridMin = GridSelection.position.min;
                Vector3Int gridMax = GridSelection.position.max;

                Vector3 min = GridSelection.grid.CellToWorld(gridMin);
                Vector3 max = GridSelection.grid.CellToWorld(gridMax);

                bounds = new Bounds((max + min) * .5f, max - min);
            }
<<<<<<< HEAD
=======

>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            return bounds;
        }
    }
}
