using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace MageMods
{
    [DefOf]
    public static class PsychicDamageDefOf
    {
        public static DamageDef PsychicDamage;
        static PsychicDamageDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PsychicDamageDefOf));
        }
    }
}
