using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[CreateAssetMenu (menuName = "Stages/Basic Stage")]
public class Stage : ScriptableObject
{
    [Header("How long the stage lasts")]
    public float durationTime;
    [Header("List of Enemies in this stage")]
    public List<GameObject> EnemiesThatCanSpawn = new List<GameObject>();
    [Header("Whether the stage is infinite, use for the last scene")]
    public bool infinite = false;
    [Header("Whether all enemies must be dead to start next stage")]
    public bool dependentStage = false;
    [Header("Control Speed of Enemies in this stage")]
    public float enemySpeedMultiplier = 1.0f;
}