using System;

namespace AntonWCodeExercise
{
   // NOTE: Made a few changes to class TerrainPoints, these changes include:
   //       1) consistently expressing unit types as a suffix,
   //       2) removing the setters since partially populated captures are not valid,
   //       3) removing the plural 's' since each TerrainPoint represents a
   //          single data capture

   public class TerrainPoint
   {
      public float distanceFT
      {
         get; // set;
      }

      public float pitchDEG
      {
         get; // set;
      }

      public float timeDiffMS
      {
         get; // set;
      }

      //      NOTE: this default constructor (and associated comment) should be removed
      //      since it's unnecessary and using it could yield TerrainPoints which are
      //      partially populated with some valid and some default information.
      //
      //      public TerrainPoint()
      //      {
      //          distanceFT = 0.0f;
      //          pitchDEG = 0.0f;
      //          timeDiffMS = 0.0f;
      //      }

      //      NOTE: if the distance measurement axis is not orthagonal to the pitch
      //            measurement axis, it would probably be good to remove the x & y
      //            prefix from the parameter names since their context would not be
      //            valid

      /// <summary>
      /// Constructor to create instance of TerrainPoint capture.
      /// </summary>
      /// <param name="xDistanceFT">the distance in feet relative to reference
      /// point the measuring instrument has moved since last capture</param>
      /// <param name="yPitchDEG">the +/- angle in degrees relative to the
      /// horizon</param>
      /// <param name="deltaMS">the elapsed time in milliseconds since the
      /// last capture</param>

      public TerrainPoint(float xDistanceFT, float yPitchDEG, float deltaMS)
      {
         distanceFT = xDistanceFT;
         pitchDEG = yPitchDEG;
         timeDiffMS = deltaMS;
      }
   }
}
