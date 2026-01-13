using System;

namespace Core.Ads
{
    public interface IAdProvidable
    {
        AdData AdData { get; }

        void InitializeAdProvider(PlatformData data);

        IBannerAdProvider BannerAdsProvider { get; }
        IAdsProvider InterstitialAdsProvider { get; }
        IAdsProvider RewardedAdsProvider { get; }
    }

    public interface IAdsProvider
    {
        Action OnSuccess { get; }
        Action OnFailed { get; }

        string CurrentAdUnitId { get; }

        void Initialize(string adUnitId);

        void ShowAd(Action onSuccess = null, Action onFailed = null);
        void LoadAd();

        void DestroyAd();
    }

    public interface IBannerAdProvider : IAdsProvider
    {
        BannerAdPlacementData BannerAdPlacementData { get; }

        void SetBannerAdPlacementData(BannerAdPlacementData bannerAdPlacementData);

        void ApplyCustomProperties();
    }
}