using System.Collections.Generic;
using UnityEngine;

public class BoulderCommandController : MonoBehaviour
{
    private List<BoulderCommand> commands = new List<BoulderCommand>();
    private int currentCommandIndex = -1;

    // A command to add to the command list
    public void ExecuteCommand(BoulderCommand command)
    {
        commands.Add(command);
        command.Execute();

        currentCommandIndex = commands.Count - 1;
    }

    // Undo the command and update the list and the counters
    public void UndoCommand()
    {
        if (currentCommandIndex < 0) {
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
}
