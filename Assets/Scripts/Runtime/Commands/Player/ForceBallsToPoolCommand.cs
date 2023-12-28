using System.Linq;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using UnityEngine;
 

namespace Runtime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        
        private PlayerManager _manager;
        private PlayerForceData _forceData;
        
        public ForceBallsToPoolCommand(PlayerManager manager, PlayerForceData ForceData)
        {
            _manager = manager;
            _forceData = ForceData;
        }

        internal void Execute()
        {
            var managerTransform = _manager.transform;
            var position = managerTransform.position;
            var forcePos =new Vector3(position.x,position.y-1,position.z+.9f);;

            var collider = Physics.OverlapSphere(forcePos, 1.7f);
       
            var collectibleColliderList= collider.Where(col=>col.CompareTag("Collectable")).ToList();

            foreach (var col in collectibleColliderList)
            {
                if (col.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddForce(new Vector3(0,_forceData.ForceParaMeters.y,_forceData.ForceParaMeters.z), ForceMode.Impulse);
                }
            }
            collectibleColliderList.Clear();
            ;
        }
    }
}