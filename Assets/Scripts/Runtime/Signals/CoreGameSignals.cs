using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        #region  Singleton
        private static CoreGameSignals _instance;


        public static CoreGameSignals Instance
        {
            get
            {
         
                if (_instance == null)
                {
                    
                    _instance = FindObjectOfType<CoreGameSignals>();

                   
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
        
        public UnityAction<byte>onLevelInitialize=delegate{  };
        public UnityAction onClearActiveLevel=delegate{  };
        public UnityAction onLevelSuccesful=delegate{  };
        public UnityAction onLevelFailed=delegate{  };
        public UnityAction onNextLevel=delegate{  };
        public UnityAction onRestartLevel=delegate{  };
        public UnityAction onReset=delegate{  };
        public Func<byte> onGetLevelValue= delegate { return 0;}; 
        public UnityAction  onStageAreaEntered = delegate {  };
        public UnityAction<byte>  onStageAreaSuccessful = delegate {  };
        public UnityAction onFinishAreaEntered = delegate {  };



    }    

}