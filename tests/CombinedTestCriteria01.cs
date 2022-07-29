using NUnit.Framework;

namespace AntonWCodeExercise;

/// <summary>
/// Draft of a combined test suite to exercise functionality of the VerifyIntervalDistance, VerifyIntervalVelocity,
/// and VerifyPitchLimit TPointEvaluators.
/// </summary>

// TODO: Move the duplicated positive test cases here so they can be checked as
//       quickly (and efficiently) as possible

public class CombinedTestCriteria01
{
   private TerrainPointEvaluator[] acceptanceCriteria = new TerrainPointEvaluator[3];

   private const float ONE_MILLIONTH_FLT = 0.000001f;
   private const float ONE_HUNDRED_THOUSANDTH_FLT = 0.00001f;

   [SetUp]
   public void Setup()
   {
      acceptanceCriteria[0] = new VerifyIntervalDistance();

      acceptanceCriteria[1] = new VerifyIntervalVelocity();

      acceptanceCriteria[2] = new VerifyPitchLimit();
   }

   [TearDown]
   public void Teardown()
   {
   }

   // Positive Test Cases
   [Test]
   public void test0001ProvidedExampleDataSubset()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 4.0f, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);


      tPoints.Add(new TerrainPoint(1.0f, 10.0f, 500.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);


      tPoints.Add(new TerrainPoint(1.999f, -2.0f, 854.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }


   // Negative Cases
   [Test]
   public void test0101VerifyIntervalDistanceEvaluatorActive()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(1.5f, 10.0f, 500.0f));
      tPoints.Add(new TerrainPoint(2.7f, 35.0f, 750.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(1, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_DISTANCE_INTERVAL_LIMIT_EXCEEDED,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Interval distance between TerrainPoint[0] and TerrainPoint[1] exceeds one foot interval limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);

   }


   [Test]
   public void test0102VerifyIntervalVelocityEvaluatorActive()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, -14.9f, 500.0f));
      tPoints.Add(new TerrainPoint(0.9f, 37.0f, 125.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(1, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_MOVEMENT_VELOCITY_EXCEEDS_TRAVEL_LIMIT,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Interval velocity between TerrainPoint[0] and TerrainPoint[1] exceeds 4 feet per second travel limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }

   [Test]
   public void test0103VerifyPitchLimitEvaluatorActive()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 55.0f, 500.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(0, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Pitch angle for TerrainPoint[0] exceeds +45.0 degree limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }
}