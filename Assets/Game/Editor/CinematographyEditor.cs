using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cinematopgraphy))]
[CanEditMultipleObjects]
public class CinematographyEditor : Editor {

    SerializedProperty cameraDolly;
    SerializedProperty cameraDollySpeed;

    private void OnEnable()
    {
        cameraDolly = serializedObject.FindProperty("_cameraDolly");
        cameraDollySpeed = serializedObject.FindProperty("_cameraDollySpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //base.OnInspectorGUI();

        Cinematopgraphy cinematography = (Cinematopgraphy)target;

        GUILayout.Space(10);
        GUILayout.Label("Camera Actions");
        GUILayout.Space(10);

        //cinematography._cameraDolly = EditorGUILayout.Toggle("Camera Dolly", cinematography._cameraDolly);
        EditorGUILayout.PropertyField(cameraDolly);
        if (cinematography._cameraDolly)
        {
            //cinematography._cameraDollySpeed = EditorGUILayout.FloatField("Camera Dolly Speed", cinematography._cameraDollySpeed);
            EditorGUILayout.PropertyField(cameraDollySpeed);

        }

        GUILayout.Space(10);


        cinematography._cameraPan = EditorGUILayout.Toggle("Camera Pan", cinematography._cameraPan);
        if (cinematography._cameraPan)
        {
            cinematography._cameraPanSpeed = EditorGUILayout.FloatField("Camera Pan Speed", cinematography._cameraPanSpeed);
        }

        GUILayout.Space(10);

        cinematography._cameraPedestal = EditorGUILayout.Toggle("Camera Pedestal", cinematography._cameraPedestal);
        if (cinematography._cameraPedestal)
        {
            cinematography._cameraPedestalSpeed = EditorGUILayout.FloatField("Camera Pedestal Speed", cinematography._cameraPedestalSpeed);
        }

        GUILayout.Space(10);

        cinematography._cameraRotate = EditorGUILayout.Toggle("Camera Rotate", cinematography._cameraRotate);
        if (cinematography._cameraRotate)
        {
            cinematography._cameraRotateSpeed = EditorGUILayout.FloatField("Camera Rotate Speed", cinematography._cameraRotateSpeed);
        }

        GUILayout.Space(10);

        cinematography._cameraTilt = EditorGUILayout.Toggle("Camera Tilt", cinematography._cameraTilt);
        if (cinematography._cameraTilt)
        {
            cinematography._cameraTiltSpeed = EditorGUILayout.FloatField("Camera Tilt Speed", cinematography._cameraTiltSpeed);
        }

        GUILayout.Space(10);

        cinematography._cameraTrack = EditorGUILayout.Toggle("Camera Track", cinematography._cameraTrack);
        if (cinematography._cameraTrack)
        {
            cinematography._cameraTrackSpeed = EditorGUILayout.FloatField("Camera Dolly Speed", cinematography._cameraTrackSpeed);
        }

        serializedObject.ApplyModifiedProperties();

    }
}
