﻿using System;
using System.Reflection;

namespace FlubuCore.Scripting.Analysis.Processors
{
    public class ReferenceDirectiveProcessor : IScriptProcessor
    {
        public bool Process(ScriptAnalyzerResult analyzerResult, string line, int lineIndex)
        {
            if (!line.TrimStart().StartsWith("//#ref", StringComparison.OrdinalIgnoreCase))
                return false;

            int dllIndex = line.IndexOf(" ", StringComparison.Ordinal);

            if (dllIndex < 0)
                return true;

            string dll = line.Substring(dllIndex);
            var type = Type.GetType(dll, true);
            var ass = type.GetTypeInfo().Assembly;

            analyzerResult.References.Add(ass.ToAssemblyInfo());
            return true;
        }
    }
}