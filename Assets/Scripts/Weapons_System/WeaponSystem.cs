using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    #region Variables

    [Header("References")]
    public InputManager inputManager;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsDamagable;

    [Header("Gun Stats")]
	public bool automaticFire;
    public int damage, magazineSize, bulletsPerTap;
	private int bulletsLeft, bulletsShot;
	public float spread, range, reloadTime, timeBetweenShooting, timeBetweenShots;

    [Header("Weapon Preferences")]
    public AudioSource audioSource;
    public GameObject muzzleFlash, bulletHole;

    // Basic Booleans
    bool shooting, readyToShoot = true, reloading, m, mouseClicked, aimWeapons;
    // Aimweapon
    public Transform aimWeapon;
    private float aimSpeed=2f;

    #endregion

    #region Basic Functions

    private void Start()
    {
        fpsCam = Camera.main;

        inputManager = transform.root.GetComponent<InputManager>();

        inputManager.onFootActions.Shoot.started += ctx => mouseClicked = true;
		inputManager.onFootActions.Shoot.canceled += ctx => mouseClicked = false;
        

        inputManager.onFootActions.Reload.performed += ctx => WeaponReload();

        ReloadFinnished();
    }

    private void Update()
    {
        if (automaticFire)
        {
            WeaponShoot(mouseClicked);
        }
        else
        {
            WeaponShoot(mouseClicked);
            mouseClicked = false;
        }
    }

    private void WeaponShoot(bool shoot)
	{
        if (Time.timeScale == 0) return;

        shooting = shoot;

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Instantiate(muzzleFlash, attackPoint);
            audioSource.Play();

            readyToShoot = false;

            // Storing Forward Direction
            Vector3 forward = fpsCam.transform.forward;

            // Random Spread Value
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            // Calculating Direction with Spread
            Vector3 direction = forward + new Vector3(x, y, 0);

            Ray ray = new Ray(fpsCam.transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 10);

            RaycastHit rayHit;

            // RayCast
            if (Physics.Raycast(ray, out rayHit, range, whatIsDamagable))
            {
                //Debug.Log($"What we Hit: {rayHit.transform.name}");

                HealthSystem damageObject = rayHit.collider.GetComponent<HealthSystem>();

                if (damageObject)
                { 
                    damageObject.TakeDamage(damage);
                    Instantiate(bulletHole, rayHit.point + rayHit.normal * 0.01f, Quaternion.LookRotation(rayHit.normal), rayHit.transform);
                }
                else
                    Instantiate(bulletHole, rayHit.point + rayHit.normal * 0.01f, Quaternion.LookRotation(rayHit.normal));
            }


            bulletsLeft--;
            //bulletsShot--;

            Invoke("ResetShot", timeBetweenShooting);

            //if (bulletsShot > 0 && bulletsLeft >0)
            //    Invoke("WeaponShoot", timeBetweenShots);
        }
        else
        {
            //Debug.Log("CANNOT SHOOT");
            //Debug.Log($"Ready to shoot: {readyToShoot}");
            //Debug.Log($"Shooting: {shooting}");
            //Debug.Log($"Reloading: {reloading}");
            //Debug.Log($"Bullets Left: {bulletsLeft}");
        }
	}

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void WeaponReload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            Debug.Log("RELOADING");
            reloading = true;
            Invoke("ReloadFinnished", reloadTime);
        }
    }

    private void ReloadFinnished()
    {
        Debug.Log("RELOAD FINNISHED");
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void Aiming()
    {
        if (Input.GetMouseButton(1))
            transform.position = Vector3.MoveTowards(transform.position, aimWeapon.position, aimSpeed*Time.deltaTime);
        
    }


	#endregion
}	
