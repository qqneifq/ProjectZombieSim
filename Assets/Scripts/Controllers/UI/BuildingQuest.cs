using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingQuest : BasicQuestModel
{
    public override void OnStart()
    {
        questStage = QuestController.TutorialStage.building;
        base.OnStart();
        Builder.OnBuildingBuilt += BuildingBuilt;
    }

    public void BuildingBuilt()
    {
        CompleteTask();
        Builder.OnBuildingBuilt -= BuildingBuilt;
    }
}
