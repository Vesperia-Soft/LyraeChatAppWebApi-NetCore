﻿using LyraeChatApp.Domain.UnitOfWork;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.UnitOfWorkSql;

public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
{
    private SqlConnection _context { get; set; }
    private SqlTransaction _transaction { get; set; }
    public IUnitOfWorkRepository Repositories { get; set; }

    public UnitOfWorkSqlServerAdapter(string connectionString)
    {
        _context = new SqlConnection(connectionString);
        _context.Open();

        _transaction = _context.BeginTransaction();

        Repositories = new UnitOfWorkSqlServerRepository(_context, _transaction);
    }
    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
        }

        if (_context != null)
        {
            _context.Close();
            _context.Dispose();
        }

        Repositories = null;
    }

    public void SaveChanges()
    {
        _transaction.Commit();
    }
}
