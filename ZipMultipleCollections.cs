using System;
using System.Collections;
using System.Collections.Generic;

namespace UCC_Coding_Exam
{
    public static class ZipMultipleCollections
    {
        public static IEnumerable ZipToSignatoryName(
            this IEnumerable<object> title, 
            IEnumerable<string> firstName, 
            IEnumerable<string> middleName, 
            IEnumerable<string> lastName, 
            IEnumerable<string> suffix, 
            Func<object, object, object, object, object> func)
        {
            using (var t = title.GetEnumerator())
            using (var fn = firstName.GetEnumerator())
            using (var mn = middleName.GetEnumerator())
            using (var ln = lastName.GetEnumerator())
            using (var sfix = suffix.GetEnumerator())
            {
                while (t.MoveNext() && fn.MoveNext() && mn.MoveNext() && ln.MoveNext() && sfix.MoveNext())
                {
                    yield return func(fn.Current, mn.Current, ln.Current, sfix.Current);
                }
            }
        }
    }
}