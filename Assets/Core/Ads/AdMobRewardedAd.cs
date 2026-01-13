using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Core.Ads
{
    public class AdMobRewardedAd : IAdsProvider
    {
        public Action OnSuccess { get; private set; }
        public Action OnFailed { get; private set;}
        public string CurrentAdUnitId { get; private set; }

        private RewardedAd rewardedAd;

        public void Initialize(string adUnitId)
        {
            CurrentAdUnitId = adUnitId;

            LoadAd();
        }

        public void ShowAd(Action onSuccess = null, Action onFailed = null)
        {
            if (rewardedAd == null)
            {
                LoadAd();
                return;
            }
            
            OnSuccess = onSuccess;
            OnFailed = onFailed;

            if (rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward r) =>
                {
                    OnSuccess?.Invoke();
                    LoadAd();
                });
            }
        }

        public void LoadAd()
        {
            if (rewardedAd != null)
            {
                DestroyAd();
            }
            
            var adRequest = new AdRequest();

            RewardedAd.Load(CurrentAdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                rewardedAd = ad;

                if (error != null)
                {
                    Debug.Log($"AdMobRewardedAd.LoadAd error {error} {CurrentAdUnitId}.");
                    return;
                }
                
                rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.Log($"The AdMob rewarded ad error {error}.");
                    
                    OnFailed?.Invoke();
                    LoadAd();
                };
            });
        }

        public void DestroyAd()
        {
            if (rewardedAd == null) return;
            
            rewardedAd?.Destroy();
            rewardedAd = null;
        }
    }
}