using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Source.Scripts.Libs.AdsManager
{
    /// <inheritdoc cref="IVideoRewardComponent"/>
    /// <summary>
    /// Google Admob Reward video component
    /// </summary>
    public class GoogleRewardVideoComponent : MonoBehaviour, IVideoRewardComponent
    {
        [SerializeField, Tooltip("Video Banner Id")]
        private string _videoBannerId;
        
        /// <summary>
        /// Rewarder video object
        /// </summary>
        private RewardedAd _rewardBasedVideo;

        /// <inheritdoc />
        /// <summary>
        /// Is ready show reward video ?
        /// </summary>
        public bool IsReady { get { return _rewardBasedVideo.IsLoaded(); } }

        /// <inheritdoc />
        /// <summary>
        /// Is need reward user for watch reward video?
        /// </summary>
        public bool IsNeedReward { get; set; }
        
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
            _rewardBasedVideo = new RewardedAd(_videoBannerId);;

            _rewardBasedVideo.OnAdLoaded += VideoLoaded;
            _rewardBasedVideo.OnAdFailedToLoad += VideoFailedToLoad;
            _rewardBasedVideo.OnUserEarnedReward += VideoRewarded;
            _rewardBasedVideo.OnAdClosed += VideoClosed;

            TryLoad();
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Start try load reward video
        /// </summary>
        public void TryLoad()
        {
            if (!_rewardBasedVideo.IsLoaded())
                return;

            var request = new AdRequest.Builder().Build();
            _rewardBasedVideo.LoadAd(request);
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
            _rewardBasedVideo.Show();
        }

        /// <summary>
        /// On video load callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VideoLoaded(object sender, EventArgs args)
        {
        }

        /// <summary>
        /// On video failed load callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        }

        /// <summary>
        /// On video closed callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VideoClosed(object sender, EventArgs args)
        {
            IsShowing = false;
            if (OnCloseWatch != null)
                OnCloseWatch.Invoke("Admob Reward ADS");
        }

        /// <summary>
        /// On video complete watch callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void VideoRewarded(object sender, Reward args)
        {
            IsNeedReward = true;
        }

    }
}