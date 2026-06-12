using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward ;
    }
}
