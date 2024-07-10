using System;
using UnityEngine;
using static QuestController;


public class MovementQuest : BasicQuestModel
{
    [SerializeField]
    private bool movedUp = false;
    [SerializeField]
    private bool movedDown = false;
    [SerializeField]
    private bool movedLeft = false;
    [SerializeField]
    private bool movedRight = false;
    [SerializeField]
    private bool jumped = false;

    public override void OnStart()
    {
        base.OnStart();
        questStage = TutorialStage.movement;
    }

    private void Update()
    {
        if (taskStarted)
        {
            bool isMoving = false;

            if (Input.GetKey(KeyCode.W))
            {
                movedUp = true;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movedDown = true;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movedLeft = true;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movedRight = true;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                isMoving = true;
            }

            if (isMoving && !taskStarted)
            {
                taskStarted = true;
                taskStartTime = Time.time;
            }

            if (taskStarted && movedUp && movedDown && movedLeft && movedRight && jumped && (Time.time - taskStartTime >= requiredDuration))
            {
                Debug.Log("Player moved in all four directions and jumped within at least 10 seconds.");
                Debug.Log($"Quest {questStage}");
                CompleteTask();
            }
        }
    }
}

