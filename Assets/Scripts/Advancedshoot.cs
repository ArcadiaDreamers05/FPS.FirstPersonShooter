using UnityEngine;

public class AdvancedShoot : MonoBehaviour
{
    [Header("Arma")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 8f;

    [Header("Riferimenti")]
    public Camera fpsCam;
    public GameObject impactEffectPrefab;

    private float nextTimeToFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Danno al nemico
            Health h = hit.transform.GetComponent<Health>();
            if (h != null) h.TakeDamage(damage);

            // Effetto impatto
            if (impactEffectPrefab != null)
            {
                Vector3 spawnPos = hit.point + hit.normal * 0.01f;
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                GameObject fx = Instantiate(impactEffectPrefab, spawnPos, rot);
                Destroy(fx, 2f);
            }
        }
    }
}