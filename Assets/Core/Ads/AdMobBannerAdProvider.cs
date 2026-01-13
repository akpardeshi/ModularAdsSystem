using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Core.Ads
{
    public class AdMobBannerAdProvider : IBannerAdProvider
    {
        public Action OnSuccess { get; private set; }
        public Action OnFailed { get; private set; }
        public string CurrentAdUnitId { get; private set; }

        private AdPosition CurrentAdPosition { get; set; } = AdPosition.Top;
        private AdSize AdSize { get; } = AdSize.Banner;
        private BannerView bannerView;
        private Vector2Int customAdPosition;

        public void Initialize(string adUnitId)
        {
            CurrentAdUnitId = adUnitId;
        }

        public BannerAdPlacementData BannerAdPlacementData { get; private set; }

        private void SetAdPosition()
        {
            CurrentAdPosition = BannerAdPlacementData.adPosition;
        }

        public void SetBannerAdPlacementData(BannerAdPlacementData bannerAdPlacementData)
        {
            BannerAdPlacementData = bannerAdPlacementData;
        }

        public void ApplyCustomProperties()
        {
            SetAdPosition();
        }

        public void ShowAd(Action onSuccess = null, Action onFailed = null)
        {
            if (bannerView == null)
            {
                LoadAd();
                return;
            }

            OnSuccess = onSuccess;
            OnFailed = onFailed;

            bannerView?.Show();
        }

        public void LoadAd()
        {
            if (bannerView != null)
            {
                return;
            }

            bannerView = new BannerView(CurrentAdUnitId, AdSize, CurrentAdPosition);

            bannerView.LoadAd(new AdRequest());

            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                OnFailed?.Invoke();
                Debug.Log($"AdMobBannerAd.ShowAd error: {error}.");
                DestroyAd();
                LoadAd();
            };
            
            HideAd();
        }

        public void DestroyAd()
        {
            if (bannerView == null) return;

            bannerView.Destroy();
            bannerView = null;
        }

        private void HideAd()
        {
            if (bannerView == null) return;

            bannerView.Hide();
        }
    }
}