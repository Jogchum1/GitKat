using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CamControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera based on the pan direction in the inspector
                CamManager.instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Vector2 exitDirection = (collision.transform.position - collider.bounds.center).normalized;

            if (customInspectorObjects.swapCameras && customInspectorObjects.cameraOnLeft != null && customInspectorObjects.cameraOnRight != null)
            {
                CamManager.instance.CameraCheckAndSwap(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, exitDirection);
            }

            if (customInspectorObjects.panCameraOnContact)
            {
                CamManager.instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    public CinemachineVirtualCamera cameraOnLeft;
    public CinemachineVirtualCamera cameraOnRight;

    public PanDirection panDirection;
    public float panDistance = 3f;
    public float panTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

//[CustomEditor(typeof(CamControlTrigger))]
//public class MyScriptEditor : Editor
//{
//    CamControlTrigger camControlTrigger;

//    private void OnEnable()
//    {
//        camControlTrigger = (CamControlTrigger)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        if (camControlTrigger.customInspectorObjects.swapCameras)
//        {
//            camControlTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left", camControlTrigger.customInspectorObjects.cameraOnLeft,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

//            camControlTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right", camControlTrigger.customInspectorObjects.cameraOnRight,
//                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
//        }

//        if (camControlTrigger.customInspectorObjects.panCameraOnContact)
//        {
//            camControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
//                camControlTrigger.customInspectorObjects.panDirection);

//            camControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", camControlTrigger.customInspectorObjects.panDistance);
//            camControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", camControlTrigger.customInspectorObjects.panTime);
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(camControlTrigger);
//        }
//    }
//}