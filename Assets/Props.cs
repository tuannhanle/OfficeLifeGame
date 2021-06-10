using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour, ICollidable
{
    
    public void EnterCollided(Vector3 attacker, float force)
    {
        var dir = this.transform.position- attacker;
        GetComponent<Rigidbody>()?.AddForce(new Vector3(dir.x, 1f,dir.z*2f) *force,ForceMode.Impulse);
        GetComponent<Collider>().isTrigger = true;
        StartCoroutine(SelfDestroy());
    }
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        StopCoroutine(SelfDestroy());
        Destroy(this.gameObject);
    }


}
