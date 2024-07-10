using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static QuestController;

public class BasicQuestModel : MonoBehaviour
{
    [SerializeField]
    public bool taskStarted = false;
    [SerializeField]
    public float requiredDuration = 6f;
    [SerializeField]
    public TutorialStage questStage;
    public float taskStartTime = 0f;

    public static event Action<TutorialStage> OnQuestCompleted;

    public void OnQuestStartHandler(TutorialStage stage)
    {
        if(questStage == stage)
        {
            taskStarted = true;
            taskStartTime = Time.time;
        }
    }

    public void CompleteTask()
    {
        taskStarted = false;
        OnQuestCompleted?.Invoke(questStage);
    }

    // Start is called before the first frame update
    public void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        Debug.Log($"Initializing {questStage}");
        QuestController.OnQuestStart += OnQuestStartHandler;
    }

}
