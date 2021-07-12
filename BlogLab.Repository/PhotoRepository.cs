using BlogLab.Models.Account;
using BlogLab.Models.Photo;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public interface IPhotoRepository
    {
        public Task<Photo> InsertAsync(PhotoCreate photoCreate, int applicationUserId);
        public Task<Photo> GetAsync(int photoId);
        public Task<IReadOnlyList<Photo>> GetAllByUserIdAsync(int applicationUserId);
        public Task<int> DeleteAsync(int photoId);
    }

    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration _config;
        private readonly IRepositoryHelper _repositoryHelper;

        public PhotoRepository(
            IConfiguration config,
            IRepositoryHelper repositoryHelper
            )
        {
            _config = config;
            _repositoryHelper = repositoryHelper;
        }

        

        public async Task<int> DeleteAsync(int photoId)
        {
            //return await _repositoryHelper.ExecuteDeleteAsync("Photo_delete", new { PhotoId = photoId });

            int affectedRows = 0;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                affectedRows = await connection.ExecuteAsync("Photo_delete",
                    new { PhotoId = photoId }, commandType: CommandType.StoredProcedure);
            }

            return affectedRows;
        }

        public async Task<IReadOnlyList<Photo>> GetAllByUserIdAsync(int applicationUserId)
        {
            //List<Photo> photos = await _repositoryHelper.QueryGetAll("Photo_GetByUserId", new { ApplicationUserId = applicationUserId });
            //return photos;

            IEnumerable<Photo> photos;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                photos = await connection.QueryAsync<Photo>(
                    "Photo_GetByUserId",
                    new { ApplicationUserId = applicationUserId },
                    commandType: CommandType.StoredProcedure);
            }

            return photos.ToList();
        }

 
        public async Task<Photo> GetAsync(int photoId)
        {
            Photo photos;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                photos = await connection.QueryFirstOrDefaultAsync<Photo>(
                    "Photo_Get",
                    new { PhotoId = photoId },
                    commandType: CommandType.StoredProcedure);
            }

            return photos;
        }

        public async Task<Photo> InsertAsync(PhotoCreate photoCreate, int applicationUserId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("PublicId", typeof(string));
            dataTable.Columns.Add("ImageUrl", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            dataTable.Rows.Add(
                photoCreate.PublicId,
                photoCreate.ImageUrl,
                photoCreate.Description);

            int newPhotoId;

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                newPhotoId = 
                    await connection.ExecuteScalarAsync<int>(
                        "Photo_Insert",
                        new
                        {
                            Photo = dataTable.AsTableValuedParameter("dbo.PhotoType"),
                            ApplicationUserId = applicationUserId
                        },
                        commandType: CommandType.StoredProcedure);
            }

            Photo photo = await GetAsync(newPhotoId);

            return photo;
        }
    }
}
