using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    internal Transform navTarget;

    [SerializeField]
    internal AttackHandler attack;

    public MeleeWeaponInfo weaponInfo;

    internal float targetDistance;
    internal NavMeshAgent nav;
    internal float fleeTime = 0;
    internal float immobilized = 0;
    internal float refireTime = 0;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (refireTime >= 0)
        {
            refireTime -= Time.deltaTime;
        }
        if (immobilized > 0)
        {
            WaitForMovement();
        }
        else
        {
            DoAction();
        }
    }

    private void DoAction()
    {
        if (navTarget == null)
            return;
        
        RaycastHit hit;
        Physics.Raycast(transform.position, navTarget.position - transform.position, out hit, weaponInfo.range);
        targetDistance = Vector3.Distance(transform.position, navTarget.position);

        if ((targetDistance < weaponInfo.range) && hit.transform == navTarget)
        {
            //attack
            if (nav.enabled == true)
                nav.enabled = false;
            if (refireTime <= 0)
            {
                refireTime = weaponInfo.cooldown;
                immobilized = weaponInfo.animationDuration + .25f;
                Attack();
            }
        }
        else
        {
            //approach
            if (nav.enabled == false)
                nav.enabled = true;
            nav.SetDestination(navTarget.position);
        }
    }

    private void Attack()
    {
        attack.Attack(weaponInfo, transform.gameObject);
    }

    private void WaitForMovement()
    {
        if (nav.enabled == true)
            nav.enabled = false;
        immobilized -= Time.deltaTime;
        if (refireTime > 0)
        {
            transform.LookAt(navTarget);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    public void SetTarget(Transform t)
    {
        navTarget = t;
    }
}
