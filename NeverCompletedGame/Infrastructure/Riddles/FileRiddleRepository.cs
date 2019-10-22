using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Playing;

namespace Infrastructure.Riddles
{
    public class FileRiddleRepository : IRiddleRepository
    {
        public Riddle GetRiddle(int level)
        {
            var ass = typeof(Riddle).GetTypeInfo().Assembly;
            using (Stream resource = ass.GetManifestResourceStream("Playing.riddles.json"))
            {
                using (var streamReader = new StreamReader(resource))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        
                        JsonSerializer js = JsonSerializer.Create();
                        List<Riddle> riddles = js.Deserialize<List<Riddle>>(jsonTextReader);
                        object x = js.Deserialize(jsonTextReader);
                        Riddle riddle = riddles.FirstOrDefault(r => r.Level == level);
                        return riddle;
                    }
                }
            }
        }
    }
}
