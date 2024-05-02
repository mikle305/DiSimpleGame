using UnityEngine;

namespace SimpleGame.Data
{
    [CreateAssetMenu(menuName = "Configs/Game", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationCurve MinNumberByLevelCurve { get; private set; }
        [field: SerializeField] public AnimationCurve MaxNumberByLevelCurve { get; private set; }
    }
}