using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public interface IRepositoryHelper
    {
        Task<int> ExecuteDeleteAsync(string procedureName, object parameters);
        Task<dynamic> QueryGetAll(string procedureName, object parameters);
    }

    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly IConfiguration _config;

        public RepositoryHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> ExecuteDeleteAsync(string procedureName, object parameters)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var object3 = new { a = "" };

                affectedRows = await connection.ExecuteAsync(procedureName,
                    parameters, commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<dynamic> QueryGetAll(string procedureName, object parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var object3 = new { a = "" };

                return await connection.QueryAsync(procedureName,
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
