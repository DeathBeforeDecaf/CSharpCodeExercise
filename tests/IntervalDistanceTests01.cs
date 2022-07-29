using NUnit.Framework;

namespace AntonWCodeExercise;

/// <summary>
/// Draft of test cases to exercise functionality of the VerifyintervalDistance TPointEvaluator.
/// </summary>
public class IntervalDistanceTests01
{
   private TerrainPointEvaluator[] acceptanceCriteria = new TerrainPointEvaluator[1];

   private const float ONE_MILLIONTH_FLT = 0.000001f;

   [SetUp]
   public void Setup()
   {
      acceptanceCriteria[0] = new VerifyIntervalDistance();
   }

   [TearDown]
   public void Teardown()
   {
   }

   // Positive Test Cases

   /// <summary>
   /// Uses a subset of the test data from the example, probably a good place to start.
   /// NOTE: Assuming that the time values are off by a factor of 1000 in the provided TerrainPoints.
   /// </summary>
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

   [Test]
   public void test0002MonotonicallyIncreasing()
   {
      Assert.Fail();
   }

   [Test]
   public void test0003MonotonicallyDecreasing()
   {
      // TODO: Get clarification on the exact meaning of "negative distance movements"
      //       Is this moving in reverse, or negative values for TerrainPoint.distanceFT?
      //       (assuming the later for now)

      Assert.Fail();
   }

   [Test]
   public void test0004AllPointsStationary()
   {
      Assert.Fail();
   }

   [Test]
   public void test0005StoppedThenMoving()
   {
      Assert.Fail();
   }

   [Test]
   public void test0006MovingThenStopped()
   {
      Assert.Fail();
   }

   [Test]
   public void test0007StoppedThenMovingThenStopped()
   {
      Assert.Fail();
   }

   [Test]
   public void test0008MovingThenStoppedThenMoving()
   {
      Assert.Fail();
   }

   // Negative Test Cases

   [Test]
   public void test0101FirstIntervalExceedsOneFoot()
   {
      Assert.Fail();
   }

   [Test]
   public void test0102LastIntervalExceedsOneFoot()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 4.0f, 1000.0f));
      tPoints.Add(new TerrainPoint(1.0f, 10.0f, 500.0f));
      tPoints.Add(new TerrainPoint(1.999f, -2.0f, 854.0f));
      tPoints.Add(new TerrainPoint(3.0f, 44.5f, 3000.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(3, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_DISTANCE_INTERVAL_LIMIT_EXCEEDED,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Interval distance between TerrainPoint[2] and TerrainPoint[3] exceeds one foot interval limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }

   [Test]
   public void test0103FirstIntervalNegativeDistanceFT()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(-1.0f, 2.0f, 700.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(0, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_NEGATIVE_MOVEMENT_DISTANCE_VALUE,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Negative distance movement (-"));
      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                 ") recorded at TerrainPoint[0]"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }

   [Test]
   public void test0104LastIntervalNegativeDistanceFT()
   {
      Assert.Fail();
   }

   // Pathelogical Test Cases (verify discontinuity)

   [Test]
   public void test0201FromNonFiniteToValidValue()
   {
      // tPoint[0].distanceFT = -INF]
      // tPoint[0].distanceFT = +INF]
      // tPoint[0].distanceFT = NaN

      // tPoint[1].distanceFT = (valid value in range)
      // tPoint[1].timeDiffMS = (valid value in range)

      Assert.Fail();
   }

   [Test]
   public void test0202FromValidValueToNonFinite()
   {
      // tPoint[0].distanceFT = (valid value in range)
      // tPoint[1].distanceFT = -INF
      // tPoint[1].distanceFT = +INF
      // tPoint[1].distanceFT = NaN

      // tPoint[1].timeDiffMS = (valid value in range)

      Assert.Fail();
   }

   [Test]
   public void test0203FromNonFiniteToNonFinite()
   {
      // tPoint[0].distanceFT = -INF]
      // tPoint[0].distanceFT = +INF]
      // tPoint[0].distanceFT = NaN

      // tPoint[1].distanceFT = -INF
      // tPoint[1].distanceFT = +INF
      // tPoint[1].distanceFT = NaN

      // tPoint[1].timeDiffMS = (valid value in range)

      Assert.Fail();
   }

   [Test]
   public void test0204DeltaTEqualsZero()
   {
      Assert.Fail();
   }

   [Test]
   public void test0205DeltaTEqualsEpsilon()
   {
      Assert.Fail();
   }

   [Test]
   public void test0206DeltaTEqualsOneMillionth()
   {
      Assert.Fail();
   }


   [Test]
   public void test0207DeltaTNonFinite()
   {
      // tPoint[0].distanceFT = (valid value in range)
      // tPoint[1].distanceFT = (valid value in range)

      // tPoint[1].timeDiffMS = -INF
      // tPoint[1].timeDiffMS = +INF
      // tPoint[1].timeDiffMS = NaN

      Assert.Fail();
   }

}
