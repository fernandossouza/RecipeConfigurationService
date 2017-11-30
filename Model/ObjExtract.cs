using System.Collections.Generic;
namespace recipeconfigurationservice.Model
{
    public class ObjExtract
    {
        public dynamic jsonExtractDynamic{get;set;}
        public Dictionary<string,string> dicExtract {get;set;}

        public ObjExtract ()
        {
            this.dicExtract = new Dictionary<string,string>();
        }
    }
}