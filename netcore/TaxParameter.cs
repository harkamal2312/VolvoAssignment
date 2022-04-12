using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Congestion.Calculator
{
    internal class TaxParameter
    {
        public TaxParameter()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "TaxParameters.json");
            using var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            TaxParameters = JsonConvert.DeserializeObject<List<TaxSlabsModel>>(json);
        }

        public List<TaxSlabsModel> TaxParameters
        {
            get;
        } = null;
    }
}
