using System;
using System.Collections.Generic;
using System.Linq;
using recipeconfigurationservice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using recipeconfigurationservice.Data;
using recipeconfigurationservice.Model;
using System.Threading.Tasks;

using recipeconfigurationservice.ETLClass.Interface;
using recipeconfigurationservice.ETLClass;

namespace recipeconfigurationservice.Services
{
    public class TransformService : ITransformService
    {
        private readonly ApplicationDbContext _context;

        public TransformService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IDictionary<string,string>> Extraction(int extractId, dynamic jsonExtract)
        {
            IExtractService extractService = new ExtractService(_context);

            var extract = await extractService.getExtract(extractId);

            List<TransformFactory> transformList = new List<TransformFactory>();

            foreach(var configuration in extract.extractConfiguration)
            {
                var tranformClass = TransformFactory.Instance(configuration,null,"extract");
                if(tranformClass != null)
                    transformList.Add(tranformClass);
            }

            // Criando um Objeto de extração
            ObjExtract objExtract = new ObjExtract();
            objExtract.jsonExtractDynamic = jsonExtract;

            List<Task<bool>> listTasks = new List<Task<bool>>();

            foreach(var transform in transformList)
            {
             listTasks.Add(transform.Extract(objExtract));
            }

            await Task.WhenAll(listTasks);


            await Loading(extract.extractId,objExtract.dicExtract);
           
            return objExtract.dicExtract;
        }

         public async Task<bool> Loading(int extractId, Dictionary<string,string> dicExtract)
        {
             ILoadService loadService = new LoadService(_context);
             //Load
            var loads = await loadService.getLoadsPerExtractId(extractId);
            // Lista com as classe de extração
            List<TransformFactory> transformList = new List<TransformFactory>();

            foreach( var load in loads)
            {
                foreach(var configuration in load.loadConfiguration)
                {
                    var tranformClass = TransformFactory.Instance(null,configuration,"load");
                    if(tranformClass != null)
                        transformList.Add(tranformClass);
                }
            }

             List<Task<bool>> listTasks = new List<Task<bool>>();

            foreach(var transform in transformList)
            {
             listTasks.Add(transform.Load(dicExtract));
            }

            await Task.WhenAll(listTasks);

            return true;

        }


    }
}