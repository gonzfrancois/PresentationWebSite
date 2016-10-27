﻿using System.Collections;
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
                if (actualList.Count != expectedList.Count)
                    Assert.Fail("Property {0}.{1} does not match. Expected IList containing {2} elements but was IList containing {3} elements", property.PropertyType.Name, property.Name, expectedList.Count, actualList.Count);

                for (var i = 0; i < actualList.Count; i++)
                {
                    PropertyValuesAreEquals(actualList[i], expectedList[i]);
                }
            }
        }
    
}