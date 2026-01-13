using UnityEngine;

namespace Core.Ads
{
    [CreateAssetMenu(fileName = "BannerPlacementDataSO", menuName = "Scriptable Objects/BannerPlacementDataSO")]
    public class BannerPlacementDataSO : ScriptableObject
    {
        public BannerAdPlacementData customProperties;
    }
}