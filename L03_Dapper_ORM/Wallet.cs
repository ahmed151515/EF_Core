﻿namespace L03_Dapper_ORM

{
	public class Wallet
	{
		public int Id { get; set; }
		public string? Holder { get; set; }
		public decimal? Balance { get; set; }

		public override string ToString()
		{
			return $"[{Id}] {Holder} ({Balance:C})";
		}
	}
}
