using System;
using Runtime.Commands.Player;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public byte StageValue;

        internal ForceBallsToPoolCommand ForceCommand;

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;

        private PlayerData _data;


        private void Awake()
        {
            _data= GetPlayerData();
            SendDataControllers();
            Init();
        }

        private void Init()
        {
            ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
        }

        private void SendDataControllers()
        {
            movementController.SetData(_data.MovementData);
            meshController.SetData(_data.MeshData);
            
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken += ()=> movementController.IsReadyToMove(true);
           InputSignals.Instance.OnInputRelased+= ()=> movementController.IsReadyToMove(false);
           InputSignals.Instance.OnInputDragged += OnInputDragged;
           UISignals.Instance.onPlay += ()=>   movementController.IsReadyToPlay(true);
           CoreGameSignals.Instance.onLevelSuccesful+=()=>movementController.IsReadyToPlay(false);
           CoreGameSignals.Instance.onLevelFailed+= ()=>movementController.IsReadyToPlay(false);
           CoreGameSignals.Instance.onStageAreaEntered+=  ()=>     movementController.IsReadyToPlay(false);
           CoreGameSignals.Instance.onStageAreaSuccessful+= OnStageAreaSuccessful;
           CoreGameSignals.Instance.onFinishAreaEntered+= OnFinishAreaEntered;
           CoreGameSignals.Instance.onReset+= OnReset;
        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.OnInputTaken -= ()=> movementController.IsReadyToMove(true);
            InputSignals.Instance.OnInputRelased -=  ()=> movementController.IsReadyToMove(false);
            InputSignals.Instance.OnInputDragged -= OnInputDragged;
            UISignals.Instance.onPlay -= ()=>   movementController.IsReadyToPlay(true);
            CoreGameSignals.Instance.onLevelSuccesful -= ()=>movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onLevelFailed -=  ()=>movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaEntered -= ()=>     movementController.IsReadyToPlay(false);
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
     
    
        private void OnInputDragged(HorizontalInputParams inputParams)
        {
          
            movementController.UpdateInputParams(inputParams);
        }

   
        private void OnStageAreaSuccessful(byte value)
        {
            StageValue = (byte)++value;
            movementController.IsReadyToPlay(true);
           
            meshController.ScaleUpPlayer();
            meshController.PlayConfetti();
            meshController.ShowUpText();
        }
      

    
        private void OnFinishAreaEntered()
        {
             CoreGameSignals.Instance.onLevelSuccesful?.Invoke();
             // Mini Game
        }

       
        private void OnReset()
        {
            StageValue = 0;
            movementController.OnReset();
            physicsController.OnReset();
            meshController.OnReset();
        }
    }
}