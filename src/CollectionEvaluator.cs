using System;

namespace AntonWCodeExercise
{
   /// <summary>
   /// Evaluates the Acceptance Criteria for a collection of TerrainPoint values,
   /// returning the number of failure points from evaluation.
   /// </summary>
   public class CollectionEvaluator
   {
      private int acceptedPointCounter;

      public List<EvaluationResult> failingPoints
      {
         get;
      }

      public TerrainPoint[] points
      {
         get;
      }

      public CollectionEvaluator(TerrainPoint[] collection)
      {
         failingPoints = new List<EvaluationResult>();

         points = collection;
      }

      /// <summary>Apply each of TerrainPointEvaluator[] to TerrainPoint collection, aggregating results in the manner
      /// specified in TerrainPointEvaluationType (fail fast or return all failures).</summary>
      /// <param name="evalType">TPE_FAIL_FAST_ON_FIRST_ERROR returns on first error
      /// TPE_AGGREGATE_FAILURES collects all failure results from all TerrainPoints evaluated against all
      /// acceptanceCriteria</param>
      /// <param name="acceptanceCriteria">Array of class instances implementing TerrainPointEvaluator interface, used
      /// to verify the Acceptance Criteria is successfully met</param>
      /// <returns>The combined number of failures and unhandled TestPoint data discontinuities</returns>
      /// <exception cref="InvalidDataException">Exception thrown when the behavior of the evaluation type is
      /// unspecified (unknown)</exception>

      public int evaluate(TerrainPointEvaluationType evalType, TerrainPointEvaluator[] acceptanceCriteria)
      {
         if (TerrainPointEvaluationType.TPE_UNKNOWN != evalType)
         {
            acceptedPointCounter = 0;

            failingPoints.Clear();

            for (int i = 0; i < points.Length; i++)
            {
               int failureCounter = 0;

               foreach (TerrainPointEvaluator ac in acceptanceCriteria)
               {
                  TerrainPoint[] input = new TerrainPoint[(0 != i) ? 2 : 1];

                  if (2 == input.Length)
                  {
                     input[0] = points[i - 1];

                     input[1] = points[i];
                  }
                  else
                  {
                     input[0] = points[i];
                  }

                  EvaluationResult result = ac.evaluate(input, i);

                  for (int j = 0; j < result.status.Count; j++)
                  {
                     if ((int)EvaluationCodeType.EC_SUCCESS != result.status[j].code)
                     {
                        failingPoints.Add(result);

                        ++failureCounter;
                     }
                  }
               }

               if (0 == failureCounter)
               {
                  ++acceptedPointCounter;
               }
               else if (TerrainPointEvaluationType.TPE_FAIL_FAST_ON_FIRST_ERROR == evalType)
               {
                  return failingPoints.Count;
               }
            }
         }
         else
         {
            throw new InvalidDataException("CollectionEvaluator.evaluate(): EvaluationType must be a well known "
                                           + "value ( TPE_FAIL_FAST_ON_FIRST_ERROR or TPE_AGGREGATE_FAILURES )");
         }

         return failingPoints.Count;
      }

      public int getAcceptedCount()
      {
         return acceptedPointCounter;
      }
   }
}

