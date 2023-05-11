
public class BoulderEmptyCommand : BoulderCommand
{
    public BoulderEmptyCommand(IEntity entity, float time) : base(entity, time)
    {
    }

    public override void Execute()
    {
    }

    public override void Undo()
    {
    }
}
