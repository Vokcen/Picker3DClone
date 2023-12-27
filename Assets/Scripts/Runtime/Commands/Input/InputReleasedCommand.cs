using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class InputReleasedCommand : IInputCommand
    {
        public void Execute()
        {
            InputSignals.Instance.OnInputRelased?.Invoke();
            Debug.LogWarning("Executed ----> OnInputRelased");
        }
    }
}