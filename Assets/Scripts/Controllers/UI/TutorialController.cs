using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    List<Canvas> stages;
    enum tutorialStage
    {
        movement = 0,
        camera = 1,
        shooting = 2,
        resource = 3,
        building = 4,
        completed = 5
    }
    bool isCompleted;
    tutorialStage currentStage;
    void Start()
    {
        isCompleted = false;// load from save
        if(!isCompleted)
        {
            StartTutorial();
        }
    }

    void Update()
    {
        if(CheckRequirements() & !isCompleted)
        {
            MoveNextStage();
        }
    }
    void StartTutorial()
    {
        currentStage = tutorialStage.movement;
        OpenStage(currentStage);
    }
    void MoveNextStage()
    {
        int i = (int)currentStage;
        if (i == 5)
        {
            isCompleted = !isCompleted;
        }
        else
        {
            CloseStage(currentStage);
            i++;
            currentStage = (tutorialStage)i;
            OpenStage(currentStage);
        }
    }
    void OpenStage(tutorialStage stage)
    {

    }
    void CloseStage(tutorialStage stage)
    {

    }
    bool CheckRequirements()
    {
        return true;
    }
}
