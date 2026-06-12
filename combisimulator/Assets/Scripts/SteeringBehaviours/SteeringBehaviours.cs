using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    public static Vector3 Seek ( IAgent agent, IAgent target)
    {
        Vector3 desired = ( target.Position - agent.Position ).normalized * agent.MaxSpeed ;
        Vector3 steering = Vector3.ClampMagnitude ( desired - agent.Velocity , agent.MaxForce ) ;
        return steering ;
    }

    public static Vector3 Arrive ( IAgent agent , IAgent target , float arriveRadius )
    {
        Vector3 desired = ( target.Position - agent.Position ).normalized * agent.MaxSpeed ;

        if (Vector3.Distance ( agent.Position , target.Position ) < arriveRadius )
        {
            desired *= Vector3.Distance ( agent.Position, target.Position ) / arriveRadius ;
        }

        Vector3 steering = Vector3.ClampMagnitude ( desired - agent.Velocity , agent.MaxForce ) ;
        return steering ;
    }

    public class SimpleAgent : IAgent
    {
        public float MaxSpeed { get ; set ; }
        public float MaxForce { get ; set ; }
        public Vector3 Position { get ; set ; }
        public Vector3 Velocity { get ; set ; }

        public SimpleAgent ( Vector3 position )
        {
            Position = position;
            Velocity = Vector3.zero;
            MaxSpeed = 0;
            MaxForce = 0;
        }
    }
}
