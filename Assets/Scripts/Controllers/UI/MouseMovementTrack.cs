using System;
using UnityEngine;
using static QuestController;

public class CameraQuest : BasicQuestModel
{
    [SerializeField]
    public Transform cameraTransform;
    public float threshold = 10f;
    [SerializeField]
    private bool lookedRight = false;
    [SerializeField]
    private bool lookedLeft = false;
    [SerializeField]
    private bool lookedUp = false;
    [SerializeField]
    private bool lookedDown = false;
    private Vector3 initialCameraRotation;

    public override void OnStart()
    {
        initialCameraRotation = cameraTransform.eulerAngles;
        base.OnStart();
    }


    void Update()
    {

        if (taskStarted)
        {
            Vector3 currentCameraRotation = cameraTransform.eulerAngles;

            // ???????????? ???? ????????
            float horizontalAngle = Mathf.DeltaAngle(initialCameraRotation.y, currentCameraRotation.y);
            float verticalAngle = Mathf.DeltaAngle(initialCameraRotation.x, currentCameraRotation.x);

            // ????????? ???????????
            if (horizontalAngle > threshold)
            {
                lookedRight = true;
            }
            if (horizontalAngle < -threshold)
            {
                lookedLeft = true;
            }
            if (verticalAngle > threshold)
            {
                lookedUp = true;
            }
            if (verticalAngle < -threshold)
            {
                lookedDown = true;
            }

            // ?????????, ??? ??? ??????????? ????????????? ? ?????? ?? ????? 10 ??????
            if (lookedRight && lookedLeft && lookedUp && lookedDown && (Time.time - taskStartTime >= requiredDuration))
            {
                Debug.Log("Player looked in all required directions for at least 10 seconds.");
                CompleteTask();
            }
        }
    }
}
