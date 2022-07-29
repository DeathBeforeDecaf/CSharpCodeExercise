using System;
namespace AntonWCodeExercise
{
   /// <summary>
   /// Enumerated type representing the outcome status code of a given
   /// TerrainPoint evaluation.
   /// </summary>
   public enum EvaluationCodeType
   {
      EC_UNKNOWN = 0,
      EC_SUCCESS = 1,
      EC_DISTANCE_INTERVAL_LIMIT_EXCEEDED = 2,
      EC_NEGATIVE_MOVEMENT_DISTANCE_VALUE = 4,
      EC_MOVEMENT_VELOCITY_EXCEEDS_TRAVEL_LIMIT = 8,
      EC_PITCH_SENTINEL_VALUE_EXCEEDED = 16,
   }
}

