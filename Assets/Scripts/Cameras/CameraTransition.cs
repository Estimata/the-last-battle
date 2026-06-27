using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _explorationCamera;
    [SerializeField] private CinemachineCamera _battleCamera;

    private void BattleEntered()
    {
        _battleCamera.Priority = 100;
    }

    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleInitiated -= BattleEntered;
    }
}
