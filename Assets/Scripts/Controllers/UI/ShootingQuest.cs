using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingQuest : BasicQuestModel
{ // ????????? ????????????????? ??????? ? ????????

    private bool shotFired = false;
    private bool buttonRPressed = false;
    private bool button1Pressed = false;
    private bool taskCompleted = false;
    public override void OnStart()
    {
        questStage = QuestController.TutorialStage.shooting;
        base.OnStart();
    }


    void Update()
    {

        // ???????? ??????? ?????????? ???????
        if (taskStarted && !taskCompleted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotFired = true;
                Debug.Log("Shot fired.");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                buttonRPressed = true;
                Debug.Log("Button R pressed.");
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                button1Pressed = true;
                Debug.Log("Button 1 pressed.");
            }

            // ????????, ??? ??? ???????? ????????? ? ?????? ?? ????? 10 ??????
            if (shotFired && buttonRPressed && button1Pressed && (Time.time - taskStartTime >= requiredDuration))
            {
                Debug.Log("Player completed the shooting quest.");
                CompleteTask();
            }
        }
    }
}
