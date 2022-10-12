
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
	public class PersonnelRepository : EfEntityRepositoryBase<Personnel, ProjectDbContext>, IPersonnelRepository
	{
		public PersonnelRepository(ProjectDbContext context) : base(context)
		{
		}
	}
}
