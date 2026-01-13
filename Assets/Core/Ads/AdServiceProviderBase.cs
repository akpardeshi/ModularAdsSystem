using System;
using UnityEngine;

namespace Core.Ads
{
    public class AdServiceProviderBase : IAdProvidable
    {
        public IBannerAdProvider BannerAdsProvider { get; protected set;}
        public IAdsProvider InterstitialAdsProvider { get; protected set;}
        public IAdsProvider RewardedAdsProvider { get; protected set;}

        public AdData AdData { get; private set; }
        
        public PlatformData PlatformData { get; private set; }

        public virtual void InitializeAdProvider(PlatformData data)
        {
            
            PlatformData = data;
        }
    }
}