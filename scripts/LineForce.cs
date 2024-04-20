using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private bool isIdle;
    private bool isAiming;
    private Rigidbody rigidbod;
    [SerializeField] private float stopVelocity = 0.15f;
    private float shotPower=2;
    public Vector3[] positions;

    private void Awake()
    {
        rigidbod = GetComponent<Rigidbody>();
        isIdle = true;
        isAiming = false;
        lineRenderer.enabled = false;
    }


    private void FixedUpdate()
    {
        if (rigidbod.velocity.magnitude < stopVelocity){
            Stop();
        }
        
        ProcessAim();
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

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        Vector3 direction=-(horizontalWorldPoint-transform.position).normalized;
        float strength = 10*Vector3.Distance(transform.position, horizontalWorldPoint);
        rigidbod.AddForce(direction*strength*shotPower);
    }

    private void DrawLine(Vector3 worldPoint){
        // Calculate the length of the line
        float lineLength = 0;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++){
            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        // Adjust shotPower based on the length of the line
        shotPower = lineLength;

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
