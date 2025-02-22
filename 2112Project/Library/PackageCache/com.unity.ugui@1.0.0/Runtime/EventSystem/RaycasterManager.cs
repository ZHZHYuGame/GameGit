using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
    internal static class RaycasterManager
    {
        private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();

        public static void AddRaycaster(BaseRaycaster baseRaycaster)
<<<<<<< HEAD
=======
=======
    public static class RaycasterManager
    {
        private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();

        internal static void AddRaycaster(BaseRaycaster baseRaycaster)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (s_Raycasters.Contains(baseRaycaster))
                return;

            s_Raycasters.Add(baseRaycaster);
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        /// <summary>
        /// List of BaseRaycasters that has been registered.
        /// </summary>
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        public static List<BaseRaycaster> GetRaycasters()
        {
            return s_Raycasters;
        }

<<<<<<< HEAD
        public static void RemoveRaycasters(BaseRaycaster baseRaycaster)
=======
<<<<<<< HEAD
        public static void RemoveRaycasters(BaseRaycaster baseRaycaster)
=======
        internal static void RemoveRaycasters(BaseRaycaster baseRaycaster)
>>>>>>> 5efc6cefed85800961bebdf3974ec322da11a611
>>>>>>> 9ad7118b7bb183b686754ae747ab8afd5cd5ca9b
        {
            if (!s_Raycasters.Contains(baseRaycaster))
                return;
            s_Raycasters.Remove(baseRaycaster);
        }
    }
}
