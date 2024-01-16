using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;

    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;

    private Coroutine panCamCoroutine;

    [HideInInspector]
    public CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private Vector2 startingTrackedObjectOffset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].enabled)
            {
                //set the current active camera
                currentCamera = allVirtualCameras[i];

                //set the framing transposer
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
    }

    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        panCamCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        //handle pan from trigger
        if (!panToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
                default:
                    break;
            }

            endPos *= panDistance;

            startingPos = startingTrackedObjectOffset;

            endPos += startingPos;
        }

        //handle the pan back to starting position
        else
        {
            startingPos = framingTransposer.m_TrackedObjectOffset;
            endPos = startingTrackedObjectOffset;
        }

        //handle the actual panning of the camera
        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }
    }

    #region Swap Cameras

    //checks the exit direction and swaps to the corresponding camera
    public void CameraCheckAndSwap(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, float triggerExitDirection)
    {
        //if the current camera is the camera on the left and our trigger exit direction was on the right
        if (triggerExitDirection > 0f)
        {
            CameraSwap(cameraFromRight);
        }

        //if the current camera is the camera on the right and our trigger exit direction was on the left
        else if (triggerExitDirection < 0f)
        {
            CameraSwap(cameraFromLeft);
        }
    }

    private void CameraSwap(CinemachineVirtualCamera newCam)
    {
        //newCam.enabled = true;
        currentCamera.enabled = false;
        currentCamera = newCam;
        currentCamera.enabled = true;

        framingTransposer = newCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    #endregion
}
