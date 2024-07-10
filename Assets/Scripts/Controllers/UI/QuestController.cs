using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class QuestController : MonoBehaviour
{
    [SerializeField]
    QuestStage[] stages;
    [SerializeField]
    bool tutorialStarted;
    public static event Action<string, string> OnQuestShow;
    public static event Action<TutorialStage> OnQuestStart;
    public enum TutorialStage
    {
        camera = 0,
        movement = 1,
        shooting = 2,
        resource = 3,
        building = 4,
        completed = 5
    }
    [SerializeField]
    bool isCompleted;
    [SerializeField]
    TutorialStage currentStage = TutorialStage.camera;
    void Start()
    {
        isCompleted = false;// load from save
        CameraQuest.OnQuestCompleted += OnQuestCompletedHandler;
        MovementQuest.OnQuestCompleted += OnQuestCompletedHandler;
    }
    private void Update()
    {
        if(!tutorialStarted)
        {
            if (!isCompleted)
            {
                tutorialStarted = true;
                int i = (int)currentStage;

                StartTutorial();
                //OnQuestShow?.Invoke(stages[i].Name, stages[i].Text);
            }
        }
    }

    void OnQuestCompletedHandler(TutorialStage stage)
    {
        if (stage == currentStage)
        {
            Debug.Log($"Completed stage {stage}");
            MoveNextStage();
        }
    }

    void StartTutorial()
    {
        Debug.Log("Start tutorial");
        currentStage = (TutorialStage)0;
        OpenStage(currentStage);
    }
    void MoveNextStage()
    {
        Debug.Log("Moving next stage");
        int i = (int)currentStage;
        i++;
        currentStage = (TutorialStage)i;
        OpenStage(currentStage);
    }
    void OpenStage(TutorialStage stage)
    {
        OnQuestStart?.Invoke(currentStage);
        int i = (int)stage;
        Debug.Log($"Starting stage {i}/{stage}");
        OnQuestShow?.Invoke(stages[i].Name, stages[i].Text);
    }
}
