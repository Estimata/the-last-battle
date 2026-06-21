public interface IState<T>
{
    void Enter(T context);
    void UpdateState(T context);
    void Exit(T context);
}
