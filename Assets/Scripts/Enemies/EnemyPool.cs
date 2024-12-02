using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private List<EnemiesPrefabsList> _enemyPrefabsList;
    private List<List<Enemy>> _pooledEnemies; 

    public int EnemyLevelCount => _enemyPrefabsList.Count;

    private void Awake()
    {
        _pooledEnemies = new List<List<Enemy>>();

        
        for (int i = 0; i < _enemyPrefabsList.Count; i++)
        {
            List<Enemy> enemiesForLevel = new List<Enemy>();
            
            for (int j = 0; j < 5; j++)
            {
                Enemy enemy = Instantiate(_enemyPrefabsList[i].Enemies[0]);
                enemy.GetComponent<EnemyHealth>().Died += ReturnEnemy;
                enemy.gameObject.SetActive(false);
                enemiesForLevel.Add(enemy);
            }
            _pooledEnemies.Add(enemiesForLevel);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _pooledEnemies.Count; i++)
        {
            foreach (Enemy enemy in _pooledEnemies[i])
            {
                if (enemy != null)
                {
                    enemy.transform.GetComponent<EnemyHealth>().Died -= ReturnEnemy;
                }
            }
        }
    }

    
    public void GetEnemy(int enemyLevel, SpawnPoint spawn)
    {
        
        foreach (Enemy enemy in _pooledEnemies[enemyLevel])
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);  
                enemy.transform.position = spawn.transform.position;

                return;  
            }
        }

        Enemy newEnemy = _enemyPrefabsList[enemyLevel].Enemies[Random.Range(0, _enemyPrefabsList[enemyLevel].Enemies.Count)];
        newEnemy.gameObject.SetActive(true);
        newEnemy.transform.position = spawn.transform.position;

        newEnemy.GetComponent<EnemyHealth>().Died += ReturnEnemy;

        _pooledEnemies[enemyLevel].Add(newEnemy);
    }

    public void ReturnEnemy(Enemy enemy)
    {
        Debug.Log(2);
        enemy.gameObject.SetActive(false);
    }
}

