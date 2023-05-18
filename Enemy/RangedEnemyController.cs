using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : BasicEnemyController
{
    public RangedWeaponInfo weaponInfo;

    private void DoAction()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, navTarget.position - transform.position, out hit, weaponInfo.range);
        targetDistance = Vector3.Distance(transform.position, navTarget.position);
        if (((targetDistance < 3.5f && hit.transform == navTarget) || fleeTime > -.1f))
        {
            //flee
            if (nav.enabled == false)
                nav.enabled = true;
            if (fleeTime > -.1f)
                fleeTime -= Time.deltaTime;
            else
                fleeTime = 2;
            nav.SetDestination(transform.position - navTarget.position);
        }
        else if ((targetDistance < weaponInfo.range) && hit.transform == navTarget)
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
}
