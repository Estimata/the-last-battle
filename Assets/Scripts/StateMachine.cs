using UnityEngine;

public class StateMachine<T>
{
    public event System.Action<IState<T>> OnStateChanged;
    private IState<T> _currentState;
    private readonly T _context;

    public StateMachine(T context, IState<T> initialState)
    {
        _context = context;
        
        initialState.Enter(context);
        _currentState = initialState;
    }

    public void ChangeState(IState<T> newState)
    {
        _currentState.Exit(_context);
        newState.Enter(_context);
        _currentState = newState;

        OnStateChanged?.Invoke(newState);
    }

    public void Update()
    {
        _currentState.UpdateState(_context);
    }

    public void Interrupt(IState<T> newState)
    {
        if (_currentState.CanBeInterrupted(newState)) ChangeState(newState);
    }
}