// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Core.Events
{
    [CustomEditor(typeof(Event), true)]
    [CanEditMultipleObjects()]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.ObjectField("Asset", target, typeof(Event), false);
            }

            base.OnInspectorGUI();

            using (new EditorGUI.DisabledScope(!Application.isPlaying))
            {
                Event e = target as Event;
                if (GUILayout.Button("Raise"))
                {
                    e.Raise();
                }

                GUILayout.Label(string.Format("{0} delegate handler(s) registered", e.ActiveListenerCount));
            }
        }
    }
}