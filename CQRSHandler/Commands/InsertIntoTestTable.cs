using System;
using CQRSHandler.Abstractions;

namespace CQRSHandler.Commands
{
	public class InsertIntoTestTable : ICommand
	{
		public string? TestColumn1 { get; set; }

		public string? TestColumn2 { get; set; }
	}
}

