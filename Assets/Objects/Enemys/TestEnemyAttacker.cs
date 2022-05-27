using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EditorScripts;

namespace RampageCars
{
    public class TestEnemyAttacker : MonoEventReceiver
    {
        [SerializeField]
        SerializeInterface<ISubscribeable<TriggerReceiveInfo>> collisionReceiver;
        Coroutine attackCo;
        [SerializeField]
        float attackDamage = 1;
        [SerializeField]
        float impulseScale = 2;

        [SerializeField]
        [Tooltip("最大回転角速度")]
        float maxDeltaDegree = 5.0f;

        [SerializeField]
        [Tooltip("攻撃を始める許容角度")]
        float startAttackDegree = 10.0f;

        [SerializeField]
        [Tooltip("攻撃の準備時間")]
        float attackWaitTime = 0.8f;

        [SerializeField]
        float attackPower = 100.0f;
        [SerializeField]
        float attackPosY = 5.0f;

        [SerializeField]
        ConstraintOnOtherObject constraint;

        bool isAttacking = false;

        private void Start()
        {
            OnDestroyed += collisionReceiver.Interface.Subscribe(Collision);
        }

        void Collision(TriggerReceiveInfo info)
        {
            if (constraint.IsConstant)
            {
                return;
            }
            if (attackCo is not null || info.body.gameObject.GetComponent<IPlayerTag>() is null)
            {
                return;
            }
            attackCo = StartCoroutine(Attack(info.body.transform));
        }

        IEnumerator Attack(Transform target)
        {
            var enemyToPlayer = target.transform.position - transform.position;

            var dot = Vector3.Dot(enemyToPlayer, transform.right);

            var dotSign = dot > 0 ? -1 : 1;

            var addQ = Quaternion.AngleAxis(90 * dotSign, Vector3.up);

            float time = 0;

            bool isNext = false;

            //徐々にプレイヤーのほうを向く
            while (true)
            {
                Vector3 direction = target.transform.position - transform.position;
                direction.y = 0;

                var targetQ = Quaternion.LookRotation(direction, Vector3.up) * addQ;

                var diff = Quaternion.Angle(targetQ, transform.rotation);

                if (!isNext)
                {
                    isNext = diff < startAttackDegree;
                }
                if (isNext)
                {
                    var add = Quaternion.AngleAxis(dotSign * -30, transform.forward);
                    targetQ *= add;
                    time += Time.deltaTime;
                    if (time > attackWaitTime)
                    {
                        break;
                    }
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQ, maxDeltaDegree);

                yield return null;
            }

            var rb = GetComponent<Rigidbody>();
            var forcedPos = transform.position + (transform.up * attackPosY);
            var force = transform.right;
            force.Normalize();

            rb.AddForceAtPosition(-dotSign * attackPower * force, forcedPos, ForceMode.Impulse);

            isAttacking = true;

            yield return new WaitForSeconds(1.0f);
            isAttacking = false;

            attackCo = null;
        }

        private void Update()
        {
            if (attackCo != null)
            {
                if (constraint.IsConstant)
                {
                    StopCoroutine(attackCo);
                    attackCo = null;
                    isAttacking = false;
                }
                return;
            }
            transform.LookAt(transform.forward, Vector3.up);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isAttacking)
            {
                return;
            }
            var com = collision.collider.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (com is IPlayerTag)
            {
                Vector3 direction = collision.collider.transform.position - transform.position;
                com.Publish(new(attackDamage, direction.normalized * impulseScale));
            }
        }
    }
}
