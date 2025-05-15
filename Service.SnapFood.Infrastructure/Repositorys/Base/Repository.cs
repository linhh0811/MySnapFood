﻿using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Repositorys.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }



        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate) ?? throw new Exception("Bản ghi không tồn tại");
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id) ?? throw new Exception("Bản ghi không tồn tại");
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id) ?? throw new Exception("Bản ghi không tồn tại");
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            return entities;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Where(criteria).ToList();
        }
    }
}