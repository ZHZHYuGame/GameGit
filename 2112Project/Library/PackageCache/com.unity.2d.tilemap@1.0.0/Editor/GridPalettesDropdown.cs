using System;

namespace UnityEditor.Tilemaps
{
    internal class GridPalettesDropdown : FlexibleMenu
    {
        public GridPalettesDropdown(IFlexibleMenuItemProvider itemProvider, int selectionIndex, FlexibleMenuModifyItemUI modifyItemUi, Action<int, object> itemClickedCallback, float minWidth)
            : base(itemProvider, selectionIndex, modifyItemUi, itemClickedCallback)
        {
            minTextWidth = minWidth;
        }

        internal class MenuItemProvider : IFlexibleMenuItemProvider
        {
            public int Count()
            {
<<<<<<< HEAD
                return GridPalettes.palettes.Count + 1;
=======
                return GridPaintingState.palettes.Count + 1;
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }

            public object GetItem(int index)
            {
<<<<<<< HEAD
                if (index < GridPalettes.palettes.Count)
                    return GridPalettes.palettes[index];
=======
                if (index < GridPaintingState.palettes.Count)
                    return GridPaintingState.palettes[index];
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611

                return null;
            }

            public int Add(object obj)
            {
                throw new NotImplementedException();
            }

            public void Replace(int index, object newPresetObject)
            {
                throw new NotImplementedException();
            }

            public void Remove(int index)
            {
                throw new NotImplementedException();
            }

            public object Create()
            {
                throw new NotImplementedException();
            }

            public void Move(int index, int destIndex, bool insertAfterDestIndex)
            {
                throw new NotImplementedException();
            }

            public string GetName(int index)
            {
<<<<<<< HEAD
                if (index < GridPalettes.palettes.Count)
                    return GridPalettes.palettes[index].name;
                else if (index == GridPalettes.palettes.Count)
=======
                if (index < GridPaintingState.palettes.Count)
                    return GridPaintingState.palettes[index].name;
                else if (index == GridPaintingState.palettes.Count)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
                    return "Create New Palette";
                else
                    return "";
            }

            public bool IsModificationAllowed(int index)
            {
                return false;
            }

            public int[] GetSeperatorIndices()
            {
<<<<<<< HEAD
                return new int[] { GridPalettes.palettes.Count - 1 };
=======
                return new int[] { GridPaintingState.palettes.Count - 1 };
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
            }
        }
    }
}
