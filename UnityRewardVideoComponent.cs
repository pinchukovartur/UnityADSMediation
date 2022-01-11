using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Source.Scripts.Libs.AdsManager
{
    public class UnityRewardVideoComponent : MonoBehaviour, IVideoRewardComponent, IUnityAdsListener
    {
        [SerializeField, Tooltip("Unity game id")]
        private string _gameId = "3220349";
        
        [SerializeField, Tooltip("Unity placement id")]
        private string _placementId = "OpenLater";

        /// <inheritdoc />
        /// <summary>
        /// Is ready show reward video ?
        /// </summary>
        public bool IsReady
        {
            get { return Advertisement.IsReady(_placementId) && Advertisement.GetPlacementState(_placementId) == PlacementState.Ready; }
            //get { return false; }
            set { }
        }

        /// <inheritdoc />
        /// <summary>
        /// Is showing reward video in this time ?
        /// </summary>
        public bool IsShowing { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Is need reward user for watch reward video?
        /// </summary>
        public bool IsNeedReward { get; set; }

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
            switch (showResult)
            {
                case ShowResult.Failed:
                    IsNeedReward = false;
                    break;
                case ShowResult.Skipped:
                    IsNeedReward = false;
                    break;
                case ShowResult.Finished:
                    IsNeedReward = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("showResult", showResult, null);
            }

            if (OnCloseWatch != null)
                OnCloseWatch.Invoke("Unity Reward ADS");
        }
    }
}