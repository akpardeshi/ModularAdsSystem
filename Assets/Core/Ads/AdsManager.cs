using System.Collections.Generic;
using UnityEngine;

namespace Core.Ads
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField] private AdsDataSO adsDataSo;

        private IAdProvidable currentAdProvider;

        private AdPlatform currentAdPlatform;

        private readonly Dictionary<AdPlatform, PlatformData> platformDataDictionary = new();

        void Start()
        {
            InitializeCurrentPlatform();
            InitializePlatformDataDictionary();
            InitializeAdProvider();
        }

        void InitializeCurrentPlatform()
        {
            #if UNITY_ANDROID
            currentAdPlatform = AdPlatform.Android;
            #elif UNITY_IOS
            currentAdPlatform = AdPlatform.iOS;
            #endif
        }

        void InitializePlatformDataDictionary()
        {
            AdData adData = adsDataSo.GetAdData(AdProviderType.AdMob);
            foreach (var data in adData.platformData)
            {
                platformDataDictionary.TryAdd(data.platform, data);
            }
        }

        void InitializeAdProvider()
        {
            if (!platformDataDictionary.TryGetValue(currentAdPlatform, out var platformData))
            {
                Debug.LogError("No platform data found for current platform");
                return;
            }
            
            AdMobAdServiceProvider adMobAdServiceProvider = new AdMobAdServiceProvider();
            currentAdProvider = adMobAdServiceProvider;
            currentAdProvider.InitializeAdProvider(platformData);
        }

        public void ShowBannerAd()
        {
            currentAdProvider?.BannerAdsProvider.ShowAd
            (
                () => { },
                () => { }
            );
        }

        public void ShowRewardedAd()
        {
            currentAdProvider?.RewardedAdsProvider.ShowAd
            (
                () => { },
                () => { }
            );
        }

        public void ShowInterstitialAd()
        {
            currentAdProvider?.InterstitialAdsProvider.ShowAd
            (
                () => { },
                () => { }
            );
        }

        public void RequestBannerAd()
        {
            currentAdProvider?.BannerAdsProvider.LoadAd();
        }

        public void RequestInterstitialAd()
        {
            currentAdProvider?.InterstitialAdsProvider.LoadAd();
        }

        public void RequestRewardedAd()
        {
            currentAdProvider?.RewardedAdsProvider.LoadAd();
        }

        public void DestroyBannerAd()
        {
            currentAdProvider?.BannerAdsProvider.DestroyAd();
        }
        
        public void DestroyInterstitialAd()
        {
            currentAdProvider?.InterstitialAdsProvider.DestroyAd();
        }
        
        public void DestroyRewardedAd()
        {
            currentAdProvider?.RewardedAdsProvider.DestroyAd();
        }
    }
}