using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    public GameObject[] exploder;
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.impulse);
        if (collision.impulse.x+collision.impulse.z>6500 || collision.impulse.x + collision.impulse.z < -6500)
        {
            StartCoroutine(courutine());
        }
    }
    public IEnumerator courutine()
    {
        for (int i = 0; i <exploder.Length; i++)
            exploder[i].SetActive(true);
        yield return new WaitForSeconds(1.6f);
        Destroy(gameObject);
    }
}