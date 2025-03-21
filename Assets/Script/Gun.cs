using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1.5f)]
    private float fireRate;
    [SerializeField]
    [Range(1, 30)]
    private int pelor;
    bool lagiReload;
    float timer;
    GameObject holdFlash;
    public GameObject muzzleSpawn;
    public GameObject[] muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                if (pelor == 0 && !lagiReload)
                {
                    lagiReload = true;
                    StartCoroutine(Reload()); ;
                }
                else if (pelor <= 30 && !lagiReload)
                {
                    pelor -= 1;
                    timer = 0f;
                    FireGun();
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && pelor < 30)
            {
                lagiReload = true ;
                StartCoroutine(Reload());
            }
        }
    }

    private void FireGun()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            int randomNumberForMuzze1Flash = Random.Range(0, 5);
            holdFlash = Instantiate(muzzleFlash[randomNumberForMuzze1Flash], muzzleSpawn.transform.position,
                 muzzleSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzleSpawn.transform;
            Destroy(holdFlash, 0.1f);
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        pelor = 30;
        lagiReload = false;
    }
}
