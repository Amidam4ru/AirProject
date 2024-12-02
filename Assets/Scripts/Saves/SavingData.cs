using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingData : MonoBehaviour
{
    [SerializeField] private TurretShooter _turretShooter; // взять скорострельность
    [SerializeField] private PlayerMover _playerMover; // взять скорость
    [SerializeField] private Health _playerHealth; // взять здоровье
    [SerializeField] private PlayerScore _playerScore; // взять очки игрока
    [SerializeField] private PlayerMoney _playerMoney; // взять монеты игрока
    [SerializeField] private TerritoryChanging _territoryChanging; // взять уровень территории

    private void Save()
    {
        
    }
}
