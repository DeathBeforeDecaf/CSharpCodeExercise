using System;

namespace AntonWCodeExercise
{
   public class EvaluationStatus
   {
      public int code
      { get; }

      public string message
      { get; }

      /// <summary>
      /// Constructor for a single Acceptance Criteria evaluation result.
      /// Categorizes the result of the criteria evaluation (code), and
      /// provides a verbose description of the outcome (message)
      /// </summary>
      /// <param name="code">Unique code result per Acceptance Criteria
      /// when the evaluation completes</param>
      /// <param name="message">A human readable verbose status message</param>

      public EvaluationStatus(int code, string message)
      {
         this.code = code;
         this.message = message;
      }
   }
}

