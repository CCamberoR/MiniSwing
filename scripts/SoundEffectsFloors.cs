using UnityEngine;

public class SoundEffectsFloors : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip greenFloorSound;
    public AudioClip brownFloorSound;
    public AudioClip iceFloorSound;
    public Rigidbody rb;

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    public void PlayGreenFloorSound(){
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(greenFloorSound);
    }

    public void PlayBrownFloorSound(){
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(brownFloorSound);
    }

    public void PlayIceFloorSound(){
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(iceFloorSound);
    }

    public void PlaySoundEffect(string floorType){
        switch(floorType){
            case "green":
                PlayGreenFloorSound();
                break;
            case "grey":
                PlayBrownFloorSound();
                break;
            case "ice":
                PlayIceFloorSound();
                break;
        }
    }

    // Detect collision with another object
    void OnCollisionStay(Collision collision){
        // Check the tag of the object the player collided with
        if (rb.velocity != Vector3.zero){  // Check if the ball is moving
            if (collision.gameObject.tag == "GreenFloor"){
                PlayGreenFloorSound();
            }
            else if (collision.gameObject.tag == "BrownFloor"){
                PlayBrownFloorSound();
            }
            else if (collision.gameObject.tag == "IceFloor"){
                PlayIceFloorSound();
            }
        }
    }
}