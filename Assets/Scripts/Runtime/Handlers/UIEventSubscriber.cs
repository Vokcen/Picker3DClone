using System;
using Runtime.Enums;
using Runtime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber: MonoBehaviour
    {
        [SerializeField] private UIEventSubscriptionType type;
        [SerializeField] private Button button;

    [ShowInInspector]
      private UIManager _manager;


      private void Awake()
      {
          GetReferances();
      }

      private void GetReferances()
      {
          _manager = FindObjectOfType<UIManager>();
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
          switch (type)
          {
              case UIEventSubscriptionType.OnPlay:
                  button.onClick.AddListener(_manager.Play);
                  break;
              case UIEventSubscriptionType.OnNextLevel:
                  button.onClick.AddListener(_manager.NextLevel);
                  break;
              case UIEventSubscriptionType.OnRestartLevel:
                  button.onClick.AddListener(_manager.RestartLevel);
                  break;
              default:
                  throw new ArgumentOutOfRangeException();
          }
      }
      private void UnSubscribeEvent()
      {
          switch (type)
          {
              case UIEventSubscriptionType.OnPlay:
                  button.onClick.RemoveListener(_manager.Play);
                  break;
              case UIEventSubscriptionType.OnNextLevel:
                  button.onClick.RemoveListener(_manager.NextLevel);
                  break;
              case UIEventSubscriptionType.OnRestartLevel:
                  button.onClick.RemoveListener(_manager.RestartLevel);
                  break;
              default:
                  throw new ArgumentOutOfRangeException();
          }
          
            
      }
    }
}