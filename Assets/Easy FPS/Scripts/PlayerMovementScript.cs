using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementScript : MonoBehaviour {

    Rigidbody rb;

    public float currentSpeed;
    [HideInInspector] public Transform cameraMain;
    public float jumpForce = 500;
    [HideInInspector] public Vector3 cameraPosition;

    void Awake(){
        rb = GetComponent<Rigidbody>();

        // ✅ FIX PHYSICS
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.drag = 1f;
        rb.angularDrag = 0.05f;
        rb.freezeRotation = true;

        cameraMain = transform.Find("Main Camera");
        bulletSpawn = cameraMain.Find("BulletSpawn");
        ignoreLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private Vector3 slowdownV;
    private Vector2 horizontalMovement;

    void FixedUpdate(){
        RaycastForMeleeAttacks();

        // ✅ กันลอย
        rb.AddForce(Vector3.down * 20f);

        PlayerMovementLogic();
    }

    void PlayerMovementLogic(){

        // ✅ FIX velocity
        currentSpeed = rb.velocity.magnitude;
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);

        if (horizontalMovement.magnitude > maxSpeed){
            horizontalMovement = horizontalMovement.normalized * maxSpeed;
        }

        rb.velocity = new Vector3(
            horizontalMovement.x,
            rb.velocity.y,
            horizontalMovement.y
        );

        if (grounded){
            rb.velocity = Vector3.SmoothDamp(
                rb.velocity,
                new Vector3(0, rb.velocity.y, 0),
                ref slowdownV,
                deaccelerationSpeed
            );
        }

        float control = grounded ? 1f : 0.5f;

        rb.AddRelativeForce(
            Input.GetAxis("Horizontal") * accelerationSpeed * control * Time.deltaTime,
            0,
            Input.GetAxis("Vertical") * accelerationSpeed * control * Time.deltaTime
        );

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            deaccelerationSpeed = 0.5f;
        else
            deaccelerationSpeed = 0.1f;
    }

    void Jumping(){
        if (Input.GetKeyDown(KeyCode.Space) && grounded){
            
            // ✅ FIX jump
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (_jumpSound) _jumpSound.Play();
            _walkSound.Stop();
            _runSound.Stop();
        }
    }

    void Update(){
        Jumping();
        Crouching();
        WalkingSound();
    }

    void WalkingSound(){
        if (_walkSound && _runSound){
            if (RayCastGrounded()){
                if (currentSpeed > 1){
                    if (maxSpeed == 3){
                        if (!_walkSound.isPlaying){
                            _walkSound.Play();
                            _runSound.Stop();
                        }
                    }
                    else if (maxSpeed == 5){
                        if (!_runSound.isPlaying){
                            _walkSound.Stop();
                            _runSound.Play();
                        }
                    }
                }
                else{
                    _walkSound.Stop();
                    _runSound.Stop();
                }
            }
            else{
                _walkSound.Stop();
                _runSound.Stop();
            }
        }
    }

    private bool RayCastGrounded(){
        RaycastHit groundedInfo;
        if(Physics.Raycast(transform.position, Vector3.down, out groundedInfo, 1.2f, ~ignoreLayer)){
            return true;
        }
        return false;
    }

    void Crouching(){
        if(Input.GetKey(KeyCode.C)){
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,0.6f,1), Time.deltaTime * 15);
        }
        else{
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,1,1), Time.deltaTime * 15);
        }
    }

    public int maxSpeed = 5;
    public float deaccelerationSpeed = 15.0f;
    public float accelerationSpeed = 50000.0f;

    public bool grounded;

    void OnCollisionStay(Collision other){
        foreach(ContactPoint contact in other.contacts){
            if(Vector3.Angle(contact.normal,Vector3.up) < 60){
                grounded = true;
            }
        }
    }

    void OnCollisionExit (){
        grounded = false;
    }

    // ================== MELEE SYSTEM (UNCHANGED) ==================

    RaycastHit hitInfo;
    private float meleeAttack_cooldown;
    private string currentWeapo;

    private LayerMask ignoreLayer;
    Ray ray1, ray2, ray3, ray4, ray5, ray6, ray7, ray8, ray9;

    private float rayDetectorMeeleSpace = 0.15f;
    private float offsetStart = 0.05f;

    public Transform bulletSpawn;

    public bool been_to_meele_anim = false;

    private void RaycastForMeleeAttacks(){

        if (meleeAttack_cooldown > -5)
            meleeAttack_cooldown -= Time.deltaTime;

        if (GetComponent<GunInventory>().currentGun){
            if (GetComponent<GunInventory>().currentGun.GetComponent<GunScript>())
                currentWeapo = "gun";
        }

        ray1 = new Ray (bulletSpawn.position + (bulletSpawn.right*offsetStart), bulletSpawn.forward);
        ray2 = new Ray (bulletSpawn.position - (bulletSpawn.right*offsetStart), bulletSpawn.forward);
        ray3 = new Ray (bulletSpawn.position, bulletSpawn.forward);

        if (GetComponent<GunInventory>().currentGun){
            if (GetComponent<GunInventory>().currentGun.GetComponent<GunScript>().meeleAttack == true && !been_to_meele_anim){
                been_to_meele_anim = true;
                StartCoroutine("MeeleAttackWeaponHit");
            }
        }
    }

    IEnumerator MeeleAttackWeaponHit(){
        if (Physics.Raycast(ray3, out hitInfo, 2f, ~ignoreLayer)){
            if (hitInfo.transform.tag=="Dummie"){
                InstantiateBlood(hitInfo,false);
            }
        }
        yield return null;
    }

    public GameObject bloodEffect;

    void InstantiateBlood (RaycastHit _hitPos,bool swordHitWithGunOrNot){
        if (currentWeapo == "gun"){
            if (_hitSound) _hitSound.Play();

            if (!swordHitWithGunOrNot && bloodEffect){
                Instantiate(bloodEffect, _hitPos.point, Quaternion.identity);
            }
        }
    }

    [Header("SOUNDS")]
    public AudioSource _jumpSound;
    public AudioSource _freakingZombiesSound;
    public AudioSource _hitSound;
    public AudioSource _walkSound;
    public AudioSource _runSound;
}