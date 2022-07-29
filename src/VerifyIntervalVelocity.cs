using System;
namespace AntonWCodeExercise
{
   public class VerifyIntervalVelocity : TerrainPointEvaluator
   {
      public VerifyIntervalVelocity() { }

      /// <summary>
      /// Evaluates a TerrainPoint or a TerrainPoint interval (2 points) to
      /// determine the velocity of capture between the two points exceeds
      /// four feet per second velocity limit.  NOTE: Assume that the data
      /// point is deterministically invalid if velocity exceeds 4 ft/s.
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
            if (float.IsFinite(point[0].distanceFT) && float.IsFinite(point[1].distanceFT)
                 && float.IsFinite(point[1].timeDiffMS))
            {
               // must have non-negative movement and positive deltaT
               if ((0.0f <= point[0].distanceFT) && (0.0f <= point[1].distanceFT) && (float.Epsilon < point[1].timeDiffMS))
               {
                  // in previous point <= current point
                  if (point[1].distanceFT >= point[0].distanceFT)
                  {
                     float deltaX = point[1].distanceFT - point[0].distanceFT;

                     float feetPerSecond = (float)(((double)deltaX * 1000.0 / ((double)point[1].timeDiffMS)));

                     if (4.0f < feetPerSecond)
                     {
                        string errorMsg =
                            "VerifyIntervalVelocity.evaluate(): Interval velocity between TerrainPoint[" + (position - 1) + "] and TerrainPoint["
                            + position + "] exceeds 4 feet per second travel limit (" + String.Format("{0:0.00}", feetPerSecond) + ")";

                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_MOVEMENT_VELOCITY_EXCEEDS_TRAVEL_LIMIT, errorMsg));
                     }
                     else
                     {
                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
                     }
                  }
                  else // reverse movement
                  {
                     float deltaX = point[0].distanceFT - point[1].distanceFT;

                     float feetPerSecond = (float)(((double)deltaX * 1000.0 / ((double)point[1].timeDiffMS)));

                     if (4.0f < feetPerSecond)
                     {
                        string errorMsg =
                            "VerifyIntervalVelocity.evaluate(): Interval velocity between TerrainPoint[" + (position - 1) + "] and TerrainPoint["
                            + position + "] exceeds 4 feet per second travel limit (" + String.Format("{0:0.00}", feetPerSecond) + ")";

                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_MOVEMENT_VELOCITY_EXCEEDS_TRAVEL_LIMIT, errorMsg));
                     }
                     else
                     {
                        status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
                     }
                  }
               }
               else // negative movement, warn only (handled by VerifyIntervalDistance)
               {
                  // must have non-negative movement and positive deltaT
                  if (0.0f > point[1].distanceFT)
                  {
                     string warnMsg =
                         "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                         + "] with negative distanceFT value of (" + String.Format("{0:0.00}", point[1].distanceFT) + ")";

                     status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
                  }

                  if (float.Epsilon >= point[1].timeDiffMS)
                  {
                     string warnMsg =
                         "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                         + "] with timeDiffMS value of (" + String.Format("{0:0.00}", point[1].timeDiffMS) + ")";

                     status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
                  }
               }
            }
            else
            {
               if (float.IsInfinity(point[1].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                      + "] with distanceFT value of (+INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNegativeInfinity(point[1].distanceFT))
               {
                  string warnMsg =
                      "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                      + "] with distanceFT value of (-INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));

               }
               else if (float.IsNaN(point[1].distanceFT))
               {
                  string warnMsg =
                  "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                  + "] with distanceFT value of (NaN)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }

               if (float.IsInfinity(point[1].timeDiffMS))
               {
                  string warnMsg =
                      "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                      + "] with timeDiffMS value of (+INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
               else if (float.IsNegativeInfinity(point[1].timeDiffMS))
               {
                  string warnMsg =
                      "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                      + "] with timeDiffMS value of (-INF)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));

               }
               else if (float.IsNaN(point[1].timeDiffMS))
               {
                  string warnMsg =
                      "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                      + "] with timeDiffMS value of (NaN)";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
               }
            }
         }
         else if (1 == point.Length)
         {
            if (float.IsInfinity(point[0].distanceFT))
            {
               string warnMsg =
                   "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                   + "] with distanceFT value of (+INF)";

               status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
            }
            else if (float.IsNegativeInfinity(point[0].distanceFT))
            {
               string warnMsg =
                   "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                   + "] with distanceFT value of (-INF)";

               status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
            }
            else if (float.IsNaN(point[0].distanceFT))
            {
               string warnMsg =
                   "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                   + "] with distanceFT value of (NaN)";

               status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
            }

            if (0.0f > point[0].distanceFT)
            {
               string warnMsg =
                   "VerifyIntervalVelocity.evaluate(): Ignoring TerrainPoint[" + position
                   + "] with negative distanceFT value of (" + String.Format("{0:0.00}", point[0].distanceFT) + ")";

               status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_UNKNOWN, warnMsg));
            }
         }
         else
         {
            throw new InvalidDataException("VerifyIntervalVelocity.evaluate(TerrainPoint[] point, int position): point.Length must be 1 or 2");
         }

         // fallthrough for incomplete result where the error will be reported elsewhere
         if (0 == status.Count)
         {
            status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
         }

         return new EvaluationResult(status, position);
      }
   }
}

