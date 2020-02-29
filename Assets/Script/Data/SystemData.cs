using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SystemData", menuName="Data/SystemData")]
public class SystemData : ScriptableObject {

	[Header("Tank")]
	public int tankHealthValue;
	public int tankBulletSpeed;
	public int tankMoveSpeed;
    
	[Header("Enemy")]
	public int enemyHealthValue;
	public int enemyBulletSpeed;
	public int enemyMoveSpeed;
	public int enemyAttackDistance;
	public float enemyFireRate;
	public float enemyCreateRate;
    public int enemyUplimit;

    [Header("Turret")]
    public int turretHealthValue;
    public float turretFireRate;
	public int turretSpeed;

	[Header("Plane")]
	public int planeHealthValue;
    public int planeSpeed;
    public float planeUpForce;
	public float planeTailUpForce;
	public float plane_W_LiftSpeed;
	public float plane_S_LiftSpeed;
	public float plane_A_LiftSpeed;
	public float plane_D_LiftSpeed;

    public void Easy() {
        enemyHealthValue = 1;
        enemyBulletSpeed = 20;
        enemyMoveSpeed = 5;
        enemyAttackDistance = 30;
        enemyFireRate = 1;
        enemyCreateRate = 3;
        enemyUplimit = 5;
    }
    public void Normal() {
        enemyHealthValue = 1;
        enemyBulletSpeed = 25;
        enemyMoveSpeed = 10;
        enemyAttackDistance = 40;
        enemyFireRate = 0.5f;
        enemyCreateRate = 2;
        enemyUplimit = 10;
    }
    public void Hard() {
        enemyHealthValue = 2;
        enemyBulletSpeed = 30;
        enemyMoveSpeed = 20;
        enemyAttackDistance = 50;
        enemyFireRate = 0.1f;
        enemyCreateRate = 1;
        enemyUplimit = 20;
    }
}

