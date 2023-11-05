using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_Boletim
{
    public static class EnvFileReader
    {
        private static Dictionary<string, string> envVariables = new Dictionary<string, string>();

        public static void LoadEnvFile(string path)
        {
            if (File.Exists(path))
            {
                var lines = File.ReadLines(path);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0];
                        var value = parts[1];
                        envVariables[key] = value;
                    }
                }
            }
        }

        public static string GetEnv(string key)
        {
            if (envVariables.ContainsKey(key))
            {
                return envVariables[key];
            }
            return null; // Variável de ambiente não encontrada
        }
    }
}
