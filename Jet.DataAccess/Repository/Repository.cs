﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Jet.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet=_db.Set<T>();
			_db.FilmTable.Include(u => u.Category);
			_db.OrderTable.Include(u => u.User);
			_db.ApplicationUserTable.Include(u => u.Company);
			_db.FilmTable.Include(u => u.FilmImages);
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}
		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties=null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null) query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (string include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}
			return query.ToList();
		}

		public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (string include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}
			return query.FirstOrDefault();
		}
	}
}
