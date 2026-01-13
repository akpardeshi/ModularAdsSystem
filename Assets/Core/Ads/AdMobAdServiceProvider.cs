using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Core.Ads
{
    public class AdMobAdServiceProvider : AdServiceProviderBase
    {
        private PlatformData platformData;

        private Dictionary<AdSourceType, AdSource> adsProvidersDictionary;

        private void InitializeAdMob()
        {
            InitializeAdMobBannerAdProvider();

            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                BannerAdsProvider = new AdMobBannerAdProvider();
                adsProvidersDictionary.TryGetValue(AdSourceType.Banner, out var bannerAddSource);
                BannerAdsProvider.Initialize(bannerAddSource.adUnitID);
                BannerAdsProvider.SetBannerAdPlacementData(platformData.bannerPlacementData.customProperties);
                BannerAdsProvider.ApplyCustomProperties();

                InterstitialAdsProvider = new AdMobInterstitialAd();
                adsProvidersDictionary.TryGetValue(AdSourceType.Interstitial, out var interstitialAddSource);
                InterstitialAdsProvider.Initialize(interstitialAddSource.adUnitID);

                RewardedAdsProvider = new AdMobRewardedAd();
                adsProvidersDictionary.TryGetValue(AdSourceType.Rewarded, out var rewardedAddSource);
                RewardedAdsProvider.Initialize(rewardedAddSource.adUnitID);
            });
        }

        public override void InitializeAdProvider(PlatformData data)
        {
            base.InitializeAdProvider(data);
            platformData = data;
            InitializeAdMob();
        }

        private void InitializeAdMobBannerAdProvider()
        {
            adsProvidersDictionary = new();

            foreach (var adSource in platformData.adSource)
            {
                adsProvidersDictionary.TryAdd(adSource.adSourceType, adSource);
            }
        }
    }
}