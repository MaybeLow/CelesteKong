using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderCommandController : MonoBehaviour
{
    private List<BoulderCommand> commands = new List<BoulderCommand>();
    private int currentCommandIndex = -1;

    public void ExecuteCommand(BoulderCommand command)
    {
        commands.Add(command);
        command.Execute();

        currentCommandIndex = commands.Count - 1;
    }

    public void UndoCommand()
    {
        if (currentCommandIndex < 0) {
            return;
        }

        commands[currentCommandIndex].Undo();
        commands.RemoveAt(currentCommandIndex);
        currentCommandIndex--;
    }
}
