#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Verpha.HierarchyDesigner
{
    public static class HierarchyDesigner_Shared_EnumFilter
    {
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
#endif