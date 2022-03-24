using EditorScripts;
using UnityEngine;


namespace RampageCars
{
    class Player : MonoBehaviour, IPlayer
    {


        [SerializeField]
        float speed = 1;
        [SerializeField]
        float boostPower = 100;
        [SerializeField]
        float jumpPower = 10;
        [SerializeField]
        float steeringMax = 10;
        [SerializeField]
        float steeringThreshold = 10;

        Rigidbody rb;


        float steerAngle;
        float motorTorque;

        [SerializeField]
        float drag = 1f;
        [SerializeField]
        Vector3 dragFactor = new Vector3(1, 1, 1);

        SpeedMeasure speedMeasure;

        Vector3 boost;

        [SerializeField, NotNull]
        GroundChecker groundChecker;

        [SerializeField]
        CollisionDamageInfo collisionDamageInfo;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            speedMeasure = GetComponent<SpeedMeasure>();
        }
        private void Update()
        {
        }

        private void FixedUpdate()
        {
            //重力
            rb.AddForce(Physics.gravity, ForceMode.Acceleration);

            //ブースト
            rb.AddForce((boost * boostPower), ForceMode.Force);

            if (groundChecker.IsGrounded)
            {
                //地上回転
                var absF = Mathf.Abs(speedMeasure.SpeedForward);
                var st = Mathf.Min(1, absF) * Mathf.Min(1, steeringThreshold / absF);
                rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(steerAngle * steeringMax * st, Vector3.up));

                //移動
                rb.AddForce(motorTorque * speed * transform.forward, ForceMode.Force);

                //Drag
                var localV = transform.InverseTransformDirection(rb.velocity);
                float forceX = -drag * dragFactor.x * localV.x;
                float forceY = -drag * dragFactor.y * localV.y;
                float forceZ = -drag * dragFactor.z * localV.z;
                rb.AddRelativeForce(new Vector3(forceX, forceY, forceZ));
            }
            else
            {
                //空中回転
                rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(steerAngle * steeringMax, Vector3.up));

            }
        }
        public void SetMotorTorque(float motorTorque)
        {
            this.motorTorque = motorTorque;
        }

        public void SetSteerAngle(float steerAngle)
        {
            this.steerAngle = steerAngle;
        }

        public void Boost(Vector3 v)
        {
            boost = v;
        }

        public void Jamp()
        {
            if (groundChecker.IsGrounded)
            {
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            collisionDamageInfo.Collision = collision;

            var damageable = collision.gameObject.GetComponent<IPublishable<ICollisionDamageInfo>>();
            if (damageable is not null and IEnemyTag)
            {
                damageable.Publish(collisionDamageInfo);
            }
        }
    }
}
