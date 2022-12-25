using System;
using CQRSHandler.Abstractions;
using CQRSHandler.Commands;
using CQRSHandler.Domains;
using Dapper;

namespace CQRSHandler.Queries
{
	public class GetAllValueTestTable : IQuery<TestTableRecords>
	{
        
    }
}

