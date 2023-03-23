using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCommandController : MonoBehaviour
{
    private List<PlatformCommand> commands = new List<PlatformCommand>();
    private int currentCommandIndex = -1;

    public void ExecuteCommand(PlatformCommand command)
    {
        commands.Add(command);
        command.Execute();

        currentCommandIndex = commands.Count - 1;
    }

    public void UndoCommand()
    {
        if (currentCommandIndex < 0)
        {
            return;
        }

        commands[currentCommandIndex].Undo();
        commands.RemoveAt(currentCommandIndex);
        currentCommandIndex--;
    }

    public void ResetCommandList()
    {
        currentCommandIndex = -1;
        commands.Clear();
    }

    public bool IsEmpty()
    {
        return commands.Count == 0;
    }

    public void AddBuffer(List<PlatformCommand> buffer)
    {
        commands.AddRange(buffer);
    }
}
