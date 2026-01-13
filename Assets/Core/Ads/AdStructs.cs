using System.Collections.Generic;
using GoogleMobileAds.Api;

namespace Core.Ads
{
    [System.Serializable]
    public struct AdData
    {
        public AdProviderType adProviderType;
        public List<PlatformData> platformData;
    }

    [System.Serializable]
    public struct PlatformData
    {
        public AdPlatform platform;
        public List<AdSource> adSource;
        public BannerPlacementDataSO bannerPlacementData;
    }

    [System.Serializable]
    public struct AdSource
    {
        public AdSourceType adSourceType;
        public string adUnitID;
    }

    [System.Serializable]
    public struct BannerAdPlacementData
    {
        public AdPosition adPosition;
    }
}