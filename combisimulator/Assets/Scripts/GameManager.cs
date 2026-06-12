using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //  SINGLETON
    public static GameManager Instance { get ; private set ; }

    //  REFERENCES
    private PassangersCrowdManager crowdManager ;

    [SerializeField] private float passangerSpawnInterval = 5f ;
    [SerializeField] private float passangerWaveInterval = 0.5f ;

    [SerializeField] private int minPassangerWave = 1 ;
    [SerializeField] private int maxPassangerWave = 4 ;


    //  STATE VARIABLES
    private float _sinceLastSpawn , _waveTimer ;
    private int currentWavePending ;


    private void Awake ()
    {
        if ( Instance != null ) Destroy ( this.gameObject ) ;
        else Instance = this ;
    }

    private void Start ()
    {
        crowdManager = FindAnyObjectByType < PassangersCrowdManager > () ;
    }

    private void Update ()
    {
        _sinceLastSpawn += Time.deltaTime ; // clock

        //  time for spawning
        if ( _sinceLastSpawn > passangerSpawnInterval )
        {
            _sinceLastSpawn -= passangerSpawnInterval ;
            currentWavePending = UnityEngine.Random.Range ( minPassangerWave , maxPassangerWave + 1 ) ;
            _waveTimer = 0f ;
        }

        //  if waves are pending
        if ( currentWavePending > 0 )
        {
            _waveTimer -= Time.deltaTime ;
            
            if ( _waveTimer <= 0 )
            {
                _waveTimer += passangerWaveInterval ;
                crowdManager.SpawnPassanger () ;
                currentWavePending-- ;
            }
        }
    }


    #region PUBLIC API -----------------------------------------------
    public void RegisterPositive ()
    {

    }
    public void RegisterNegative ()
    {

    }
    #endregion // ----------------------------------------------------
}