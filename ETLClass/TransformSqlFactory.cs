using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;

namespace recipeconfigurationservice.ETLClass
{
    public abstract class TransformSqlFactory : TransformFactory
    {
         public static TransformSqlFactory InstanceSQL(ExtractConfiguration extractConfiguration,LoadConfiguration loadConfiguration
         ,IJson jsonService, string typeInstance)
        {
            if(typeInstance.ToLower() == "extract")
            {

                switch(extractConfiguration.sqlConfiguration.typeDb)
                {
                    case EtypeDb.PostgreSql:                    
                        return new TransformSqlPostgre(extractConfiguration.sqlConfiguration,null,jsonService);
                }
                return null;
            }
            else if(typeInstance.ToLower() == "load")
            {
                switch(loadConfiguration.sqlLoad.typeDB)
                {
                    case EtypeDb.PostgreSql:                    
                        return new TransformSqlPostgre(null,loadConfiguration.sqlLoad,jsonService);
                }
                return null;
            }
            return null;

        }

    }
}