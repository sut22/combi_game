using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Passanger : MonoBehaviour , IAgent
{
    public Vector3 Position { get { return transform.position ; } set { transform.position = value ; } }
    public Vector3 Velocity { get ; set ; } 
    public float MaxSpeed { get ; set ; } = 5f ;
    public float MaxForce { get ; set ; } = 7f ;

    //  DECLARATIONS
    public enum State { WalkingIn , Seated , Attended , WalkingOut }
    public enum WalkingProgress { Outside , Door , Center , SeatRow , Seat }

    //  REFERENCES
    private PassangersCrowdManager manager ;
    [SerializeField] private Canvas canvas ;
    [SerializeField] private Image filledImage ;
    [SerializeField] private TextMeshProUGUI imageText ;
    private bool CanCharge;

    //  PROPERTIES
    [field:SerializeField] public State CurrentState { get ; private set ; }
    private float attendTime ;

    //  STATE VARIABLES
    private float _attendanceWait , _attendancePercent ;
    private WalkingProgress _walkingProgress ;
    private Seat chosenSeat ;


    float distanceW = 0.2f ;

    private void Awake()
    {
        CanCharge = false;
    }

    private void Start ()
    {
        manager = FindAnyObjectByType <  PassangersCrowdManager > () ;

        SwitchState ( State.WalkingIn ) ;


        attendTime = manager.GetAttendTime () ;
        canvas.enabled = false ;
    }


    private void Update ()
    {
        ManageStatesBehaviour () ;
        ManageVisuals () ;
    }

    private void ManageVisuals ()
    {
        if ( _attendanceWait > 0 && _attendanceWait < attendTime )
        {
            _attendancePercent = 1 - ( attendTime - _attendanceWait ) / attendTime ;
            filledImage.fillAmount = _attendancePercent ;
            imageText.text = Mathf.FloorToInt ( _attendanceWait ).ToString () ;
        }
        if ( _attendancePercent > 0.65f )
            filledImage.color = manager.FullAttendColor ;
        else if ( _attendancePercent > 0.325f )
            filledImage.color = manager.MediumAttendColor ;
        else
            filledImage.color = manager.ExpiringAttendColor ;
    }

    #region STATE BEHAVIOURS --------------------------------------------------------------------------
    private void ManageStatesBehaviour ()
    {
        switch ( CurrentState )
        {
            case State.WalkingIn : case State.WalkingOut : ManageWalking () ; break ;
            case State.Seated : case State.Attended : ManageLifecycle () ; break ;
        }
    }
    
    //  los pasajeros esperan un tiempo, y luego se van, independientemente d si los atienden o no
    private void ManageLifecycle ()
    {
        if ( _attendanceWait > 0f )
            _attendanceWait -= Time.deltaTime ;
        else
        {
            //  el pasajero ya se va y nunca fue atendido
            if ( CurrentState == State.Seated )
                GameManager.Instance.RegisterNegative () ;

            SwitchState ( State.WalkingOut ) ;
        }
    }

    private void ManageWalking ()
    {
        Velocity = Vector3.ClampMagnitude ( Velocity , MaxForce ) ;

        if ( CurrentState == State.WalkingIn )
        {
            if ( _walkingProgress == WalkingProgress.Outside )
            {
                Velocity += SteeringBehaviours.Seek ( this , new SteeringBehaviours.SimpleAgent ( manager.Door.position ) ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , manager.Door.position ) < distanceW )
                {
                    _walkingProgress = WalkingProgress.Door ;
                    Velocity = Vector3.zero ;
                }
                    
            }
            else if ( _walkingProgress == WalkingProgress.Door )
            {
                Velocity += SteeringBehaviours.Seek ( this , new SteeringBehaviours.SimpleAgent ( manager.EntranceCenter.position ) ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , manager.EntranceCenter.position ) < distanceW )
                {
                    _walkingProgress = WalkingProgress.Center ;
                    Velocity = Vector3.zero ;
                }
            }
            else if ( _walkingProgress == WalkingProgress.Center )
            {
                if ( chosenSeat == null)
                {
                    chosenSeat = manager.GetAvailableSeat () ;
                    chosenSeat.Sit () ;
                }
                

                Velocity += SteeringBehaviours.Seek ( this , chosenSeat ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , chosenSeat.Position ) < distanceW )
                {
                    _walkingProgress = WalkingProgress.Seat ;
                    Velocity = Vector3.zero ;
                    // Debug.Log ( "Arrived" ) ;
                }
            }
            else if ( _walkingProgress == WalkingProgress.Seat )
            {
                SwitchState ( State.Seated ) ;
            }
        }

        if ( CurrentState == State.WalkingOut )
        {
            if ( _walkingProgress == WalkingProgress.Seat )
            {
                if ( chosenSeat != null )
                {
                    chosenSeat.Free () ;
                    chosenSeat = null ;
                }
                

                Velocity += SteeringBehaviours.Seek ( this , new SteeringBehaviours.SimpleAgent ( manager.EntranceCenter.position ) ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , manager.EntranceCenter.position ) < 0.05f )
                    _walkingProgress = WalkingProgress.Center ;
            }
            else if ( _walkingProgress == WalkingProgress.Center )
            {
                Velocity += SteeringBehaviours.Seek ( this , new SteeringBehaviours.SimpleAgent ( manager.Door.position ) ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , manager.Door.position ) < 0.05f )
                    _walkingProgress = WalkingProgress.Door ;
            }
            else if ( _walkingProgress == WalkingProgress.Door )
            {
                Velocity += SteeringBehaviours.Seek ( this , new SteeringBehaviours.SimpleAgent ( manager.Outside.position ) ) ;
                Position += Velocity * Time.deltaTime ;

                if ( Vector3.Distance ( transform.position , manager.Outside.position ) < 0.05f )
                    _walkingProgress = WalkingProgress.Outside ;
            }
            else if ( _walkingProgress == WalkingProgress.Outside )
            {
                Destroy ( this.gameObject ) ;
            }
        }
    }
    #endregion // -------------------------------------------------------------------------------------

    private void SwitchState ( State newState )
    {
        CurrentState = newState ;

        switch ( newState )
        {
            case State.WalkingIn :
                _walkingProgress = WalkingProgress.Outside ;
                break ;

            case State.Seated :
                _attendanceWait = attendTime ;
                canvas.enabled = true ;
                CanCharge = true ;
                break ;

            case State.Attended :
                GameManager.Instance.RegisterPositive () ;
                break ;

            case State.WalkingOut :
                _walkingProgress = WalkingProgress.Seat ;
                CanCharge = false;
                break ;
        }
    }

    #region PUBLIC API -----------------------------------------------------------
    public void Attend ()
    {
        SwitchState ( State.Attended ) ;
    }
    #endregion // ----------------------------------------------------------------

    public bool ChargeState()
    {
        return CanCharge ;
    }

    public float timePassenger()
    {
        return _attendanceWait;
    }
}
