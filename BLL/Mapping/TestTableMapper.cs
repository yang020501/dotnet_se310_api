using System;
using AutoMapper;
using BLL.DTOs.Blocks;
using BLL.DTOs.Test;
using CQRSHandler.Domains;
using DAL.Aggregates;

namespace BLL.Mapping
{
	public class TestTableMapper
	{
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<TestTableRecords, InsertTestTableResponseDto>();
        }
    }
}

