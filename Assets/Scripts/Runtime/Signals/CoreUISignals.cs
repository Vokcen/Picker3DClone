using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoBehaviour
    {
        #region  Singleton

        
        private static CoreUISignals _instance;

        public static CoreUISignals Instance
        {
            get
            {
         
                if (_instance == null)
                {
                    
                    _instance = FindObjectOfType<CoreUISignals>();

                   
                }

                return _instance;
            }
        }

        private void Awake()
        {

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }

            
            _instance = this;
     
        }
        #endregion
        
        public UnityAction<UIPanelTypes,int>onOpenPanel=delegate {  };
        public UnityAction<int>onClosePanel=delegate {  };
        public UnityAction onCloseAllPanel=delegate {  };
    }
}