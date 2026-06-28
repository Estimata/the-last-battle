using UnityEngine;
using System;

public class StateMachine<T>
{
    public event Action<IState<T>> OnStateChanged;
    private IState<T> _initialState;
    private IState<T> _currentState;
    private readonly T _context;

    public StateMachine(T context, IState<T> initialState, bool enabled = true)
    {
        _context = context;
        _initialState = initialState;
        
        if (enabled) Enable();
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
        _currentState?.Update(_context);
    }

    public void Interrupt(IState<T> newState)
    {
        if (_currentState.CanBeInterrupted(newState)) ChangeState(newState);
    }

    public void Enable()
    {
        _initialState.Enter(_context);
        _currentState = _initialState;
    }

    public void Disable()
    {
        _currentState.Exit(_context);
        _currentState = null;
    }
}