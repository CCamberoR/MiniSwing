using UnityEngine;

public class Dropper : MonoBehaviour{
    
    [SerializeField] private GameObject holePos;
    public float MaxHoleDropOffset;
    private float stayTimer = 0;
    public float MaxstayTime;
    private bool hasDropped = false;


    void OnTriggerStay(Collider other){
        if (other.enabled && other.CompareTag("Ball")){
            stayTimer += Time.deltaTime;
            Ball putter=other.GetComponent<Ball>();
            if (putter){
                putter.IsAtHole = true;
            }

            Vector3 ballXYPos = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            Vector3 holeXYPos = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            if (Mathf.Abs(ballXYPos.x - holeXYPos.x) < MaxHoleDropOffset &&
                Mathf.Abs(ballXYPos.y - holeXYPos.y) < MaxHoleDropOffset &&
                (other.attachedRigidbody.velocity.magnitude < 1 || stayTimer >= MaxstayTime)){
                if (!hasDropped){
                    hasDropped = true;
                    other.transform.position = holePos.transform.position;
                    other.attachedRigidbody.velocity = Vector3.zero;
                    putter.SetDummyBall(true);
                    StartCoroutine(Game.Instance.FinishLevel());
                }

            }
        }
    }
    void Start(){
        
    }

    void Update(){
        
    }
}
