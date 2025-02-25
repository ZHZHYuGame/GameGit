using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.Tilemaps
{
    internal class GridPalettes : ScriptableSingleton<GridPalettes>
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private static bool s_RefreshCache;

        [SerializeField] private List<GameObject> m_PalettesCache;

        public static List<GameObject> palettes
        {
            get
            {
                if (instance.m_PalettesCache == null || s_RefreshCache)
                {
                    instance.RefreshPalettesCache();
                    s_RefreshCache = false;
                }

<<<<<<< HEAD
=======
=======
        private List<GameObject> m_PalettesCache;

        internal static Action palettesChanged;

        internal static List<GameObject> palettes
        {
            get
            {
                if (instance.m_PalettesCache == null
                    || (instance.m_PalettesCache.Count > 0 && instance.m_PalettesCache[0] == null))
                {
                    instance.RefreshPalettesCache();
                }
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
                return instance.m_PalettesCache;
            }
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        private void RefreshPalettesCache()
        {
            if (instance.m_PalettesCache == null)
                instance.m_PalettesCache = new List<GameObject>();
<<<<<<< HEAD
=======
=======
        private void OnEnable()
        {
            CleanCache();
        }

        private void OnDisable()
        {
            CleanCache();
        }

        private void RefreshPalettesCache()
        {
            if (m_PalettesCache == null)
                m_PalettesCache = new List<GameObject>();
            m_PalettesCache.Clear();
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b

            string[] guids = AssetDatabase.FindAssets("t:GridPalette");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GridPalette paletteAsset = AssetDatabase.LoadAssetAtPath(path, typeof(GridPalette)) as GridPalette;
                if (paletteAsset != null)
                {
                    string assetPath = AssetDatabase.GetAssetPath(paletteAsset);
                    GameObject palette = AssetDatabase.LoadMainAssetAtPath(assetPath) as GameObject;
                    if (palette != null)
                    {
                        m_PalettesCache.Add(palette);
                    }
                }
            }
            m_PalettesCache.Sort((x, y) => String.Compare(x.name, y.name, StringComparison.OrdinalIgnoreCase));
<<<<<<< HEAD
        }

        public class AssetProcessor : AssetPostprocessor
=======
<<<<<<< HEAD
        }

        public class AssetProcessor : AssetPostprocessor
=======

            palettesChanged?.Invoke();
        }

        private class AssetProcessor : AssetPostprocessor
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            public override int GetPostprocessOrder()
            {
                return 1;
            }

            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
            {
                if (!GridPaintingState.savingPalette)
                    CleanCache();
            }
        }

        internal static void CleanCache()
        {
            instance.m_PalettesCache = null;
        }
    }
}
