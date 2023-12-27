using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class InputTakenCommand:IInputCommand
    {
        public void Execute()
        {
            InputSignals.Instance.OnInputTaken?.Invoke();
            Debug.LogWarning("Executed ----> OnInputTaken");
        }
    }
}