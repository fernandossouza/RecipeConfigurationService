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
                var tranformClass = TransformFactory.Instance(configuration);
                if(tranformClass != null)
                    transformList.Add(tranformClass);
            }

            // Criando um Objeto de extração
            ObjExtract objExtract = new ObjExtract();
            objExtract.jsonExtractDynamic = jsonExtract;


            foreach(var transform in transformList)
            {
              transform.Extract(objExtract);
            }
            return objExtract.dicExtract;
        }

         public bool Loading(int loadId, Dictionary<string,string> dicExtract)
        {
            return true;

        }


    }
}