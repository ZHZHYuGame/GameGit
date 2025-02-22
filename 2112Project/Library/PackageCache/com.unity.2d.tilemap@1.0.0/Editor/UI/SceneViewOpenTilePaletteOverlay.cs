using UnityEditor.Overlays;
using UnityEditor.Toolbars;
<<<<<<< HEAD

namespace UnityEditor.Tilemaps
{
    [Overlay(typeof(SceneView), k_OverlayId, k_DisplayName)]
=======
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.Tilemaps
{
    [Overlay(typeof(SceneView), k_OverlayId, k_DisplayName
        , defaultDockZone = DockZone.RightColumn
        , defaultDockPosition = DockPosition.Bottom
        , defaultDockIndex = 0
        , defaultLayout = Layout.Panel)]
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
    internal class SceneViewOpenTilePaletteOverlay : ToolbarOverlay, ITransientOverlay
    {
        internal const string k_OverlayId = "Scene View/Open Tile Palette";
        private const string k_DisplayName = "Open Tile Palette";

<<<<<<< HEAD
        public SceneViewOpenTilePaletteOverlay() : base("Tile Palette/Open Palette") {}
=======
        public SceneViewOpenTilePaletteOverlay() : base(TilePaletteOpenPalette.k_ToolbarId) {}
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611

        public bool visible => SceneViewOpenTilePaletteHelper.showInSceneViewActive && SceneViewOpenTilePaletteHelper.IsActive();
    }

<<<<<<< HEAD
    [EditorToolbarElement("Tile Palette/Open Palette")]
    sealed class TilePaletteOpenPalette : EditorToolbarButton
    {
=======
    [EditorToolbarElement(k_ToolbarId)]
    sealed class TilePaletteOpenPalette : EditorToolbarButton
    {
        internal const string k_ToolbarId = "Tile Palette/Open Palette";

>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        const string k_ToolSettingsClass = "unity-tool-settings";

        private static string k_LabelText = L10n.Tr("Open Tile Palette");
        private static string k_TooltipText = L10n.Tr("Opens the Tile Palette Window");
<<<<<<< HEAD
=======
        private const string k_IconPath = "Packages/com.unity.2d.tilemap/Editor/Icons/Tilemap.TilePalette.png";
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611

        public TilePaletteOpenPalette() : base(SceneViewOpenTilePaletteHelper.OpenTilePalette)
        {
            name = "Open Tile Palette";
            AddToClassList(k_ToolSettingsClass);

<<<<<<< HEAD
            icon = EditorGUIUtility.LoadIconRequired("Tilemap Icon");
=======
            icon = EditorGUIUtility.LoadIconRequired(k_IconPath);
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            text = k_LabelText;
            tooltip = k_TooltipText;
        }
    }
}
