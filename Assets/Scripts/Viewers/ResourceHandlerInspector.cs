#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceHandler))]
public class ResourceHandlerInspector : Editor
{
    private enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }

    private ResourceHandler _classHolder;
    private SerializedObject _targetObject;
    private SerializedProperty _resourcesArray;
    private SerializedProperty _maxResourcesArray;


    void OnEnable()
    {

        _classHolder = (ResourceHandler)target;
        _targetObject = new SerializedObject(_classHolder);
        _resourcesArray = _targetObject.FindProperty("_resources");
        _maxResourcesArray = _targetObject.FindProperty("_maxResources");
    }

    public override void OnInspectorGUI()
    {
        _targetObject.Update();
        EditorGUILayout.LabelField("Player Initial Resources");

        for (int i = 0; i < ResourceTypes.Count(); i++)
        {
            if (_resourcesArray.arraySize <= i)
            {
                _classHolder.PlusResources();
            }

            SerializedProperty resourceObject = _resourcesArray.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(resourceObject, new GUIContent(ResourceTypes.Name(i)));
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player Initial Max Resources");

        for (int i = 0; i < ResourceTypes.Count(); i++)
        {
            if (_maxResourcesArray.arraySize <= i)
            {
                _classHolder.PlusMaxResources();
            }

            SerializedProperty resourceObject = _maxResourcesArray.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(resourceObject, new GUIContent(ResourceTypes.Name(i)));
        }

        _targetObject.ApplyModifiedProperties();
    }
}
#endif
