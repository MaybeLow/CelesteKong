
public class BoulderDirectionCommand : BoulderCommand
{
    private Boulder boulder;
    
    // The purpose of this command is to keep track of direction changes when the boulder hits the ground
    public BoulderDirectionCommand(IEntity entity, float time, Boulder boulder) : base(entity, time)
    {
        this.boulder = boulder;
    }

    public override void Execute()
    {
        boulder.ChangeBoulderDirection();
    }

    public override void Undo()
    {
        boulder.ChangeBoulderDirection();
    }
}
