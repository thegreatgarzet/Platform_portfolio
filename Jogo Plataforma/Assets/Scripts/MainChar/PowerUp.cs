using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionCode.ColorPalettes
{
    public class PowerUp : MonoBehaviour
    {
        public ColorPalette color;
        public GameObject weapon;
        public AmmoRefillControl ammoRef;
        public int id, idWeapon, idColor;
    }
}
