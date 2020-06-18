using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "GameSettings/Config")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float blockLoopDelta;
    [SerializeField] private float blockStartOffset;
    [SerializeField] private float levelHeight;
    [SerializeField] private float blocksSpeed;
    [SerializeField] private float hitAccuracy;

    public float BlockLoopDelta     => blockLoopDelta;
    public float BlockStartOffset   => blockStartOffset;
    public float LevelHeight        => levelHeight;
    public float BlocksSpeed        => blocksSpeed;
    public float HitAccuracy        => hitAccuracy;

}
