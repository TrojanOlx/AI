using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaceDlib.Core.Common
{
    public class UnderlineSplitContractResolver : DefaultContractResolver
    {

        public static JsonSerializerSettings GetSettings() {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new UnderlineSplitContractResolver()
            };
            return settings;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return CamelCaseToUnderlineSplit(propertyName);
        }
        // ContractResolver来实现实现属性的映射
        private string CamelCaseToUnderlineSplit(string name)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                var ch = name[i];
                if (char.IsUpper(ch) && i > 0)
                {
                    var prev = name[i - 1];
                    if (prev != '_')
                    {
                        if (char.IsUpper(prev))
                        {
                            if (i < name.Length - 1)
                            {
                                var next = name[i + 1];
                                if (char.IsLower(next))
                                {
                                    builder.Append('_');
                                }
                            }
                        }
                        else
                        {
                            builder.Append('_');
                        }
                    }
                }

                builder.Append(char.ToLower(ch));
            }

            return builder.ToString();
        }
    }
}
