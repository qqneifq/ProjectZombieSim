using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class QuestController : MonoBehaviour
{
    [SerializeField]
    QuestStage[] stages;

    public static event Action<string, string> OnQuestStart;

    public enum TutorialStage
    {
        camera = 0,
        movement = 1,
        shooting = 2,
        resource = 3,
        building = 4,
        completed = 5
    }
    bool isCompleted;
    [SerializeField]
    TutorialStage currentStage;
    void Start()
    {
        isCompleted = false;// load from save
        MouseMovementTrack.OnStageCompleted += StageCompleteHandler;
        StartTutorial();
        int i = (int)currentStage;
        OnQuestStart?.Invoke(stages[i].Name, stages[i].Text);
    }


    void StageCompleteHandler(TutorialStage stage)
    {
        MoveNextStage();
    }

    void Update()
    {

    }
    void StartTutorial()
    {
        Debug.Log("Start tutorial");
        currentStage = (TutorialStage)0;
        OpenStage(currentStage);
    }
    void MoveNextStage()
    {
        int i = (int)currentStage;
        
        i++;
        currentStage = (TutorialStage)i;
        OpenStage(currentStage);
    }
    void OpenStage(TutorialStage stage)
    {
        
        int i = (int)stage;
        Debug.Log($"Opening stage {i}/{stage}");
        OnQuestStart?.Invoke(stages[i].Name, stages[i].Text);
    }
}
