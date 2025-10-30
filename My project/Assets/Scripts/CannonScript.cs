using UnityEngine;

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

    private void Update()
    {
        float angle = Mathf.Lerp(minZ, maxZ, Mathf.PingPong(Time.time * speed, 1f));
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Fire()
    {
        if (canFire)
        {
            ToggleCanFire(false);
            GameObject shot = Instantiate(prefab, barrel.position, barrel.rotation);
            shot.tag = ballTag;
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(-barrel.up * force);
        }
    }

    public void ToggleCanFire(bool toggle)
    {
        canFire = toggle;
    }
}
