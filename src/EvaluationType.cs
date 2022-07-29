using System;

namespace AntonWCodeExercise
{
   /// <summary>
   /// Determines whether the evaluation of the collection of TerrainPoints
   /// 'fails fast' on the first error, or if all the failures are aggregated
   /// together in the evaluation result.
   /// </summary>

   public enum TerrainPointEvaluationType
   {
      TPE_UNKNOWN = 0,
      TPE_FAIL_FAST_ON_FIRST_ERROR,
      TPE_AGGREGATE_FAILURES
   }
}

