using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public class WiperPlayerAction : MonoBehaviour, IPlayerAction
    {
        public bool CanChange { get; private set; } = true;

        [SerializeField]
        float attack = 5;
        [SerializeField]
        float distance = 3;

        [SerializeField]
        float impulseScale = 20;
        [SerializeField, NotNull]
        Transform beard;

        [SerializeField]
        Vector3 maxBeardSize = new(3, 1, 1);
        Vector3 beardSize;

        [SerializeField]
        float knockback = 1;

        public void Do()
        {
            CanChange = false;

            beardSize = beard.localScale;
            beard.localScale = maxBeardSize;
        }
        public void Finish()
        {
            var halfSize = new Vector3(
                beard.localScale.x/ beardSize.x,
                beard.localScale.y / beardSize.y,
                beard.localScale.z / beardSize.z)
                /2;

            var pos = beard.position;
            int layerMask = LayerMask.GetMask(new string[] { "Default" });
            var raycasteds = Physics.BoxCastAll(pos, halfSize, beard.forward, beard.rotation, distance, layerMask);

            var rb = GetComponent<Rigidbody>();

            foreach (var hit in raycasteds)
            {
                var damageable = hit.collider.GetComponent<IPublishable<CollisionDamageInfo>>();
                if (damageable is not null and IEnemyTag)
                {
                    damageable.Publish(new(attack, -hit.normal * impulseScale));

                    var knockbackForce = hit.normal * knockback;
                    knockbackForce.y = 0;
                    rb.AddForceAtPosition(knockbackForce, hit.point, ForceMode.Impulse);
                }
            }
            beard.localScale = beardSize;
            CanChange = true;
        }
        private void Update()
        {
            if (CanChange)
            {
                return;
            }
            var halfSize = beard.localScale / 2;
            var pos = beard.position;
            ExtDebug.DrawBoxCastBox(pos, halfSize, beard.rotation, beard.forward, distance , Color.green);
        }

    }
}
