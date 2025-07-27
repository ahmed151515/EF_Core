namespace L20_Transaction.Entities
{
	public class GLTransaction
	{
		public int Id { get; }
		public decimal Amount { get; }
		public DateTime CreatedAt { get; }
		public string Notes { get; }

		public GLTransaction(decimal amount, DateTime createdAt, string notes)
		{

			Amount = amount;
			CreatedAt = createdAt;
			Notes = notes;
		}
	}
}