using System;
namespace AntonWCodeExercise
{
   public class VerifyPitchLimit : TerrainPointEvaluator
   {
      public VerifyPitchLimit() { }

      /// <summary>
      /// Evaluates a TerrainPoint or a TerrainPoint interval (2 points) to
      /// check that pitch is a value between -45 deg and +45 deg or Nan.
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

         if ((2 != point.Length) && (1 != point.Length))
         {
            throw new InvalidDataException("VerifyPitchLimit.evaluate(TerrainPoint[] point, int position): point.Length must be 1 or 2");
         }

         int offset = 0;

         if (2 == point.Length)
         {
            offset = 1;
         }

         if (float.IsInfinity(point[offset].pitchDEG))
         {
            string errorMsg =
                "Pitch angle for TerrainPoint[" + position
                + "] exceeds +45.0 degree limit (+INF)";

            status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED, errorMsg));
         }
         else if (float.IsNegativeInfinity(point[offset].pitchDEG))
         {
            string errorMsg =
                "Pitch angle for TerrainPoint[" + position
                + "] exceeds -45.0 degree limit (-INF)";

            status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED, errorMsg));
         }
         else if (float.IsNaN(point[offset].pitchDEG))
         {
            status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
         }
         else
         {
            if (0.0f <= point[offset].pitchDEG)
            {
               if (45.0f < point[offset].pitchDEG)
               {
                  string errorMsg =
                      "Pitch angle for TerrainPoint[" + position
                      + "] exceeds +45.0 degree limit ("
                      + String.Format("{0:0.0000}", point[offset].pitchDEG) + ")";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED, errorMsg));
               }
               else
               {
                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
               }
            }
            else // negative pitch
            {

               if (-45.0f > point[offset].pitchDEG)
               {
                  string errorMsg =
                      "Pitch angle for TerrainPoint[" + position
                      + "] exceeds -45.0 degree limit ("
                      + String.Format("{0:0.0000}", point[offset].pitchDEG) + ")";

                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED, errorMsg));
               }
               else
               {
                  status.Add(new EvaluationStatus((int)EvaluationCodeType.EC_SUCCESS, "OK"));
               }
            }
         }

         return new EvaluationResult(status, position);
      }
   }
}

