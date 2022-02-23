using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    [RequireComponent(typeof(Car))]
    public class PlayerCarController : MonoBehaviour
    {
        Car car;

        void Start()
        {
            car = GetComponent<Car>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
