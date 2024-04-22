using UnityEngine;

public class SoundEffectsWalls : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip redWallHitSound;
    public AudioClip blueWallHitSound;
    public AudioClip brownWallHitSound;

    //The sound effect sounds when the player hits the walls
    public void PlayRedWallHitSound(){
        audioSource.PlayOneShot(redWallHitSound);
    }

    public void PlayBlueWallHitSound(){
        audioSource.PlayOneShot(blueWallHitSound);
    }

    public void PlayBrownWallHitSound(){
        audioSource.PlayOneShot(brownWallHitSound);
    }

    // Detect collision with another object
    void OnCollisionEnter(Collision collision){
        // Check the tag of the object the player collided with
        if (collision.gameObject.tag == "RedWall"){
            PlayRedWallHitSound();
        }
        else if (collision.gameObject.tag == "BlueWall"){
            PlayBlueWallHitSound();
        }
        else if (collision.gameObject.tag == "BrownWall"){
            PlayBrownWallHitSound();
        }
    }
    


}
