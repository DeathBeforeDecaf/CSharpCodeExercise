using NUnit.Framework;

namespace AntonWCodeExercise;

/// <summary>
/// Draft of test cases to exercise functionality of the VerifyintervalVelocity TPointEvaluator.
/// </summary>

public class IntervalVelocityTests01
{
   private TerrainPointEvaluator[] acceptanceCriteria = new TerrainPointEvaluator[1];

   private const float ONE_MILLIONTH_FLT = 0.000001f;

   [SetUp]
   public void Setup()
   {
      acceptanceCriteria[0] = new VerifyIntervalVelocity();
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
   public void test0101FirstIntervalEqualsFourFeetPerSecond()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(0.0f, 4.0f, 500.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);


      tPoints.Add(new TerrainPoint(4.0f, 10.0f, 1000.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);
   }

   [Test]
   public void test0102LastIntervalEqualsFourFeetPerSecond()
   {
      Assert.Fail();
   }

   [Test]
   public void test0103FirstIntervalExceedsFourFeetPerSecondByOneMillionth()
   {
      Assert.Fail();
   }

   [Test]
   public void test0104LastIntervalExceedsFourFeetPerSecondByOneMillionth()
   {
      List<TerrainPoint> tPoints = new List<TerrainPoint>();

      tPoints.Add(new TerrainPoint(1.0f, 4.0f, 500.0f));

      CollectionEvaluator ceval = new CollectionEvaluator(tPoints.ToArray());

      int failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);


      tPoints.Add(new TerrainPoint(5.0f, 10.0f, 1000.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      Assert.Zero(failCount);


      tPoints.Add(new TerrainPoint(9.0f + ONE_MILLIONTH_FLT, -7.0f, 1000.0f));

      ceval = new CollectionEvaluator(tPoints.ToArray());

      failCount = ceval.evaluate(TerrainPointEvaluationType.TPE_AGGREGATE_FAILURES, acceptanceCriteria);

      // verify one error count
      Assert.AreEqual(1, failCount);

      Assert.AreEqual(1, ceval.failingPoints.Count);

      // verify error location and content
      Assert.AreEqual(2, ceval.failingPoints[0].position);
      Assert.AreEqual(EvaluationCodeType.EC_MOVEMENT_VELOCITY_EXCEEDS_TRAVEL_LIMIT,
                      Enum.ToObject(typeof(EvaluationCodeType), ceval.failingPoints[0].status[0].code));

      Assert.IsTrue(ceval.failingPoints[0].status[0].message.Contains(
                    "Interval velocity between TerrainPoint[1] and TerrainPoint[2] exceeds 4 feet per second travel limit"));

      Console.WriteLine("Failing TerrainPoint.status.message = " + ceval.failingPoints[0].status[0].message);
   }

   [Test]
   public void test0105FirstIntervalExceedsFourFeetPerSecondByOneMillionthInReverse()
   {
      Assert.Fail();
   }

   [Test]
   public void test0106LastIntervalExceedsFourFeetPerSecondByOneMillionthInReverse()
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
      // tPoint[1].timeDiffMS = Nan

      Assert.Fail();
   }
}

