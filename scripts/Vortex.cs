using UnityEngine;

public class Vortex : MonoBehaviour{

    private Collider vortexCollider;

    public float VortexForce=10;

    private void Awake(){
        vortexCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other){
        if (other.CompareTag("Player")){
            Vector3 normal = vortexCollider.bounds.center - other.transform.position;
            other.attachedRigidbody.AddForce(normal * VortexForce);
        }
    }
}
