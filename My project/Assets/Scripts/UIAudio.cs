using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip uiButton;

    public void PlaySound()
    {
        AS.PlayOneShot(uiButton, 1f);
    }
}
