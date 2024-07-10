using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EVP
{
    public class CarCrashSound : MonoBehaviour
    {
        public bool EnteredCarCrash = false;
        public string Enteredcoll;
        public AudioSource CrashSound;
        //public FourWheelGearInput VehicleScript;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "ColliderSound" || other.gameObject.name == "Terrain")
            {
                EnteredCarCrash = true;
                CrashSound.enabled = true;
            }
        }
        void OnTriggerStay(Collider other)
        {
            Enteredcoll = other.gameObject.name;

            if ((other.gameObject.name == "ColliderSound" || other.gameObject.name == "Terrain") && !EnteredCarCrash)
            {
                EnteredCarCrash = true;
                CrashSound.enabled = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "ColliderSound" || other.gameObject.name == "Terrain")
            {
                EnteredCarCrash = false;
                CrashSound.enabled = false;
            }
        }


    }
}
