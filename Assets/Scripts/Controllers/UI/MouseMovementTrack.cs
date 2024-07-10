using System;
using UnityEngine;

public class MouseMovementTrack : MonoBehaviour
{
    public Transform cameraTransform;
    public float threshold = 10f; // ??????????? ????????? ???? ??? ???????????? ????????
    public float requiredDuration = 4f; // ????????? ????????????????? ??????? ? ????????
    bool isCompleted;
    [SerializeField]
    private bool lookedUp = false;
    [SerializeField]
    private bool lookedDown = false;
    [SerializeField]
    private bool lookedLeft = false;
    [SerializeField]
    private bool lookedRight = false;
    private bool taskStarted = false;
    private float taskStartTime = 0f;

    private Vector3 initialCameraRotation;

    void Start()
    {
        initialCameraRotation = cameraTransform.eulerAngles;
    }

    void Update()
    {
        if (!isCompleted)
        {
            Vector3 currentCameraRotation = cameraTransform.eulerAngles;
            Vector3 deltaRotation = currentCameraRotation - initialCameraRotation;

            if (!taskStarted)
            {
                if (deltaRotation.magnitude > threshold)
                {
                    taskStarted = true;
                    taskStartTime = Time.time;
                }
            }

            if (taskStarted)
            {
                if (deltaRotation.x > threshold)
                {
                    lookedUp = true;
                }
                if (deltaRotation.x < -threshold)
                {
                    lookedDown = true;
                }
                if (deltaRotation.y < -threshold)
                {
                    lookedLeft = true;
                }
                if (deltaRotation.y > threshold)
                {
                    lookedRight = true;
                }

                if (lookedUp && lookedDown && lookedLeft && lookedRight && Time.time - taskStartTime >= requiredDuration)
                {
                    Debug.Log("Player looked in all four directions for at least 4 seconds.");
                    CompleteTask();
                }

                initialCameraRotation = currentCameraRotation;
            }
        }
    }
    public static event Action<QuestController.TutorialStage> OnStageCompleted;
    private void CompleteTask()
    {
        isCompleted = true;
        OnStageCompleted?.Invoke(QuestController.TutorialStage.camera);
    }
}
