public interface IState<T>
{
    void Enter(T context);
    void Update(T context);
    void Exit(T context);
    bool CanBeInterrupted (IState<T> newState);
}
