using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.Utilities
{
    public static class CommonUtility
    {
        public static Dictionary<string, object> AddDictionaryToDynamicType(Dictionary<string, object> dictionary,
            dynamic jsonDataDynamic)
        {
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(jsonDataDynamic))
            {
                try
                {
                    object obj = propertyDescriptor.GetValue(jsonDataDynamic!);

                    if (!dictionary.ContainsKey(propertyDescriptor.Name))
                    {
                        dictionary.Add(propertyDescriptor.Name, obj);
                    }
                    else
                    {
                        dictionary.Remove(propertyDescriptor.Name);
                        dictionary.Add(propertyDescriptor.Name, obj);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            return dictionary;
        }

    }
}
