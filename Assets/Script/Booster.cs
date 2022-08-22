using UnityEngine;

public class Booster : MonoBehaviour
{
    [Tooltip("To  change the boost direction, use a 1 or 0 in each x,y,z. So far forwards, use(0,0,1)")]
    public Vector3 boostDirection = new Vector3(0, 1, 0);
    public float boostPower = 250;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(boostDirection * boostPower);
        }
    }
}
