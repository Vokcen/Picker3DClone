using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<Transform> layers = new List<Transform>();

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
         
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanel += OnCloseAllPanel;
        }   
        private void  UnSubscribeEvents()
        {         
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanel -= OnCloseAllPanel;
        }
        [Button]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate( Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);
        } 
        [Button]
        private void OnClosePanel(int value)
        {
          if(layers[value].childCount<=0)return;
    
           
    
              Destroy(layers[value].GetChild(0).gameObject);
              
         
        }    
 
        private void OnCloseAllPanel()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0)return;
                {
                    
         
 
               
                Destroy(layer.GetChild(0).gameObject);
 
                }

            }
        }

     

      
    }
}