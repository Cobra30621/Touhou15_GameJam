using Player;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class ReimuWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float fireCooldown = 0f;

    /// <summary>
    /// The shooter component responsible for firing bullets.
    /// </summary>
    [SerializeField] private Shooter shooter;

    /// <summary>
    /// The number of bullets to fire in a single shot.
    /// </summary>
    public int bulletCount = 3;

    /// <summary>
    /// The rate at which the weapon can fire (in seconds).
    /// </summary>
    public float fireRate = 0.5f;

    /// <summary>
    /// The angle of spread for the bullets when fired.
    /// </summary>
    public float spreadAngle = 15f;

    public int bulletWave = 3;

    [SerializeField] private int currentWave = 0;

    [SerializeField] private float waveCooldown = 0f;

    public float waveRate = 0.5f;

    private bool isShooting;

    /// <summary>
    /// Indicates whether to use the player's direction.
    /// </summary>
    public bool useTargetPosition = true;

    [SerializeField] public float[] bulletSpeed;

    /// <summary>
    /// The custom direction to fire towards.
    /// </summary>
    [ShowIf("@!useTargetPosition")]
    public Vector3 customDirection;

    [ShowIf("@useTargetPosition")]
    public Vector3 targetDirection;

    /// <summary>
    /// Updates the weapon's state every frame, checking if it can fire.
    /// </summary>
    void Update()
    {
        if (isShooting)
        {
            FireBySpeed(bulletSpeed[0]);
            FireBySpeed(bulletSpeed[1]);
            FireBySpeed(bulletSpeed[2]);
            fireCooldown = fireRate;
            isShooting = false;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                var targetPosition = PlayerController.Instance.transform.position;
                targetDirection = targetPosition - shooter.firePoint.position;
                isShooting = true;
            }
        }
    }

    /// <summary>
    /// Fires the weapon, shooting bullets towards the player.
    /// </summary>
    private void FireBySpeed(float singleBulletSpeed)
    {
        // Debug.Log("Fire");

        // ������a��m

        // �w�q�o�g��V
        Vector3 dir;

        // �ھڻݭn��ܵo�g��V
        if (useTargetPosition) // ���]���@�ӥ��L�ܼƥΨӨM�w�O�_�ϥΪ��a��V
        {
            dir = targetDirection;
        }
        else
        {
            dir = customDirection; // ���]���@�� Vector3 �ܼ� customDirection �Ψөw�q�۩w�q��V
        }

        shooter.Fire(bulletCount, spreadAngle, dir, singleBulletSpeed);
        //fireCooldown = fireRate;
    }
}

