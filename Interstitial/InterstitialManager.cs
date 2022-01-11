using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ADSMediation
{
    public class InterstitialManager : Singleton<InterstitialManager>
    {
        [SerializeField] private GameObject[] _componentObjects;
        private List<IInterstitialComponent> _components;

        public void Init()
        {
            _components = new List<IInterstitialComponent>();
            foreach (var componentObject in _componentObjects)
            {
                var component = componentObject.GetComponent<IInterstitialComponent>();
                if (component == null)
                {
                    Debug.LogError(string.Format("Error initialize - {0} object don't have IInterstitialComponent",
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

        public void TryLoadInterstitial()
        {
            foreach (var component in _components)
            {
                component.TryLoad();
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

        public bool IsInterstitilLoaded
        {
            get { return _components.Any(component => component.IsReady); }
        }

        public void Show()
        {
            foreach (var component in _components)
            {
                if (component.IsShowing)
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