public abstract class State<T> : IState<T>
{
    public virtual void Enter(T context) { }
    public virtual void Update(T context) { }
    public virtual void Exit(T context) { }
    public virtual bool CanBeInterrupted(IState<T> newState) => true;
}
