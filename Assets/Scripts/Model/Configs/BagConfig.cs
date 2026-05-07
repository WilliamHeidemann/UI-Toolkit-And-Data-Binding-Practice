using UnityEngine;

namespace Model.Configs
{
    [CreateAssetMenu(fileName = "New Bag Config", menuName = "Bag Config")]
    public class BagConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public int Capacity { get; set; }
    }
}