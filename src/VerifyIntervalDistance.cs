using System;
namespace AntonWCodeExercise
{
   public class VerifyIntervalDistance : TerrainPointEvaluator
   {
      public VerifyIntervalDistance() { }

      /// <summary>
      /// Evaluates a TerrainPoint or a TerrainPoint interval (2 points) to
      /// determine the difference in distance between the two points exceeds
      /// a one foot interval limit.  Also verifies TerrainPoint distance values
      /// are not negative (assuming this fits AC:1b instead of reverse movement).
      /// </summary>
      /// <param name="point">Either a single TerrainPoint or a 2 TerrainPoint
      /// interval</param>
      /// <param name="position">The offset indicator in the input array that
      /// points to the current TerrainPoint under evaluation.</param>
      /// <returns>An EvaluationResult containing a list of evaluation
      /// outcomes associated with the current TerrainPoint</returns>
      /// <exception cref="InvalidDataException">evaluate() method requires
      /// TerrainPoint of length 1 or 2, length == 1 for the 0th input
      /// element, length == 2 for any other TerrainPoint interval</exception>

      public EvaluationResult evaluate(TerrainPoint[] point, int position)
      {
         List<EvaluationStatus> status = new List<EvaluationStatus>();

         if (2 == point.Length)
         {
            if (float.IsFinite(point[0].distanceFT) && float.IsFinite(point[1].distanceFT))
            {
               // must have non-negative movement
               if ((0.0f <= point[0].distanceFT) && (0.0f <= point[1].distanceFT))
               {
                  // in previous point <= current point
                  if (point[1].distanceFT >= point[0].distanceFT)
                  {
                     float deltaX = point[1].distanceFT - point[0].distanceFT;

                     if (1.0f < deltaX)
                     {
                        string errorMsg =
                            "VerifyIntervalDistance.evaluate(): Interval distance between TerrainPoint[" + (position - 1)
                            + "] and TerrainPoint[" + position + "] exceeds one foot interval limit ("
                            + String.Format("{0:0.0000}", deltaX) + ")";

                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_DISTANCE_INTERVAL_LIMIT_EXCEEDED, errorMsg));
                     }
                  }
                  else // reverse movement
                  {
                     float deltaX = point[0].distanceFT - point[1].distanceFT;

                     if (1.0f < deltaX)
                     {
                        string errorMsg =
                           "VerifyIntervalDistance.evaluate(): Interval distance between TerrainPoint[" + (position - 1)
                           + "] and TerrainPoint[" + position + "] exceeds one foot interval limit ("
                           + String.Format("{0:0.0000}", deltaX) + ")";

                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_DISTANCE_INTERVAL_LIMIT_EXCEEDED, errorMsg));
                     }
                  }
               }
               else
               {
                  if (0.0f > point[1].distanceFT)
                  {
                     string errorMsg =
                        "VerifyIntervalDistance.evaluate(): Negative distance movement ("
                        + String.Format("{0:0.00}", point[1].distanceFT)
                        + ") recorded at TerrainPoint[" + position + "]";

                     status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_NEGATIVE_MOVEMENT_DISTANCE_VALUE, errorMsg));
                  }
               }

            }
            else
            {
               if (float.IsInfinity(point[1].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (+INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNegativeInfinity(point[1].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (-INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNaN(point[1].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (NaN)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
            }
         }
         else if (1 == point.Length)
         {
            if ((float.IsFinite(point[0].distanceFT)) && (0.0f > point[0].distanceFT))
            {
               string errorMsg =
                   "Negative distance movement (" + String.Format("{0:0.00}", point[0].distanceFT)
                   + ") recorded at TerrainPoint[" + position + "]";

               status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_NEGATIVE_MOVEMENT_DISTANCE_VALUE, errorMsg));
            }
            else
            {
               if (float.IsInfinity(point[0].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (+INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNegativeInfinity(point[0].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (-INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNaN(point[0].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalDistance.evaluate(): Ignoring TerrainPoint[" + position + "] with value of (NaN)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
            }
         }
         else
         {
            throw new InvalidDataException("VerifyIntervalDistance.evaluate(TerrainPoint[] point, int position): "
                                            + "point.Length must be 1 or 2");
         }

         // fallthrough for dependent points with partial data and errors handled in other iterations
         if (0 == status.Count)
         {
            status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
         }

         return new EvaluationResult(status, position);
      }
   }
}

