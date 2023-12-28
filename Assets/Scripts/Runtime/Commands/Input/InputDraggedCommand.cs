using Runtime.Interfaces;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Input
{
    public class InputDraggedCommand : IInputCommand
    {
        private readonly HorizontalInputParams _inputParams;

        public InputDraggedCommand(HorizontalInputParams inputParams)
        {
            _inputParams = inputParams;
        }

        public void Execute()
        {
          //  InputSignals.Instance.OnInputDragged?.Invoke(_inputParams);
            //Debug.LogWarning("Executed ----> OnInputDragged");
        }

        public void Execute(HorizontalInputParams inputParams)
        {
          
            InputSignals.Instance.OnInputDragged?.Invoke(inputParams);
            Debug.LogWarning("Executed ----> OnInputDragged");
        }
    }
}