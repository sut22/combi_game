using System.Collections.Generic;
using UnityEngine;

public class PassangersCrowdManager : MonoBehaviour
{
    [field:SerializeField] public Color FullAttendColor { get ; private set ; } 
    [field:SerializeField] public Color MediumAttendColor { get ; private set ; } 
    [field:SerializeField] public Color ExpiringAttendColor { get ; private set ; }

    [field:SerializeField] public Transform Outside { get ; private set ; }
    [field:SerializeField] public Transform Door { get ; private set ; }
    [field:SerializeField] public Transform EntranceCenter { get ; private set ; }

    [field:SerializeField] public Transform PassangersPrefab { get ; private set ; }
    [field:SerializeField] public List < Seat > Seats { get ; private set ; } 

    [SerializeField] private float minAttendTime , maxAttendTime ;
    
    //  STATE VARIABLES
    private List < Seat > availableSeats = new List < Seat > () ;

    private void Start ()
    {
        for ( int i = 0 ; i < Seats.Count ; i++ )
        {
            availableSeats.Add ( Seats [ i ] ) ;
        }

        Debug.Log ( availableSeats.Count ) ;
    }


    #region PUBLIC API ------------------------------------------
    public void SpawnPassanger () // CALLED FROM GAME MANAGER
    {
        Instantiate ( PassangersPrefab , Outside.position , Quaternion.identity ) ;
    }
    public float GetAttendTime () // CALLED FROM PASSANGER
    {
        return UnityEngine.Random.Range ( minAttendTime , maxAttendTime ) ;
    }
    
    public void MarkSeatRowAvailable ( Seat seatRow )
    { if ( !availableSeats.Contains ( seatRow ) ) availableSeats.Add ( seatRow ) ; }
    
    public void MarkSeatRowOccupied ( Seat seatRow )
    { if ( availableSeats.Contains ( seatRow ) ) availableSeats.Remove ( seatRow ) ; }

    public Seat GetAvailableSeat ()
    {
        int randomSeatRow = UnityEngine.Random.Range ( 0 , availableSeats.Count ) ;
        return availableSeats [ randomSeatRow ] ;
    }
    #endregion // -----------------------------------------------
}
