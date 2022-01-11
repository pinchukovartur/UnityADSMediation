using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Libs.AdsManager
{
    public class RewardVideoManager : Singleton<RewardVideoManager>
    {    
        [SerializeField] private GameObject[] _componentObjects;
        private List<IVideoRewardComponent> _components;

        public void Init()
        {
            _components = new List<IVideoRewardComponent>();
            foreach (var componentObject in _componentObjects)
            {
                var component = componentObject.GetComponent<IVideoRewardComponent>();
                if (component == null)
                {
                    Debug.LogError(string.Format("Error initialize - {0} object don't have IVideoRewardComponent", 
                        componentObject.name));
                    continue;
                }
                
                _components.Add(component);
            }

            foreach (var component in _components)
            {
                component.Init();
            }
        }

        public void TryLoadVideo()
        {
            foreach (var component in _components)
            {
                component.TryLoad();
            }
        }

        public bool IsNeedReward
        {
            get { return _components.Any(component => component.IsNeedReward); }
        }

        public void RewardComplete()
        {
            foreach (var component in _components)
            {
                component.IsNeedReward = false;
            }
        }

        public Action<string> OnCloseWatch
        {
            set
            {
                Action<string> unlockAction = (s) =>
                {
                    LockUIADSAdapter.UnLockUI();
                };

                foreach (var component in _components)
                {
                    component.OnCloseWatch = value;
                    component.OnCloseWatch += unlockAction;
                }
            }
        }

        public bool IsVideoLoaded
        {
            get { return _components.Any(component => component.IsReady); }
        }

        public void Show()
        {
            foreach (var component in _components)
            {
                if(component.IsShowing)
                    return;
            }

            foreach (var component in _components)
            {
                if (!component.IsReady) 
                    continue;

                LockUIADSAdapter.LockUI();
                component.Show();
                break;
            }
        }
        
    }
}