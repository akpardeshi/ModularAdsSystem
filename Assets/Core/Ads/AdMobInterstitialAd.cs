using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Core.Ads
{
    public class AdMobInterstitialAd : IAdsProvider
    {
        public Action OnSuccess { get; private set; }
        public Action OnFailed { get; private set; }
        public string CurrentAdUnitId { get; private set; }

        private InterstitialAd interstitialAd;

        public void Initialize(string adUnitId)
        {
            CurrentAdUnitId = adUnitId;

            LoadAd();
        }

        public void ShowAd(Action onSuccess = null, Action onFailed = null)
        {
            if (interstitialAd == null)
            {
                LoadAd();
                return;
            }

            OnSuccess = onSuccess;
            OnFailed = onFailed;

            if (interstitialAd.CanShowAd())
            {
                interstitialAd?.Show();
            }
        }

        public void LoadAd()
        {
            if (interstitialAd != null)
            {
                return;
            }
            
            var adRequest = new AdRequest();

            InterstitialAd.Load(CurrentAdUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                interstitialAd = ad;

                if (error != null)
                {
                    return;
                }
                
                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    OnSuccess?.Invoke();
                    DestroyAd();
                    LoadAd();
                };

                interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    OnFailed?.Invoke();
                    DestroyAd();
                    LoadAd();
                };
            });
        }

        public void DestroyAd()
        {
            if (interstitialAd == null) return;

            interstitialAd?.Destroy();
            interstitialAd = null;
        }
    }
}