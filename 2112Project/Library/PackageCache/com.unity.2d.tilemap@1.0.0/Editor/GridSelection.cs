using System;
<<<<<<< HEAD
using UnityEngine;
=======
<<<<<<< HEAD
using UnityEngine;
=======
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
using UnityEngine.Scripting.APIUpdating;
using Object = UnityEngine.Object;

namespace UnityEditor.Tilemaps
{
    /// <summary>Stores the selection made on a GridLayout.</summary>
    [MovedFrom(true, "UnityEditor", "UnityEditor")]
<<<<<<< HEAD
    public class GridSelection : ScriptableObject
    {
        /// <summary>Callback for when the active GridSelection has changed.</summary>
        public static event Action gridSelectionChanged;
        private BoundsInt m_Position;
        private GameObject m_Target;
        [SerializeField] private Object m_PreviousSelection;

=======
    [HelpURL("https://docs.unity3d.com/Manual/TilemapPainting-SelectionTool.html#GridSelect")]
    [Serializable]
    public class GridSelection : ScriptableObject
    {
<<<<<<< HEAD
        public static string kUpdateGridSelection = L10n.Tr("Update Grid Selection");

        /// <summary>Callback for when the active GridSelection has changed.</summary>
        public static event Action gridSelectionChanged;
        [SerializeField]
        private BoundsInt m_Position;
=======
        private static string kUpdateGridSelection = L10n.Tr("Update Grid Selection");

        /// <summary>Callback for when the active GridSelection has changed.</summary>
        public static event Action gridSelectionChanged;

        [SerializeField]
        private BoundsInt m_Position;
        [SerializeField]
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        private GameObject m_Target;
        [SerializeField]
        private Object m_PreviousSelection;

<<<<<<< HEAD
=======
        [SerializeField]
        private Scene m_Scene;
        [SerializeField]
        private GameObject m_OriginalPalette;

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        /// <summary>Whether there is an active GridSelection made on a GridLayout.</summary>
        public static bool active { get { return Selection.activeObject is GridSelection && selection.m_Target != null; } }

        private static GridSelection selection { get { return Selection.activeObject as GridSelection; } }

        /// <summary>The cell coordinates of the active GridSelection made on the GridLayout.</summary>
        public static BoundsInt position
        {
            get { return selection != null ? selection.m_Position : new BoundsInt(); }
            set
            {
                if (selection != null && selection.m_Position != value)
                {
<<<<<<< HEAD
=======
                    RegisterUndo();
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                    selection.m_Position = value;
                    if (gridSelectionChanged != null)
                        gridSelectionChanged();
                }
            }
        }

        /// <summary>The GameObject of the GridLayout where the active GridSelection was made.</summary>
        public static GameObject target { get { return selection != null ? selection.m_Target : null; } }

        /// <summary>The Grid of the target of the active GridSelection.</summary>
        public static Grid grid { get { return selection != null && selection.m_Target != null ? selection.m_Target.GetComponentInParent<Grid>() : null; } }

        /// <summary>Creates a new GridSelection and sets it as the active GridSelection.</summary>
        /// <param name="target">The target GameObject for the GridSelection.</param>
        /// <param name="bounds">The cell coordinates of selection made.</param>
        public static void Select(Object target, BoundsInt bounds)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            GridSelection newSelection = CreateInstance<GridSelection>();
            newSelection.m_PreviousSelection = Selection.activeObject;
            newSelection.m_Target = target as GameObject;
            newSelection.m_Position = bounds;
            Selection.activeObject = newSelection;
<<<<<<< HEAD
=======
=======
            var newSelection = CreateInstance<GridSelection>();
            newSelection.m_PreviousSelection = Selection.activeObject;
            newSelection.m_Target = target as GameObject;
            newSelection.m_Position = bounds;
            newSelection.m_OriginalPalette = null;
            Undo.RegisterCreatedObjectUndo(newSelection, kUpdateGridSelection);

            var currentGroup = Undo.GetCurrentGroup();
            Selection.activeObject = newSelection;
            Undo.CollapseUndoOperations(currentGroup);

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
            if (gridSelectionChanged != null)
                gridSelectionChanged();
        }

        /// <summary>Clears the active GridSelection.</summary>
        public static void Clear()
        {
            if (active)
            {
<<<<<<< HEAD
                selection.m_Position = new BoundsInt();
                Selection.activeObject = selection.m_PreviousSelection;
=======
                RegisterUndo();
                selection.m_Position = new BoundsInt();
<<<<<<< HEAD
                Selection.activeObject = selection.m_PreviousSelection;
=======
                if (selection.m_Scene.IsValid())
                {
                    DestroyImmediate(selection.m_Target);
                    selection.m_Target = null;
                    selection.m_OriginalPalette = null;
                    EditorSceneManager.ClosePreviewScene(selection.m_Scene);
                }
                Selection.activeObject = selection.m_PreviousSelection;

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                if (gridSelectionChanged != null)
                    gridSelectionChanged();
            }
        }
<<<<<<< HEAD
=======

<<<<<<< HEAD
=======
        internal static void SaveStandalone()
        {
            if (!selection.m_Scene.IsValid()
                || selection.m_OriginalPalette == null
                || selection.m_Target == null)
                return;

            TilePaletteSaveUtility.SaveTilePalette(selection.m_OriginalPalette, selection.m_Target.transform.root.gameObject);
        }

        internal static void TransferToStandalone(GameObject palette)
        {
            if (!active)
                return;

            if (!selection.m_Scene.IsValid())
            {
                selection.m_Scene = EditorSceneManager.NewPreviewScene();
                if (!selection.m_Scene.IsValid())
                    throw new InvalidOperationException("Preview scene could not be created");
            }

            SceneManager.MoveGameObjectToScene(selection.m_Target.transform.root.gameObject, selection.m_Scene);
            selection.m_OriginalPalette = palette;
        }

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        internal static void RegisterUndo()
        {
            if (selection != null)
                Undo.RegisterCompleteObjectUndo(selection, kUpdateGridSelection);
        }
<<<<<<< HEAD
=======

        private void OnDisable()
        {
            Clear();
        }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    }
}
