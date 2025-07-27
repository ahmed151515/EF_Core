namespace L20_Transaction.Entities
{
	public class BankAccount
	{
		public int AccountId { get; set; }

		public string AccountHolder { get; set; }
		public decimal Balance { get; set; }

		public List<GLTransaction> GLTransactions { get; set; } = new();

		public void Deposit(decimal amount)
		{
			if (amount > 0)
			{
				Balance += amount;
				GLTransactions.Add(new(amount, DateTime.Now, "Deposit"));
			}
		}
		public void Withdraw(decimal amount)
		{
			if (amount > 0 && Balance >= amount)
			{
				Balance -= amount;
				GLTransactions.Add(new(-amount, DateTime.Now, "Withdraw"));
			}
		}
	}

}
