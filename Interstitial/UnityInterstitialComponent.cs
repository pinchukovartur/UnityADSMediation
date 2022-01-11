using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace ADSMediation
{
    public class UnityInterstitialComponent : MonoBehaviour, IInterstitialComponent, IUnityAdsListener
    {
        [SerializeField, Tooltip("Unity game id")]
        private string _gameId = "3220349";

        [SerializeField, Tooltip("Unity placement id")]
        private string _placementId = "";

        /// <inheritdoc />
        /// <summary>
        /// Is ready show reward video ?
        /// </summary>
        public bool IsReady
        {
            get { return Advertisement.IsReady(_placementId) && Advertisement.GetPlacementState(_placementId) == PlacementState.Ready; }
        }

        /// <inheritdoc />
        /// <summary>
        /// Is showing reward video in this time ?
        /// </summary>
        public bool IsShowing { get; set; }


        /// <inheritdoc />
        /// <summary>
        /// Action call when video closed watch
        /// </summary>
        public Action<string> OnCloseWatch { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Initialize component
        /// </summary>
        public void Init()
        {
            Advertisement.Initialize(_gameId);
            Advertisement.AddListener(this);
        }

        /// <inheritdoc />
        /// <summary>
        /// Start try load reward video
        /// </summary>
        public void TryLoad()
        {
            Advertisement.Load(_placementId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Start showing reward video
        /// </summary>
        public void Show()
        {
            if (!IsReady)
                return;

            IsShowing = true;
            Advertisement.Show(_placementId);
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            IsShowing = false;
            
            if (OnCloseWatch != null)
                OnCloseWatch.Invoke("Unity Reward ADS");
        }
    }
}