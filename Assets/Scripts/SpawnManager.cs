using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _asteroidPrefab;
        
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerUpArray;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
        StartCoroutine(AsteroidSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameObject.Find("EnemyContainer") == null)
        //{
        //    _enemyContainer = new GameObject("EnemyContainer");
        //}
    }

    private IEnumerator EnemySpawn()
    {
        Debug.Log("EnemySpawn Started");

        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.5f, 9.5f), 7.5f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator PowerUpSpawn()
    {
        while (_stopSpawning == false)
        {
            Instantiate(_powerUpArray[Random.Range(0, 3)], new Vector3(Random.Range(-9.3f, 9.3f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 15));
        }
    }

    private IEnumerator AsteroidSpawn()
    {
        Debug.Log("AsteroidSpawn Started");

        while (_stopSpawning == false)
        {
            GameObject newAsteroid = Instantiate(_asteroidPrefab, new Vector3(Random.Range(-9.3f, 9.3f), Random.Range(-4.8f, 4.8f), 0), Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(15f);
        }
    }

    public void PlayerDead()
    {
        
        _stopSpawning = true;
        Debug.Log("All SpawnTimers Stopped");

        Destroy(_enemyContainer.gameObject);

        //GameObject[] enemyArray;
        //enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        //foreach (GameObject enemy in enemyArray)
        //{
        //    Destroy(enemy);
        //}

    }
}