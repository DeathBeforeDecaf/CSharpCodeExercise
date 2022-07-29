using System;

namespace AntonWCodeExercise
{
   public class EvaluationResult
   {
      public List<EvaluationStatus> status
      { get; }

      public int position
      { get; }

      /// <summary>
      /// Provides an association for a collection of evaluation results for
      /// a given TerrainPoint.
      /// </summary>
      /// <param name="status">The status codes and message of one or more
      /// TerrainPoint evaluations</param>
      /// <param name="position">The current TerrainPoint under evaluation</param>

      public EvaluationResult(List<EvaluationStatus> status,
                               int position)
      {
         this.status = status;
         this.position = position;
      }
   }
}

