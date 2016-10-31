using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Reflection;

namespace PresentationWebSite.UI.WebMvc.Tests.Helpers
{
    class AssertExtension
    {
        public static void PropertyValuesAreEquals(object actual, object expected)
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object expectedValue = property.GetValue(expected, null);
                object actualValue = property.GetValue(actual, null);

                var list = actualValue as IList;
                if (list != null)
                    AssertListsAreEquals(property, list, (IList)expectedValue);
                else if (!Equals(expectedValue, actualValue))
                    Assert.Fail("Property {0}.{1} does not match. Expected: {2} but was: {3}", property.DeclaringType?.Name, property.Name, expectedValue, actualValue);
            }
        }

        private static void AssertListsAreEquals(PropertyInfo property, IList actualList, IList expectedList)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            if (actualList.Count != expectedList.Count)
                Assert.Fail("Property {0}.{1} does not match. Expected IList containing {2} elements but was IList containing {3} elements", property.PropertyType.Name, property.Name, expectedList.Count, actualList.Count);

            for (var i = 0; i < actualList.Count; i++)
            {
                PropertyValuesAreEquals(actualList[i], expectedList[i]);
            }
        }

        public static bool CompareIEnumerable<T>(IEnumerable<T> one, IEnumerable<T> two, Func<T, T, bool> comparisonFunction)
        {
            var oneArray = one as T[] ?? one.ToArray();
            var twoArray = two as T[] ?? two.ToArray();

            if (oneArray.Length != twoArray.Length)
            {
                Assert.Fail("Collections are not same length");
            }

            for (var i = 0; i < oneArray.Length; i++)
            {
                var isEqual = comparisonFunction(oneArray[i], twoArray[i]);
                Assert.IsTrue(isEqual);
            }
            return true;
        }
    }

}
