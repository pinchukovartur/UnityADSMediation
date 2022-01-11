using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADSMediation
{
    public class GoogleInterstitialComponent : MonoBehaviour, IInterstitialComponent
    {
        [SerializeField, Tooltip("Interstitial Id")]
        private string _interstitialId;

        public bool IsReady { get { return _interstitial.IsLoaded(); } }
        public bool IsShowing { get; set; }
        public Action<string> OnCloseWatch { get; set; }

        private InterstitialAd _interstitial;

        public void Init()
        {
            // Initialize an InterstitialAd.
            _interstitial = new InterstitialAd(_interstitialId);

            // Called when an ad request has successfully loaded.
            _interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            _interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            _interstitial.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            _interstitial.OnAdClosed += HandleOnAdClosed;
        }

        private void HandleOnAdClosed(object sender, EventArgs e)
        {
            IsShowing = false;
            if (OnCloseWatch != null)
                OnCloseWatch.Invoke("Admob Interstitial ADS");
        }

        private void HandleOnAdOpened(object sender, EventArgs e)
        {
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
        }

        private void HandleOnAdLoaded(object sender, EventArgs e)
        {
        }

        public void Show()
        {
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
            }
        }

        public void TryLoad()
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            _interstitial.LoadAd(request);
        }

    }
}
