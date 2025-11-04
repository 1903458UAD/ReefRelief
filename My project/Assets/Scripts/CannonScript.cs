using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private float speed;
    [SerializeField] private Transform barrel;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool canFire;
    [SerializeField] private float force;
    [SerializeField] private string ballTag;
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip audioFire;
    [SerializeField] private AudioClip audioReload;
    [SerializeField] private TrashGameManager GM;

    private void Update()
    {
        float angle = Mathf.Lerp(minZ, maxZ, Mathf.PingPong(Time.time * speed, 1f));
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Fire()
    {
        if (canFire && GM.time > 0)
        {
            ToggleCanFire(false);
            AS.PlayOneShot(audioFire, 1f);
            StartCoroutine(FireDelay());
        }
    }

    public void ToggleCanFire(bool toggle)
    {
        canFire = toggle;
        if (canFire)
        {
            AS.PlayOneShot(audioReload, 1f);
        }
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject shot = Instantiate(prefab, barrel.position, barrel.rotation);
        shot.tag = ballTag;
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.AddForce(-barrel.up * force);
    }
}
