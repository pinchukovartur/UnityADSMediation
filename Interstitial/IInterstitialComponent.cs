using System;

namespace ADSMediation
{
    /// <summary>
    /// Interstitial ads component for ads manager
    /// </summary>
    public interface IInterstitialComponent
    {
        /// <summary>
        /// Is ready show reward video ?
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Is showing reward video in this time ?
        /// </summary>
        bool IsShowing { get; set; }

        /// <summary>
        /// Action call when video closed watch
        /// </summary>
        Action<string> OnCloseWatch { get; set; }

        /// <summary>
        /// Initialize component
        /// </summary>
        void Init();

        /// <summary>
        /// Start try load reward video
        /// </summary>
        void TryLoad();

        /// <summary>
        /// Start showing reward video
        /// </summary>
        void Show();
    }
}