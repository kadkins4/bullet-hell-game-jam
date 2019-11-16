using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour
{

    [Header("List of Stages that can occur")]
    public List<Stage> Stages;

    //Probably a cleaner way to do this
    public List<Stage> stageQueue;
    [SerializeField] private Stage currentStage;

    // Start is called before the first frame update
    void Start()
    {
        stageQueue = Stages;
        currentStage = stageQueue[0];
        StartCoroutine(HandleStage(currentStage));

    }

    // Update is called once per frame
    void Update()
    { 
        if(enemiesSpawned.Contains(null as GameObject))
        {
            Debug.Log("true");
            enemiesSpawned.Remove(null);
        }
    }

    //List of enemies spawned in the current stage
    public List<GameObject> enemiesSpawned = new List<GameObject>();

    void HandleSpawns(Stage stage)
    {
        for (int i = 0; i < stage.EnemiesThatCanSpawn.Count; i++)
        {
            GameObject _enemyObject = Instantiate(stage.EnemiesThatCanSpawn[i].gameObject, stage.spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
            ApplyModifiers(_enemyObject.GetComponent<Enemy>());
            enemiesSpawned.Add(_enemyObject);
            
        }
        AddListeners();
    }

    private void AddListeners()
    {
        for (int i = 0; i < enemiesSpawned.Count; i++)
        {
            enemiesSpawned[i].GetComponent<BasicEnemy>().deathEvent.AddListener(OnDeath);
        }
        
    }

    private void ApplyModifiers(Enemy enemy)
    {
        enemy.speed *= currentStage.enemySpeedMultiplier;
    }

    void NextStage()
    {
        Debug.Log("Begin Next Stage!");
        StopAllCoroutines();
        stageQueue.Remove(currentStage);
        currentStage = stageQueue[0];
        StartCoroutine(HandleStage(currentStage));
    }

    private IEnumerator HandleStage(Stage stage)
    {
        HandleSpawns(stage);
        
        yield return new WaitForSeconds(stage.durationTime);
        Debug.Log("Stage Timer Complete!");
        //If the stage does not require everyone to be dead
        if(stage.dependentStage == false)
        {
            if(currentStage.infinite == false)
            {
                //If not infinite and not depedent start next stage
                NextStage();
                yield break;
            }
            else
            {
                //If stage is infinite it will loop forever
                StartCoroutine(HandleStage(currentStage));
                yield break;
            }
        }
        /*else
        {
            //If the stage does require everyone to be dead, check if they are actually dead
            if(enemiesSpawned.Count == 0)
            {
                //If they are, start next stage
                stageQueue.Remove(stage);
                currentStage = stageQueue[0];
                StartCoroutine(HandleStage(currentStage));
                yield break;
            }
            else
            {
                //If the stage does not have everyone killed
                //No clue what to write here yet
            }
        }*/
    }

    void OnDeath(GameObject _obj)
    {
        if(enemiesSpawned.Contains(_obj))
        {
            enemiesSpawned.Remove(_obj);
            _obj.GetComponent<BasicEnemy>().deathEvent.RemoveAllListeners();
            Destroy(_obj);
        }

        //Tidy up later for the infinite stage
        if(enemiesSpawned.Count == 0 && currentStage.infinite == false)
        {
            NextStage();
        }
    }

    private void OnDisable()
    {
        enemiesSpawned.Clear();
    }

}
