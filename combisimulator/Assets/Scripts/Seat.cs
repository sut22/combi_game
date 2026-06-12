using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour , IAgent
{
    public Vector3 Position { get { return transform.position ; } set { transform.position = value ; } }
    public Vector3 Velocity { get ; set ; }
    public float MaxSpeed { get ; set ; }
    public float MaxForce { get ; set ; }

    [field:SerializeField] public List < Seat > Seats { get ; private set ; } 

    //  STATE VARIABLES
    private List < Seat > availableSeats = new List < Seat > () ;

    //  REFERENCES
    PassangersCrowdManager passangersCrowdManager ;

    private void Start ()
    {
        passangersCrowdManager = FindAnyObjectByType < PassangersCrowdManager > () ;

        for ( int i = 0 ; i < Seats.Count ; i++ )
        {
            availableSeats.Add ( Seats [ i ] ) ;
        }
    }

    public void Sit ()
    {
        passangersCrowdManager.MarkSeatRowOccupied ( this ) ;
    }

    public void Free ()
    {
        passangersCrowdManager.MarkSeatRowAvailable ( this ) ;
    }

    public Seat GetAvailableSeat ()
    {
        int randomSeat = UnityEngine.Random.Range ( 0 , availableSeats.Count ) ;
        return availableSeats [ randomSeat ] ;
    }
}
