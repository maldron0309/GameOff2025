using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameOff2025.Assets.Scripts.UI.HeroSelection
{
    public class HeroInfoModel
    {
        public string heroName { get; private set; }
        public Image heroIcon { get; private set; }
        public string heroDescription { get; private set; }
        public bool isRecruited { get; private set; }
        public GameObject heroPrefab { get; private set; }
        public HeroInfoModel(string name, Image icon, string description, GameObject prefab = null)
        {
            heroName = name;
            heroIcon = icon;
            heroDescription = description;
            isRecruited = false;
            heroPrefab = prefab;
        }

        public void Recruit()
        {
            isRecruited = true;
        }

        public void Dismiss()
        {
            isRecruited = false;
        }
    }
}