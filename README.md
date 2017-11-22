# recipeconfigurationservice
API to Extract, Transformation and load of parameter in other API or Database. Used to create, update, read and delete configurations.

## Extract Configuration
This used for configuration of extraction information.
- extractId: Id of extract.
    - Integer
    - Ignored on Create, mandatory on the other methods
- name: name of extract
    - String(50 characters)
    - Mandatory
- enabled: status of status configuration
    - String(10 characters)
    - Mandatory
    - Values: true or false
- description: description of extract configuration
    - String(200 characters)
- extractConfiguration: List of configurations
- extractConfigurationId: Id of configuration
    - Integer
    - Ignored on Create
- name: name of configuration
  - String(50 characters)
  - Mandatory
- description: description of configuration
  - String(200 characters)
- type: type of configuration
  - Enum
  - Mandatory
  - Values: Api or SQL

## SQL Configuration
- sqlConfiguration: Object for configuration of connection SQL
- commandSQL: command for execute in database 
  - String (200 characters)
- stringConection: string for connection with database
  - String (100 characters)
- typeDb: type of database
  - Enum
  - Values: PostgreSql
- input: list parameter for execution of command sql or api, case not necessary not fill
- path: name of parameter
  - String (50 characters)
- type: type of parameter
  - String (10 characters)
  - Mandatory 
  - Values: static or dynamic
- value: value for input parameter, if the type is static
  - String (50 characters)
- output: list parameter for use on process transform and load
- localName: parameter for use on process transform and load, must not have repeated names in the same extraction
- type: type of parameter
  - String (10 characters)
  - Mandatory 
  - Values: static or dynamic
- path: name of parameter output of command sql
  - String (50 characters)
- value: value for output parameter, if the type is static
  - String (50 characters)

  ## API Configuration
- apiConfiguration: Object for configuration of API of extract
- endPoint: endpoint of API
  - String (100 characters)
- method: method for extract
  - String (10 characters)
  - values: GET,PUT,POST or DELETE
- sqlConfiguration: Object for configuration of connection SQL
- commandSQL: command for execute in database 
  - String (200 characters)
- input: list parameter for execution of command sql or api, case not necessary not fill
- path: name of parameter
  - String (50 characters)
- type: type of parameter
  - String (10 characters)
  - Mandatory 
  - Values: static or dynamic
- value: value for input parameter, if the type is static
  - String (50 characters)
- output: list parameter for use on process transform and load
- localName: parameter for use on process transform and load, must not have repeated names in the same extraction
- type: type of parameter
  - String (10 characters)
  - Mandatory 
  - Values: static or dynamic
- path: name of parameter output of command sql
  - String (50 characters)
- value: value for output parameter, if the type is static
  - String (50 characters)

### JSON Example SQL Configuration:
'''json
    {
        "extractId": 1,
        "name": "Nome Novo2",
        "enabled": "true",
        "description": "Desc teste update",
        "extractConfiguration": [
            {
                "extractConfigurationId": 1,
                "name": "Configuração 1",
                "description": "Descrição",
                "type": "sql3",
                "sqlConfiguration":{ 
                	"commandSQL":"teste/da/String",
                	"stringConection":"string de conexão",
                	"typeDb":"Mssql",
                	"input": [
                    	{
                    		"path": "teste/teste2",
                        	"type": "fixo",
                        	"value": "20"
                    	}
            		],
                	"output": [
                    	{
                            "type": "fixo",
                        	"path": "path/teste",
                        	"localName": "nomeParametro",
                        	"value": null
                		}
                	]
                	
                }
            }
        ]
    }
'''
### JSON Example SQL Configuration:
'''json
    {
        "extractId": 1,
        "name": "Nome Novo2",
        "enabled": "true",
        "description": "Desc teste update",
        "extractConfiguration": [
            {
                "extractConfigurationId": 1,
                "name": "Configuração 1",
                "description": "Descrição",
                "type": "sql3",
                "sqlConfiguration":{ 
                	"commandSQL":"teste/da/String",
                	"stringConection":"string de conexão",
                	"typeDb":"Mssql",
                	"input": [
                    	{
                    		"path": "teste/teste2",
                        	"type": "fixo",
                        	"value": "20"
                    	}
            		],
                	"output": [
                    	{
                            "type": "fixo",
                        	"path": "path/teste",
                        	"localName": "nomeParametro",
                        	"value": null
                		}
                	]
                	
                }
            }
        ]
    }
'''