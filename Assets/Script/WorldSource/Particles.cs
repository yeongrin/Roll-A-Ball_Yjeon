using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject particleprefab;

    public void CreateParticles()
    {
        Instantiate(particleprefab, transform.position, transform.rotation);
    }
}
