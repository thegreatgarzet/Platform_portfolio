using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionCode.ColorPalettes
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ColorPaletteSwapper))]
    public class ColorPaletteSwapperCycle : MonoBehaviour
    {
        public KeyCode swapKey = KeyCode.N;
        public ColorPaletteSwapper swapper;
        public List<ColorPalette> palettes;
        private int _palletIndex = -1;
        public int palletNumber;
        public ControleArmas idColor;
        private void Start()
        {
            idColor = FindObjectOfType<ControleArmas>();
        }
        private void Reset()
        {
            swapper = GetComponent<ColorPaletteSwapper>();
        }

        private void Update()
        {
            SwapPalette(idColor.idColor);
            
            if (Input.GetKeyDown(swapKey)) SwapPalette(palletNumber);
        }

        private void SwapPalette(int palletNumber)
        {
            if (palettes.Count == 0) return;

            //_palletIndex = (_palletIndex + 1) % palettes.Length;
            swapper.SwitchPalette(palettes[palletNumber]);
        }
    }
}