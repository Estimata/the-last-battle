using UnityEngine;

public class FighterController : MonoBehaviour
{
    private StateMachine<FighterController> _fighterState;
    public FighterStandby StandbyState { get; private set; }

    private void Awake() {
        StandbyState = new FighterStandby();

        _fighterState = new StateMachine<FighterController>(this, StandbyState);
    }
}
