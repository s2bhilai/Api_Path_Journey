using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(
            this IEnumerable<TSource> source,string fields)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            //create a list to hold ExpandOObjects
            var expandOObjectList = new List<ExpandoObject>();

            var propertyInfoList = new List<PropertyInfo>();

            if(string.IsNullOrWhiteSpace(fields))
            {
                //all public properties should be in ExpandOObject
                var propertyInfos = typeof(TSource)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fields.Split(",");

                foreach (var field in fieldsAfterSplit)
                {
                    //can't trim var in foreach, so use another var
                    var propertyName = field.Trim();

                    //use reflection
                    var propertyInfo = typeof(TSource)
                        .GetProperty(propertyName, BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                    if(propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} wasnt found on " +
                            $"{typeof(TSource)}");
                    }

                    propertyInfoList.Add(propertyInfo);
                }
            }

            //run through source objects
            foreach (TSource sourceObject in source)
            {
                //create an expandoobject that will hold the selected properties
                // and values
                var dataShapedObject = new ExpandoObject();

                //get the value of each property we have to return
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    //add the field to the ExpandOObject
                    ((IDictionary<string, object>)dataShapedObject)
                        .Add(propertyInfo.Name, propertyValue);
                }

                //add expandoobject to list
                expandOObjectList.Add(dataShapedObject);
            }

            return expandOObjectList;
        }
    }
}
