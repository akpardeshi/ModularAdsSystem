using System.Collections.Generic;
using UnityEngine;

namespace Core.Ads
{
    [CreateAssetMenu(fileName = "AdsDataSO", menuName = "Scriptable Objects/AdsDataSO")]
    public class AdsDataSO : ScriptableObject
    {
        [SerializeField] private List<AdData> adsData;

        public AdData GetAdData(AdProviderType adProviderType)
        {
            AdData adData = adsData.Find(x => x.adProviderType == adProviderType);
            return adData;
        }
    }
}