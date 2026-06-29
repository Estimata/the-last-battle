using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public BattleUIController BattleUI;
    [SerializeField] private CinemachineTargetGroup _fighterGroup;
    [SerializeField] private BattleNavigation _battleNavigation;
    [SerializeField] private FighterTurn _fighterTurn;
    [SerializeField] private FighterManager _fighterManager;
    [SerializeField] private PlayerAction _playerAction;
    
    private StateMachine<BattleController> _battleState;
    public PrepareTurn PrepareTurnState { get; private set; }
    public PlayerTurn PlayerTurnState { get; private set; }
    public EnemyTurn EnemyTurnState { get; private set; }

    private List<FighterController> _remainingFighter;

    private void Awake() {
        InitializeStates();
    }

    private void InitializeStates()
    {
        PrepareTurnState = new PrepareTurn();
        PlayerTurnState = new PlayerTurn();
        EnemyTurnState = new EnemyTurn();

        _battleState = new StateMachine<BattleController>(this, PrepareTurnState, false);
    }

    private void Update() {
        _battleState.Update();
    }

    public void ChangeState(IState<BattleController> state) => _battleState.ChangeState(state);
    public void InterruptState(IState<BattleController> state) => _battleState.Interrupt(state);
    public List<FighterController> CreateQueue() => _fighterTurn.CreateQueue(_remainingFighter);
    public FighterController GetFighterTurn() => _fighterTurn.FighterQueue[0];
    public FighterController GetTurnAndAdvance() => _fighterTurn.GetTurnAndAdvance();

    public void SetPlayerAction()
    {
        FighterController player = GetFighterTurn();
        _playerAction.SetAction(player);
        BattleUI.RegisterBasicActionButton(player.GetBasicAction());
    }



    public void BattleEntered(BattleContext context)
    {
        Cursor.lockState = CursorLockMode.None;

        _remainingFighter = new List<FighterController>(context.Fighters);

        _battleNavigation.SetupNavigation(
            _remainingFighter,
            context.BattleCenter
        );

        _fighterManager.SetFighterSides(_remainingFighter);

        foreach (FighterController fighter in _remainingFighter)
        {
            Vector3 initialPosition = _battleNavigation.FindInitialPosition(fighter);
            _fighterGroup.AddMember(fighter.transform, 1f, 1f);

            fighter.InitiatePosition(initialPosition);
            fighter.SetTarget(_fighterManager.FindNearestTarget(fighter));
        }

        
        BattleUI.ShowFighterQueue();

        _battleState.Enable();
        InterruptState(PrepareTurnState);
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
