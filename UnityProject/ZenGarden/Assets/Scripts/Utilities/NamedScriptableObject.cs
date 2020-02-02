using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class NamedScriptableObject : ScriptableObject
    {
        public override string ToString()
        {
            SerializedObject sObj = new SerializedObject(this);
            return sObj.FindProperty("m_Name").stringValue;
        }
    }
}