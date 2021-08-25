using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject cannonHead; 
    [SerializeField] private GameObject cannonTip;
    [SerializeField] private float shootingCoolDown = 3.0f;
    [SerializeField] private float laserPower = 100f;

    private bool isPlayerInRange;
    private GameObject player;
    private float timeLeftToShoot;
    private LineRenderer cannonLaser;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cannonLaser = GetComponent<LineRenderer>();
        cannonLaser.sharedMaterial.color = Color.green;
        cannonLaser.enabled = false;
        timeLeftToShoot = shootingCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange)
        {
            cannonHead.transform.LookAt(player.transform);
            cannonLaser.SetPosition(0, cannonTip.transform.position);
            cannonLaser.SetPosition(1, player.transform.position);
            timeLeftToShoot -= Time.deltaTime;
        }

        if (timeLeftToShoot <= shootingCoolDown * 0.5)
        {
            cannonLaser.sharedMaterial.color = Color.red;
        }

        if (timeLeftToShoot <= 0)
        {
            Vector3 directionToPushBack = player.transform.position - cannonTip.transform.position;
            player.GetComponent<Rigidbody>().AddForce(directionToPushBack * laserPower, ForceMode.Impulse);
            timeLeftToShoot = shootingCoolDown * 0.6f;
            cannonLaser.sharedMaterial.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            cannonLaser.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            cannonLaser.enabled = false;
            timeLeftToShoot = shootingCoolDown;
            cannonLaser.sharedMaterial.color = Color.green;
        }
    }
}
