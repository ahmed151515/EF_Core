namespace L19_Interceptors.Entities.Contract
{
	internal interface ISoftDeleteable
	{
		public bool IsDeleted { get; set; }
		public DateTime? DeleteDate { get; set; }

		public void Delete()
		{
			IsDeleted = true;
			DeleteDate = DateTime.Now;
		}
		public void UndoDelete()
		{
			IsDeleted = false;
			DeleteDate = null;
		}
	}
}

