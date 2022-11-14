using RimWorld;
using Verse;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using static MageMods.MeleePsychicDamage;

namespace MageMods
{
    [StaticConstructorOnStartup]
    public static class MeleePsychicDamage
    {
        
        static MeleePsychicDamage()
        {
            var harmony = new Harmony("ac.mwsad.patch");
            harmony.PatchAll();

            
        }

        public static float GetPsychicSensitivity(Pawn pawn)
        {
            float test = pawn.GetStatValue(StatDefOf.PsychicSensitivity);
            return test;
        }
        
    }
    public class PsychicModExtension : DefModExtension
    {
        public float PsychicSens;
    }

    [HarmonyPatch(typeof(VerbProperties), "AdjustedMeleeDamageAmount", new Type[]
    {
        typeof(Tool),
        typeof(Pawn),
        typeof(Thing),
        typeof(HediffComp_VerbGiver)
    })]

    public static class AdjustedMeleeDamageAmount_Patch
    {
        public const float damageMin = 0f;
        public static Dictionary<Thing, bool> cachedWeapons = new Dictionary<Thing, bool>();

        public static void Postfix(ref float __result, Tool tool, Pawn attacker, Thing equipment)
        {
            if (attacker.equipment.Primary != null)
            {
                if (cachedWeapons.ContainsKey(attacker.equipment.Primary))
                {
                    if (cachedWeapons[attacker.equipment.Primary])
                    {
                        __result = __result * Mathf.Sqrt(MeleePsychicDamage.GetPsychicSensitivity(attacker));
                    }
                    return;
                }

                for (int i = 0; i < attacker.equipment.Primary.def.weaponClasses.Count; i++)
                {
                    if (Convert.ToString(attacker.equipment.Primary.def.weaponClasses[i]) == "MeleePsychicSens")
                    {
                        __result = __result * MeleePsychicDamage.GetPsychicSensitivity(attacker);
                        cachedWeapons[attacker.equipment.Primary] = true;
                        Log.Message(Convert.ToString(attacker.GetStatValue(StatDefOf.PsychicSensitivity)));
                        return;
                    }
                }

                cachedWeapons[attacker.equipment.Primary] = false;
            }
        }
    }
    [HarmonyPatch(typeof(ArmorUtility), "ApplyArmor")]
    public static class ApplyArmor_Patch
    {
        public static void Prefix(float armorPenetration, float armorRating, Thing armorThing, Pawn pawn)
        {
            if (armorThing != null)
            {
                if (armorThing.def.HasModExtension<PsychicModExtension>() == true)
                {
                    Log.Message("i worked " + armorRating);
                    armorRating = armorRating * pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                    Log.Message("i worked with this change " + armorRating);
                }

            }
        }
    }

}