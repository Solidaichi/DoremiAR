using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Param")]
public class Param : ScriptableObject
{
    public float initSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float neighborDistance;
    public float neighborFov;
    public float separationWeight;
    public float wallScale;
    public float wallDistance;
    public float wallWeight;
    public float alignmentWeight;
    public float cohesionWeiight;
}
