using System.Collections.Generic;
using Runtime.Commands.Input;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Interfaces;
using Runtime.Keys;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool _isAvaibleForTouch, _isFirstTimeTouchTaken, _isTouching;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;

        private Dictionary<int, IInputCommand> _inputCommands;
        private void Awake()
        {
            _data = GetInputData();

            InitCommands();
        }

        private void InitCommands()
        {
            _inputCommands = new Dictionary<int, IInputCommand>
            {
                { 0, new InputTakenCommand() },
                { 1, new InputReleasedCommand() },
                { 2, new InputDraggedCommand(new HorizontalInputParams()) }
            };
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").data;
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void SubscribeEvent()
        {
        
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onReset += OnReset;

    
             
        
        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
             
        }

         

    
        private void OnDisableInput()
        {
            _isAvaibleForTouch = false;
        }

        private void OnEnableInput()
        {
            _isAvaibleForTouch = true;
 
        }
        private void OnReset()
        {
            _isAvaibleForTouch = false;
           //_isFirstTimeTouchTaken = false; 
            _isTouching = false;

        }
  

        private void OnInputTaken()
        {
           

        }

      
      
        private void Update()
        {if(!_isAvaibleForTouch)return;
             if(IsPointerOverUIElements())return;
             
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                ExecuteInputCommand(1);  
            }

            if (Input.GetMouseButtonDown(0)  )
            {
                _isTouching = true;
                ExecuteInputCommand(0);
                if (_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }
             
                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) )
            {
               

                ExecuteInputCommand(2, new HorizontalInputParams
                {
                    HorizontalValue = CalculateHorizontalInput(),
                    ClampValues = _data.clampValues
                });
            }
        }
        private void ExecuteInputCommand(int commandId, HorizontalInputParams inputParams = new HorizontalInputParams())
        {
            if (_inputCommands.TryGetValue(commandId, out var command))
            {
                if (command is InputDraggedCommand draggedCommand)
                {
                    draggedCommand.Execute(inputParams);
                }
                else
                {
                    command.Execute();
                }
            }
        }
        private bool IsPointerOverUIElements()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData,results);
                return results.Count > 0;
        }
        private float CalculateHorizontalInput()
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    else
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                            _data.clampSpeed);

                    _moveVector.x = mouseDeltaPos.x;

                    _mousePosition = Input.mousePosition;

                  

                    
                    return _moveVector.x;
                }
            }
         

            return 0f;
        }
    }

}