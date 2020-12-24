﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Infrastructure.DataAccess.ScriptGenerator
{
    public static class ScriptGeneratorFactory
    {
        public static ScriptGeneratorBase CreateInstance(Type generateType)
        {
            //if (ConfigurationManager.AppSettings["scriptGenerator"] != null)
            //{
            //    string className = ConfigurationManager.AppSettings["scriptGenerator"];
            //    Type tip = System.Reflection.Assembly.GetExecutingAssembly().GetType(className);
            //    return (ScriptGeneratorBase)CreateInstanceWithParams(tip, generateType);
            //}
            //else
            //{
            //    return new MsSqlScriptGenerator(generateType);
            //}
            return new MsSqlScriptGenerator(generateType);
        }

        public static ScriptGeneratorBase CreateInstance(Type generateType, Type generatorType)
        {
            return (ScriptGeneratorBase)CreateInstanceWithParams(generatorType, generateType);
        }

        private static object CreateInstanceWithParams(Type pContext, params object[] pArguments)
        {
            var constructors = pContext.GetConstructors();

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length != pArguments.Length)
                    continue;

                // assumed you wanted a matching constructor
                // not just one that matches the first two types
                bool fail = false;
                for (int x = 0; x < parameters.Length && !fail; x++)
                    if (!parameters[x].ParameterType.IsInstanceOfType(pArguments[x]))
                        fail = true;

                if (!fail)
                    return constructor.Invoke(pArguments);
            }
            return null;
        }
    }
}
