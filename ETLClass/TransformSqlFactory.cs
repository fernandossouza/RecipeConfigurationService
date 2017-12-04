using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;

namespace recipeconfigurationservice.ETLClass
{
    public abstract class TransformSqlFactory : TransformFactory
    {
         public static TransformSqlFactory InstanceSQL(ExtractConfiguration extractConfiguration,IJson jsonService)
        {

            switch(extractConfiguration.sqlConfiguration.typeDb)
            {
                case EtypeDb.PostgreSql:                    
                    return new TransformSqlPostgre(extractConfiguration.sqlConfiguration,jsonService);
            }
            return null;

        }

    }
}