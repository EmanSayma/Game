using System;
using System.Collections.Generic;
using Xunit.Sdk;
using System.Reflection;

namespace GameEngine.Tests
{
    public class HealthDamageDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] {0, 100};
            yield return new object[] {1, 99};
            yield return new object[] {50, 50};
            yield return new object[] {101, 1};

        }
    }
}