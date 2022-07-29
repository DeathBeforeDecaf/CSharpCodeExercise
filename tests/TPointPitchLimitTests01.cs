using NUnit.Framework;

namespace AntonWCodeExercise;

/// <summary>
/// Draft of test cases to exercise functionality of the VerifyPitchLimit TPointEvaluator.
/// </summary>
public class TPointPitchLimitTests01
{
   private TerrainPointEvaluator[] acceptanceCriteria = new TerrainPointEvaluator[1];

   private const float ONE_HUNDRED_THOUSANDTH_FLT = 0.00001f;

   [SetUp]
   public void Setup()
   {
      acceptanceCriteria[0] = new VerifyPitchLimit();
   }

   [TearDown]
   public void Teardown()
   {
   }

   // Positive Test Cases
   [Test]
   public void test0001ProvidedExampleData()
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


      tPoints.Add(new TerrainPoint(3.0f, 44.5f, 3000.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }


   [Test]
   public void test0002FirstPitchIsNaN()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, float.NaN, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }

   [Test]
   public void test0003LastPitchIsNan()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 4.0f, 1000.0f));

      tPoints.Add(new TerrainPoint(1.0f, float.NaN, 500.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }

   [Test]
   public void test0004PitchOneHundredThousandthGreaterThanNegativeLimit()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, -45.0f + ONE_HUNDRED_THOUSANDTH_FLT, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }

   [Test]
   public void test0005PitchOneHundredThousandthSmallerThanPositiveLimit()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 45.0f - ONE_HUNDRED_THOUSANDTH_FLT, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }


   [Test]
   public void test0006FirstPitchEquals45Degrees()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 45.0f, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }

   [Test]
   public void test0007LastPitchEqualsNegative45Degrees()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 45.0f, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);

   }

   // Negative Test Cases

   [Test]
   public void test0101FirstPitchExceeds45DegreesByOneHundredThousandth()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 45.0f + ONE_HUNDRED_THOUSANDTH_FLT, 1000.0f));

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

   [Test]
   public void test0102LastPitchExceedsNegative45DegreesByOneHundredThousandth()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, float.NaN, 1000.0f));
      tPoints.Add(new TerrainPoint(0.8f, -45.0f - ONE_HUNDRED_THOUSANDTH_FLT, 1000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(1, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_PITCH_SENTINEL_VALUE_EXCEEDED,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Pitch angle for TerrainPoint[1] exceeds -45.0 degree limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }

   // Pathelogical Test Cases (verify discontinuity)

   [Test]
   public void test0201FromNonFiniteToValidValue()
   {
      // tPoint[0].pitchDeg = -INF]
      // tPoint[0].pitchDeg = +INF]

      // tPoint[1].pitchDeg = (valid value in range)

      Assert.Fail();
   }

   [Test]
   public void test0202FromValidValueToNonFinite()
   {
      // tPoint[0].pitchDeg = (valid value in range)
      // tPoint[1].pitchDeg = -INF
      // tPoint[1].pitchDeg = +INF

      Assert.Fail();
   }
}

