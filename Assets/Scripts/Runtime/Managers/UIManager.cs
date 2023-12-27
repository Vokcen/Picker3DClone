using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
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
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInıtialize;
            CoreGameSignals.Instance.onLevelSuccesful += OnLevelSuccesful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInıtialize;
            CoreGameSignals.Instance.onLevelSuccesful -= OnLevelSuccesful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnLevelFailed()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail,2);
        }

        private void OnLevelSuccesful()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win,2);
        }

        private void OnLevelInıtialize(byte arg0)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,0);
            UISignals.Instance.onSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
        }

        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanel?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,1);

        }

        public void Play()
        {
            Debug.LogWarning("Execute ---> Play");
            UISignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            InputSignals.Instance.onEnableInput?.Invoke();
            //Camera Signals
        }
        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
         
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
        
        }
    }
}