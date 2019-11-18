using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpawner : MonoBehaviour
{

    public GameObject[] SpawnLocations;

    [Header("List of Stages that can occur")]
    public List<Stage> Stages;

    //Probably a cleaner way to do this
    public List<Stage> stageQueue;
    [SerializeField] private Stage currentStage;

    private GameObject player;
    private Player playerScript;
    private Vector2 playerPosition;
    private bool isPlayerDead = false;

    public CameraShake cameraShaker;

    // Start is called before the first frame update
    void Start()
    {


        stageQueue = new List<Stage>(Stages);
        currentStage = stageQueue[0];
        StartCoroutine(HandleStage(currentStage));

        cameraShaker = Camera.main.GetComponent<CameraShake>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        playerScript.deathEvent.AddListener(PlayerDeath);
        

        SpawnLocations = GameObject.FindGameObjectsWithTag("Ammo");

    }

    // Update is called once per frame
    void Update()
    { 
        if(enemiesSpawned.Contains(null as GameObject))
        {
            enemiesSpawned.Remove(null);
        }

        playerPosition = player.transform.position;
    }

    //List of enemies spawned in the current stage
    public List<GameObject> enemiesSpawned = new List<GameObject>();

    void HandleSpawns(Stage stage)
    {
        for (int i = 0; i < stage.EnemiesThatCanSpawn.Count; i++)
        {
            //GameObject _enemyObject = Instantiate(stage.EnemiesThatCanSpawn[i].gameObject, FindFarthestSpawner(), Quaternion.Euler(0, 0, 0)) as GameObject;
            InstantiateEnemy(stage.EnemiesThatCanSpawn[i].gameObject, FindFarthestSpawner());
            
        }
        AddListeners();
    }

    private void AddListeners()
    {
        for (int i = 0; i < enemiesSpawned.Count; i++)
        {
            enemiesSpawned[i].GetComponent<Enemy>().deathEvent.AddListener(OnDeath);
        }
        
    }

    private void ApplyModifiers(Enemy enemy)
    {
        enemy.speed *= currentStage.enemySpeedMultiplier;
    }

    void NextStage()
    {
        if(isPlayerDead == false)
        {
            StopAllCoroutines();
            stageQueue.Remove(currentStage);
            currentStage = stageQueue[0];
            StartCoroutine(HandleStage(currentStage));
        }
    }

    private IEnumerator HandleStage(Stage stage)
    {
        HandleSpawns(stage);
        
        yield return new WaitForSeconds(stage.durationTime);
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

    public void InstantiateEnemy(GameObject _enemy, Vector3 location)
    {
        ApplyModifiers(_enemy.GetComponent<Enemy>());
        GameObject __enemy = Instantiate(_enemy, location, Quaternion.Euler(0, 0, 0)) as GameObject;
        __enemy.GetComponent<Enemy>().deathEvent.AddListener(OnDeath);
        enemiesSpawned.Add(__enemy);
    }

    void OnDeath(GameObject _obj)
    {
        if(enemiesSpawned.Contains(_obj))
        {
            StartCoroutine(cameraShaker.Shake(0.25f, 0.1f));
            enemiesSpawned.Remove(_obj);
            _obj.GetComponent<Enemy>().deathEvent.RemoveAllListeners();
            
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

    }

    private Vector3 FindFarthestSpawner()
    {
        float farthestDistance = 0;
        float currentDistance = 0;
        Vector3 _spawner = Vector3.zero;
        for (int i = 0; i < SpawnLocations.Length; i++)
        {
            currentDistance = Mathf.Abs(Vector2.Distance(SpawnLocations[i].transform.position, playerPosition));
            if(currentDistance > farthestDistance)
            {
                farthestDistance = currentDistance;
                _spawner = SpawnLocations[i].transform.position;
            }
        }
        return _spawner;
    }

    void PlayerDeath()
    {
        Debug.Log("PlayerDeath Event");
        isPlayerDead = true;
        GameObject _enemy = null;
        StopAllCoroutines();
        for (int i = 0; i < enemiesSpawned.Count; i++)
        {
            _enemy = enemiesSpawned[i];
            enemiesSpawned.Remove(_enemy);
            Destroy(_enemy);
        }
    }
}