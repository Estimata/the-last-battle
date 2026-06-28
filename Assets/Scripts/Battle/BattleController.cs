using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    [SerializeField] private BattleUIController _battleUIController;
    [SerializeField] private CinemachineTargetGroup _fighterGroup;
    [SerializeField] private BattleNavigation _battleNavigation;
    [SerializeField] private FighterTurn _fighterTurn;
    
    private StateMachine<BattleController> _battleState;
    public PrepareTurn PrepareTurnState { get; private set; }

    private List<FighterController> _remainingFighter;

    private void Awake() {
        PrepareTurnState = new PrepareTurn();

        _battleState = new StateMachine<BattleController>(this, PrepareTurnState, false);
    }

    private void Update() {
        _battleState.Update();
    }

    public void ChangeState(IState<BattleController> state) => _battleState.ChangeState(state);
    public void InterruptState(IState<BattleController> state) => _battleState.Interrupt(state);
    public void CreateQueue() => _fighterTurn.CreateQueue(_remainingFighter);



    public void BattleEntered(BattleContext context)
    {
        _remainingFighter = new List<FighterController>(context.Fighters);

        _battleNavigation.SetupNavigation(
            _remainingFighter,
            context.BattleCenter
        );

        foreach (FighterController fighter in _remainingFighter)
        {
            Vector3 initialPosition = _battleNavigation.FindInitialPosition(fighter);
            _fighterGroup.AddMember(fighter.transform, 1f, 1f);

            fighter.InitiatePosition(initialPosition);
        }

        BattleStart();
    }

    public void BattleStart()
    {   
        InterruptState(PrepareTurnState);
        _battleState.Enable();
    }

    public void BattleExited()
    {
        _battleState.Disable();
    }
 
    private void OnEnable() {
        BattleInitiator.OnBattleReady += BattleEntered;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleReady -= BattleEntered;
    }
}
