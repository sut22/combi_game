using UnityEngine;

public interface IAgent
{
    Vector3 Position { get ; set ; }
    Vector3 Velocity { get ; set ; }
    float MaxSpeed { get ; set ; }
    float MaxForce { get ; set ; }
}