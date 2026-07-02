using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public static event Action OnBattleEnded;
    public BattleUIController BattleUI;
    public FighterSelector FighterSelector;
    [SerializeField] private CinemachineTargetGroup _fighterGroup;
    [SerializeField] private BattleNavigation _battleNavigation;
    [SerializeField] private FighterTurn _fighterTurn;
    [SerializeField] private FighterManager _fighterManager;
    [SerializeField] private PlayerAction _playerAction;
    
    private StateMachine<BattleController> _battleState;
    public PrepareTurn PrepareTurnState { get; private set; }
    public PlayerTurn PlayerTurnState { get; private set; }
    public EnemyTurn EnemyTurnState { get; private set; }
    public EndTurn EndTurnState { get; private set; }

    public BattleContext BattleContext { get; private set; }
    private void Awake() {
        InitializeStates();
    }

    private void InitializeStates()
    {
        PrepareTurnState = new PrepareTurn();
        PlayerTurnState = new PlayerTurn();
        EnemyTurnState = new EnemyTurn();
        EndTurnState = new EndTurn();

        _battleState = new StateMachine<BattleController>(this, PrepareTurnState, false);
    }

    private void Update() {
        _battleState.Update();
    }

    public void ChangeState(IState<BattleController> state) => _battleState.ChangeState(state);
    public void InterruptState(IState<BattleController> state) => _battleState.Interrupt(state);

    public List<FighterController> CreateQueue() => _fighterTurn.CreateQueue(_fighterManager.RemainingFighter);
    public FighterController GetFighterTurn() => _fighterTurn.FighterQueue[0];
    public async Task<FighterController> GetTurnAndAdvance() 
    {
        await BattleUI.RemoveFighterFromQueue(GetFighterTurn());
        return _fighterTurn.GetTurnAndAdvance();
    }

    public FighterController GetSelectedFighter() => FighterSelector.SelectedFighter;
    public void SelectFighter(FighterController fighter) => FighterSelector.SetFighter(fighter);
    public void ShowActionMenu() => BattleUI.ShowActionMenu();

    public void SetPlayerAction()
    {
        FighterController player = GetFighterTurn();
        _playerAction.SetAction(player);
        
        FighterAction basicAction = player.GetBasicAction();
        Button basicActionButton = BattleUI.RegisterBasicActionButton(basicAction);
        basicActionButton.onClick.AddListener(() => _playerAction.SelectedAction(basicAction));

        BattleUI.RemoveSkillActionButtons();
        foreach (FighterAction skill in player.GetSkills())
        {
            Button skillActionButton = BattleUI.RegisterSkillActionButton(skill);
            skillActionButton.onClick.AddListener(() => _playerAction.SelectedAction(skill));
        }
    }

    public void CancelAction()
    {
        _playerAction.SelectedAction(null);
        BattleUI.UnselectAllButtons();
    }
    public bool IsActionSelected() => _playerAction.GetSelectedAction() != null;
    public async Task ExecuteAction(FighterController target)
    {
        FighterController user = GetFighterTurn();
        await _playerAction.ExecuteAction(user, target, this, BattleContext);
    }

    public FighterController FindNearestTarget(FighterController fighter) => _fighterManager.FindNearestTarget(fighter);

    public FighterController GetDiedFighter() => _fighterManager.GetDiedFighter();
    public async Task RemoveFighter(FighterController fighter)
    {
        _fighterManager.RemainingFighter.Remove(fighter);
        _fighterManager.SetFighterSides();
        _fighterTurn.FighterQueue.Remove(fighter);
        await BattleUI.RemoveFighterFromQueue(fighter);

        if (IsBattleOver()) {
            BattleExited();
            return;
        }
        foreach (FighterController finder in _fighterManager.RemainingFighter) finder.SetTarget(FindNearestTarget(finder).transform);
    }

    public bool IsBattleOver() => _fighterManager.IsBattleOver(_fighterManager.RemainingFighter);

    public void BattleEntered(BattleContext context)
    {
        BattleContext = context;
        _fighterManager.RemainingFighter = new List<FighterController>(context.Fighters);

        _battleNavigation.SetupNavigation(
            _fighterManager.RemainingFighter,
            context.BattleCenter
        );

        _fighterManager.SetFighterSides();

        foreach (FighterController fighter in _fighterManager.RemainingFighter)
        {
            Vector3 initialPosition = _battleNavigation.FindInitialPosition(fighter);
            _fighterGroup.AddMember(fighter.transform, 1f, 1f);

            fighter.InitiatePosition(initialPosition);
            fighter.SetTarget(FindNearestTarget(fighter).transform);
        }

        
        BattleUI.ShowFighterQueue();

        _battleState.Enable();
        InterruptState(PrepareTurnState);
    }

    public void BattleExited()
    {
        _battleState.Disable();
        OnBattleEnded?.Invoke();
    }
 
    private void OnEnable() {
        BattleInitiator.OnBattleReady += BattleEntered;
    }

    private void OnDisable() {
        BattleInitiator.OnBattleReady -= BattleEntered;
    }
}
