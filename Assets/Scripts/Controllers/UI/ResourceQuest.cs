using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceQuest : BasicQuestModel
{
    public override void OnStart()
    {
        questStage = QuestController.TutorialStage.resource;
        base.OnStart();
        ResourceHandler.OnResourceChange += ResourceGot;
    }
    int i = 0;
    void ResourceGot(int a, int b, int c)
    {
        i++;
        if(i == 10)
        {
            CompleteTask();
            ResourceHandler.OnResourceChange -= ResourceGot;
        }
    }
}
