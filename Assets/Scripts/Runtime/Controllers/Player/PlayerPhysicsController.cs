using System;
using DG.Tweening;
using Runtime.Controllers.Pool;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidBody;

        private const string _stageArea = "StageArea";
        private const string _finish = "FinishArea";
        private const string _miniGame = "MiniGameArea";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                
                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeResults(manager.StageValue);

                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                    }
                    else
                    {
                        CoreGameSignals.Instance.onLevelFailed?.Invoke();
                        
                         
                    }

                });
                return;
            }

            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccesful?.Invoke();
                return;
            }

            if (other.CompareTag(_miniGame))
            {
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var managerTransform = manager.transform;
            var position = managerTransform.position;
            Gizmos.DrawSphere(new Vector3(position.x,position.y-1,position.z+.9f), 1.7f);
        }

        public void OnReset()
        {
            
        }
    }
}