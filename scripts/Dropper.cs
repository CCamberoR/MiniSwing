using UnityEngine;
using UnityEngine.UI;

public class Dropper : MonoBehaviour
{
    public static Dropper Instance; // Singleton instance

    [SerializeField] private GameObject holePos;
    [SerializeField] private GameObject dummyBall;
    public float MaxHoleDropOffset;
    private float stayTimer = 0;
    public float MaxstayTime;
    private bool hasDropped = false;
    [SerializeField] private Text endLevelText; // UI Text component to display the end level message
    public int hitCount = 0; // Variable to track the number of hits



    void Start()
    {
        dummyBall.SetActive(false); // Make the "DummyBall" invisible
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.enabled && other.CompareTag("Player"))
        {
            stayTimer += Time.deltaTime;

            Vector3 ballXYPos = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            Vector3 holeXYPos = new Vector3(holePos.transform.position.x, 0f, holePos.transform.position.z);
            if (Mathf.Abs(ballXYPos.x - holeXYPos.x) < MaxHoleDropOffset &&
                Mathf.Abs(ballXYPos.z - holeXYPos.z) < MaxHoleDropOffset &&
                (other.attachedRigidbody.velocity.magnitude < 1 || stayTimer >= MaxstayTime))
            {
                if (!hasDropped)
                {
                    hasDropped = true;
                    other.gameObject.SetActive(false); // Make the "Player" ball invisible
                    dummyBall.transform.position = other.transform.position; // Set the "DummyBall" position
                    dummyBall.SetActive(true); // Make the "DummyBall" visible
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dummyBall.SetActive(false); // Make the "DummyBall" invisible
        }
    }


    void Update(){
        if (dummyBall.activeSelf && dummyBall.transform.position.y < holePos.transform.position.y){
             Debug.Log("Partida finalizada con " + hitCount + " golpes");
        }
    }

}