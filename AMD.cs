1. 
 EdiDocument ediDocument = EdiDocument.Load("H:\\AMD.txt");

            string[,] result = new string[ediDocument.Segments.Count, 2];
            int i =0;
            foreach(var tt in ediDocument.Segments)
            {
                if (tt.Elements.Count > 1)
                {
                    result[i, 0] = tt.Elements[1].ToString();
                    if (tt.Elements.Count > 2)
                    {
                        result[i, 1] = tt.Elements[2].ToString();
                    }
                    else {
                        result[i, 1] = "";
                    }
                 }
                else {
                    result[i, 0] = "";
                }

                   
                
                i++;
            }
            ediDocument.Segments[1].Id.Equals("GS");
            ediDocument.Segments[3][05].Equals("20130101");
            ediDocument.Segments[3].Element(05).DateValue.Equals(new DateTime(2013, 1, 1));
            ediDocument.Segments[7].Element(04).RealValue.Equals(19.99m);
            ediDocument.Segments[8].Element(01).NumericValue(0).Equals(1);
2. 
 public static List<string> GetRefText(string filename)
        {
            XDocument document = XDocument.Load(filename);
            var selectedTypes = new string[] { "MWB", "TRV", "CAR" };
            var customQuery = from cust in document.Descendants("Reference")
                                       where selectedTypes.Contains(cust.Attribute("RefCode").Value)
                                       select cust.Value;
            return customQuery.ToList();
        }
3. 
[HttpPost]
[ActionName("XMLMethod")]
public string Post([FromBody]object value)
