using System;
namespace AntonWCodeExercise
{
   /// <summary>
   /// Objects that implement the TerrainPointEvaluator interface are used to
   /// verify Acceptance Criteria by returning an evaluation result. If
   /// point.Length == 1, then point represents the current point at position.
   /// If point.Length == 2, then point[0] is the previous TerrainPoint and
   /// point[1] is the current point at position.
   /// </summary>

   public interface TerrainPointEvaluator
   {
      public EvaluationResult evaluate(TerrainPoint[] point, int position);
   }
}

