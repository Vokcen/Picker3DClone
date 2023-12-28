using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        [SerializeField] private List<DOTweenAnimation> tweens;
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _collectedCount;


        private readonly string _collectable="Collectable";
        private void Awake()
        {
            _data = GetData();
        }

        private PoolData GetData()
        {
                return Resources.Load<CD_Level>($"Data/CD_Level")
                    .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
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
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
        }  
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangePoolColor;
        }

        private void OnActivateTweens(byte stageValue)
        {
          if(stageValue!=stageID) return;

          foreach (var tween in tweens)
          {
              tween.DOPlay();
          }
        }

        private void OnChangePoolColor(byte stageValue)
        {
            if(stageValue!=stageID) return;
            renderer.material.DOColor(new Color(0.1607843f, 0.3137255f, 0.6039216f), 1).SetEase(Ease.Linear);
        }

        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}"; 
        }

        public bool TakeResults(byte managerStageValue)
        {
            if (stageID == managerStageValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
             if(!other.CompareTag(_collectable)) return;
             IncreaseCollectedAmount();
             SetCollectedAmountToPool();
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.CompareTag(_collectable)) return;
           DecreaseCollectedAmount();
            SetCollectedAmountToPool();
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}"; 

        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void     DecreaseCollectedAmount()
        {
            _collectedCount--;
        }
      
    }
}