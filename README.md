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
nameParameter: name of the parameter of json dynamic input.
  - String (10 characters)
  - Mandatory 
  - Values: static or dynamic
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
```json
{
    "extractId": 1,
    "name": "Nome Novo2",
    "enabled": "true",
    "description": "Desc teste update",
    "extractConfiguration": [
        {
            "extractConfigurationId": 1,
            "name": "Iniciar receita no PLC Procedure",
            "description": "configuração de extract em procedure para iniciar uma receita no plc",
            "type": "sql",
            "sqlConfiguration": {
                "commandSQL": "exec Procedure",
                "stringConection": "local\\sqlexpress",
                "typeDb": 0,
                "input": [
                    {
                        "path": "Parametro2",
                        "nameParameter": null,
                        "type": "static",
                        "value": "teste"
                    },
                    {
                        "path": "Parametro1",
                        "nameParameter": null,
                        "type": "static",
                        "value": "20"
                    }
                ],
                "output": [
                    {
                        "path": null,
                        "localName": "tempoReceita",
                        "value": "50"
                    },
                    {
                        "path": "campo1",
                        "localName": "idReceita",
                        "value": null
                    }
                ]
            },
            "apiConfiguration": null
        }
        }
    ]
}
```
### JSON Example SQL Configuration:
```json
{
    "extractId": 1,
    "name": "Nome Novo2",
    "enabled": "true",
    "description": "Desc teste update",
    "extractConfiguration": [
        {
            "extractConfigurationId": 1,
            "name": "Iniciar receita no PLC API",
            "description": "configuração de extract em API para iniciar uma receita no plc",
            "type": "Api",
            "sqlConfiguration": null,
            "apiConfiguration": {
                "endPoint": "http://127.0.0.1:8032/api/things",
                "method": "get",
                "input": [
                    {
                        "path": null,
                        "nameParameter": "Id",
                        "type": "static",
                        "value": "2"
                    }
                ],
                "output": [
                    {
                        "path": "physicalConn",
                        "localName": "physicalConnectionThing",
                        "value": null
                    },
                    {
                        "path": "description",
                        "localName": "descriptionThing",
                        "value": null
                    }
                ]
            }
        }
    ]
}
```