using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 51)]
public class QuestStage : ScriptableObject
{
    [SerializeField]
    QuestController.TutorialStage stage;
    [SerializeField]
    string name;
    [SerializeField]
    string text;
    [SerializeField]
    bool isCompleted;
    
    public string Name { get { return name; } }
    public string Text { get { return text; } }


}
