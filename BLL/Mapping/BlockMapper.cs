using AutoMapper;
using BLL.DTOs.Block;
using DAL.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class BlockMapper
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<CreateBlockRequest, Block>().AfterMap((source, destination) => destination.Id = Guid.NewGuid());
        }
    }
}
