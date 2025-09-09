using L19_Interceptors.Entities.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace L19_Interceptors.Data.Interceptors
{
	public class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			if (eventData.Context is null)
			{
				return result;
			}

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				//if (entry is null || entry.State != EntityState.Deleted || !(entry is not ISoftDeleteable entity)) // way 1
				if (entry is not { State: EntityState.Deleted, Entity: ISoftDeleteable entity })
				{
					continue;
				}
				entry.State = EntityState.Modified;
				entity.Delete();
			}
			return result;
		}
	}
}

