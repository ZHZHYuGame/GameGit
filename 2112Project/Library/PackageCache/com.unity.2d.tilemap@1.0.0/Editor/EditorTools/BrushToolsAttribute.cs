using System;
using System.Collections.Generic;

namespace UnityEditor.Tilemaps
{
<<<<<<< HEAD
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
=======
    /// <summary>
    /// An attribute for GridBrushBase which specifies the TilemapEditorTool types which can work with the GridBrushBase.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
    public class BrushToolsAttribute : Attribute
    {
        private List<Type> m_ToolTypes;
        internal List<Type> toolList
        {
            get { return m_ToolTypes; }
        }

<<<<<<< HEAD
=======
        /// <summary>
        /// Constructor for BrushToolsAttribute. Specify the TilemapEditorTool types which can work with the GridBrushBase.
        /// </summary>
        /// <param name="tools">An array of TilemapEditorTool types which can work with the GridBrushBase.</param>
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
        public BrushToolsAttribute(params Type[] tools)
        {
            m_ToolTypes = new List<Type>();
            foreach (var toolType in tools)
            {
                if (toolType.IsSubclassOf(typeof(TilemapEditorTool)) && !m_ToolTypes.Contains(toolType))
                {
                    m_ToolTypes.Add(toolType);
                }
            }
        }
    }
}
