
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Commands.Level
{

    public class OnLevelLoaderCommand
    {
        private Transform _levelHolder;
        internal OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        internal void Execute(byte levelIndex)
        {
        //  Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {levelIndex}"),_levelHolder,true); 
        
  
         
            var request =Addressables.LoadAssetAsync<GameObject>($"Prefabs/LevelPrefabs/Level {levelIndex}");
            request.Completed += handle =>
            {
                Debug.Log(handle);
           var newLevel=  Object.Instantiate(request.Result as GameObject,Vector3.zero,Quaternion.identity);
           if (newLevel != null)
           {
               newLevel.transform.SetParent(_levelHolder.transform);
           }
            };
        }
    }

}
