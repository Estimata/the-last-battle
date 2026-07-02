using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private FighterSelector _fighterSelector;
    [SerializeField] private CinemachineCamera _explorationCamera;
    [SerializeField] private CinemachineCamera _battleCamera;
    [SerializeField] private CinemachineCamera _fighterDetailCamera;
    [SerializeField] private CinemachineOrbitalFollow _fighterOrbital;
    private bool _hasTarget;
    private float _rotatingSpeed = 5f;

    private void Awake() {
        _explorationCamera.Priority = 10;
    }

    private void Update() {
        if (_hasTarget) _fighterOrbital.HorizontalAxis.Value += _rotatingSpeed * Time.deltaTime;
    }

    private void BattleEntered()
    {
        _battleCamera.Priority = 50;
    }

    private void BattleExited()
    {
        _battleCamera.Priority = 0;
        _fighterDetailCamera.Priority = 0;
    }

    private void FocusOnFighter(FighterController fighter)
    {
        if (fighter == null)
        {
            _hasTarget = false;
            _fighterDetailCamera.Priority = 0;
            return;
        }
        
        _fighterOrbital.HorizontalAxis.Value = 0;
        _fighterDetailCamera.Target.TrackingTarget = fighter.transform;
        _fighterDetailCamera.Target.LookAtTarget = fighter.transform;
        _hasTarget = true;
        _fighterDetailCamera.Priority = 100;
    }

    private void OnEnable() {
        BattleInitiator.OnBattleInitiated += BattleEntered;
        BattleController.OnBattleEnded += BattleExited;
        _fighterSelector.OnFighterSelected += FocusOnFighter;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleInitiated -= BattleEntered;
        BattleController.OnBattleEnded -= BattleExited;
        _fighterSelector.OnFighterSelected -= FocusOnFighter;
    }
}
