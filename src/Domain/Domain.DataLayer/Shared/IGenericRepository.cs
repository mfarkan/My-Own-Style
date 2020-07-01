﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Shared
{
    public interface IGenericRepository : IDisposable
    {
        Task<int> CommitAsync();
        int Commit();

        void Add<T>(T entity) where T : EntityBase;
        void AddRange<T>(params T[] entities) where T : EntityBase;
        void Delete<T>(T entity) where T : EntityBase;
        void Update<T>(T entity) where T : EntityBase;

        IQueryable<T> Query<T>() where T : EntityBase;
    }
}
