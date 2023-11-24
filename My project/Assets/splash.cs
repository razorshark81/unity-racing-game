using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class splash : MonoBehaviour
{
    public GameObject particle;
    GameObject destroy;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (i == 0)
        {
            StartCoroutine(particleon(other));
            i = 1;
        }


    }
    private IEnumerator particleon(Collider other)
    {
        destroy = Instantiate(particle, other.transform.position- new Vector3(0,1,0), Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);

        Destroy(destroy);
        yield return new WaitForSeconds(2);
        i = 0;
    }
}
