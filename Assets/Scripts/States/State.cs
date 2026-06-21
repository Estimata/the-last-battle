public abstract class State<T> : IState<T>
{
    public virtual void Enter(T context) { }
    public virtual void UpdateState(T context) { }
    public virtual void Exit(T context) { }
}
