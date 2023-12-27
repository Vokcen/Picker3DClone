using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    [CreateAssetMenu(fileName = "InputSignals", menuName = "Picker3D/Input Signals", order = 1)]
    public class InputSignals : ScriptableObject
    {
        #region Singleton

        private static InputSignals _instance;

        public static InputSignals Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<InputSignals>("InputSignals");

                    if (_instance == null)
                    {
                        _instance = CreateInstance<InputSignals>();
                        
                    }
                }

                return _instance;
            }
        }

        #endregion

        public UnityAction onFirstTimeTouchTaken=delegate{  };
        public UnityAction onEnableInput=delegate {  };
        public UnityAction onDisableInput=delegate {  };
        public UnityAction OnInputTaken=delegate{  };
        public UnityAction OnInputRelased=delegate{  };
        public UnityAction<HorizontalInputParams> OnInputDragged=delegate{  };
    }
}