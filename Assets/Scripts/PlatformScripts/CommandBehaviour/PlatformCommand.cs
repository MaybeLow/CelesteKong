
public abstract class PlatformCommand
{
    protected IPEntity entity;
    protected float time;

    public PlatformCommand(IPEntity entity, float time)
    {
        this.entity = entity;
        this.time = time;
    }

    public abstract void Execute();

    public abstract void Undo();
}
