using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MageMods
{
    internal class CompProperties_AbilityPsychicScream : CompProperties_AbilityEffect
    {
        public float range;

        public float lineWidthEnd;

        public CompProperties_AbilityPsychicScream()
        {
            compClass = typeof(CompAbilityEffect_PsychicScream);
        }
    }
    

}
