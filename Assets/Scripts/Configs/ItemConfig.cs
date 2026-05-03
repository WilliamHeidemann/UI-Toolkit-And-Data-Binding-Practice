using UnityEngine;

namespace Configs
{
    [CreateAssetMenu]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; set; }
    }
}