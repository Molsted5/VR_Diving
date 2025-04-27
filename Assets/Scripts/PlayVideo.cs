using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public GameObject helpCanvas;
    public GameObject videoPanel;
    public GameObject handPanel;
    public Image handImage;
    public Sprite[] handSprites;
    public VideoClip[] videoClips;
    public double skipForwardsTime;
    public double skipBackwardsTime;

    void Start() 
    {
        videoPanel.SetActive( false );
        handPanel.SetActive( false );
        helpCanvas.SetActive( false );
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }

    public void PlayClip( VideoClip clip ) {
        videoPanel.SetActive( true );
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void PlayClip() {
        videoPlayer.Play();
    }

    public void PauseClip() {
        videoPlayer.Pause();
    }

    public void Stop() {
        videoPlayer.Stop();
        videoPanel.SetActive( false );
    }

    public void ForwardsClip() {
        double newTime = videoPlayer.time + skipForwardsTime;
        videoPlayer.time = newTime <= videoPlayer.clip.length ? newTime : videoPlayer.clip.length;
        videoPlayer.Play();
    }

    public void BackwardsClip() {
        double newTime = videoPlayer.time - skipBackwardsTime;
        videoPlayer.time = newTime >= 0 ? newTime : 0;
        videoPlayer.Play();
    }

    public void NextClip() {
        for( int i = 0; i < videoClips.Length; i++ ) {
            if( videoPlayer.clip == videoClips[i] ) {
                videoPlayer.clip = ( i + 1 < videoClips.Length ) ? videoClips[i + 1] : videoClips[0];
                videoPlayer.Play();
                return;
            }
        }
    }
    
    public void PreviousClip() {
        for( int i = 0; i < videoClips.Length; i++ ) {
            if( videoPlayer.clip == videoClips[i] ) {
                videoPlayer.clip = ( i - 1 ) >= 0 ? videoClips[i - 1] : videoClips[videoClips.Length - 1];
                videoPlayer.Play(); 
                return;
            }
        }
    }

    public void Hand() {
        handImage.sprite = handSprites[0];
    }

    public void NextHand() {
        for( int i = 0; i < handSprites.Length ;i++ ) {
            if( handImage.sprite == handSprites[i] ) {
                handImage.sprite = ( i + 1 ) < handSprites.Length ? handSprites[i + 1] : handSprites[0];
                return;
            }
        }
    }

    public void PreviousHand() {
        for( int i = 0; i < handSprites.Length; i++ ) {
            if( handImage.sprite == handSprites[i] ) {
                handImage.sprite = (i - 1) >= 0 ? handSprites[i - 1] : handSprites[handSprites.Length - 1];
                return;
            }
        }
    }

}
