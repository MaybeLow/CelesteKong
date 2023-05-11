
public abstract class BoulderCommand
{
    protected IEntity entity;
    protected float time;

    public BoulderCommand(IEntity entity, float time)
    {
        this.entity = entity;
        this.time = time;
    }

    public abstract void Execute();

    public abstract void Undo();
}
