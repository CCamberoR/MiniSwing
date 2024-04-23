using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private bool isIdle;
    private bool isAiming;
    private Rigidbody rigidbod;
    [SerializeField] private float stopVelocity = 0.15f;
    private float shotPower=10;
    public Vector3[] positions;
    public int powerMultiplier = 35;
    public float maxShotPower = 100; // Set this to your desired maximum shot power

    public AudioSource audioSource;
    public AudioClip ballHitSound1;
    public AudioClip ballHitSound2;
    public AudioClip ballHitSound3;
    public AudioClip ballHitSound4;
    public AudioClip ballHitSound5;

    private void Awake()
    {
        rigidbod = GetComponent<Rigidbody>();
        isIdle = true;
        isAiming = false;
        lineRenderer.enabled = false;
    }


    private void FixedUpdate(){
        if (rigidbod.velocity.magnitude < stopVelocity){
            Stop();
        }
        
        ProcessAim();
        
        SlowRigidbody(rigidbod, 0.001f);

        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnMouseDown()
    {
        if(isIdle){
            isAiming = true;
        }
    }

    private void ProcessAim(){
        if(!isAiming || !isIdle){
                return;
        }
            
        Vector3? worldPoint = CastMousePoint();
        
        if(!worldPoint.HasValue){
            return;
        }
        
        DrawLine(worldPoint.Value);

        if(Input.GetMouseButtonUp(0)){
            Shoot(worldPoint.Value);
        }
    }

    private void Shoot (Vector3 worldPoint){
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 direction = (transform.position - worldPoint).normalized;
        float strength = Vector3.Distance(transform.position, worldPoint);
        rigidbod.AddForce(direction * strength * shotPower);
        Dropper.Instance.hitCount++;

        int randomSound = Random.Range(1, 6);
        switch (randomSound){
            case 1:
                audioSource.PlayOneShot(ballHitSound1);
                break;
            case 2:
                audioSource.PlayOneShot(ballHitSound2);
                break;
            case 3:
                audioSource.PlayOneShot(ballHitSound3);
                break;
            case 4:
                audioSource.PlayOneShot(ballHitSound4);
                break;
            case 5:
                audioSource.PlayOneShot(ballHitSound5);
                break;
        }
    }

    private void SlowRigidbody(Rigidbody rigidbody, float slowAmount){
        rigidbod.velocity = rigidbod.velocity.normalized * (rigidbody.velocity.magnitude - slowAmount);
    }

    private void DrawLine(Vector3 worldPoint){
        // Calculate the length of the line
        float lineLength = 0;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++){
            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        // Adjust shotPower based on the length of the line
        shotPower = powerMultiplier*lineLength;

        // Limit shotPower to a maximum value
        shotPower = Mathf.Min(shotPower, maxShotPower);

        Vector3[] positions = {transform.position, worldPoint};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop(){
        rigidbod.velocity = Vector3.zero;
        rigidbod.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private Vector3? CastMousePoint(){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        if(Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity)){
            return hit.point;
        }
        else{
            return null;
        }
    }
}
