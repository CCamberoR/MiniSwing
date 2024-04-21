using UnityEngine;

public class Vortex : MonoBehaviour{

    private Collider vortexCollider;

    public float VortexForce;

    private void Awake(){
        vortexCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other){
        if (other.CompareTag("Player")){
            Vector3 normal = other.transform.position - vortexCollider.bounds.center;
            other.attachedRigidbody.AddForce(normal * VortexForce);
        }
    }
}
