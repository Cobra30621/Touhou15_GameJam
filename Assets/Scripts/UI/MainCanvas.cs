using UnityEngine;
using Core;
using Fungus;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        public static MainCanvas Instance;
        public TMPro.TextMeshProUGUI spellCardText;
        public TMPro.TextMeshProUGUI clock;


        [SerializeField] private Canvas canvas;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void EnableCanvas(bool enable)
        {
            canvas.enabled = enable;
        }

        public IEnumerator ShowSpellCard(string spellcardname)
        {
            spellCardText.enabled = true;
            spellCardText.text = spellcardname;
            yield return new WaitForSeconds(5f);
            spellCardText.enabled = false;
        }

        public void DisplayClock(bool isShow)
        {
            if (isShow)
            {
                clock.enabled = true;
            }
            else
            {
                clock.enabled = false;

            }
        }
    }
}