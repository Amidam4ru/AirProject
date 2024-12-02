using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsCircle : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    public List<SpawnPoint> GetSpawnPoints()
    {
        return new List<SpawnPoint>(_spawnPoints);
    }
}
