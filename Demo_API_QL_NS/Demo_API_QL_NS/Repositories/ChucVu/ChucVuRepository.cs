using Demo_API_QL_NS.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Demo_API_QL_NS.Repositories.ChucVu
{
    public class ChucVuRepository:IChucVuRepository
    {
        private readonly string _connectionString;

        public ChucVuRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<ChucVuDTO>> getAllChucVu()
        {
            DataTable dt = new DataTable();
            string sql = $@"select Id_CV,Ten_CV,Ten_PB,cv.CreatedDate from ChucVu cv join 
                            PhongBan pb on cv.Id_PB = pb.Id_PB where cv.DeletedDate is null";
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                var result = dt.AsEnumerable().Select(row => new ChucVuDTO
                {
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    Ten_PB = row["Ten_PB"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }

        }
        private Hashtable InitDataChucVu(ChucVuDTO nv, bool isUpdate = false)
        {
            Hashtable val = new Hashtable();
            val.Add("Ten_CV", nv.Ten_CV);
            val.Add("Id_PB", nv.Id_PB);
            if (!isUpdate)
            {
                val.Add("CreatedDate", DateTime.UtcNow);
            }
            return val;
        }
        public async Task<bool> CreateChucVu(ChucVuDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();


                    Hashtable val = InitDataChucVu(model);

                    string query = "set dateformat dmy insert into ChucVu (Ten_CV, Id_PB, CreatedDate) " +
                                   "values (@Ten_CV, @Id_PB, @CreatedDate)";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {

                        command.Parameters.AddWithValue("@Ten_CV", val["Ten_CV"]);
                        command.Parameters.AddWithValue("@Id_PB", val["Id_PB"]);
                        command.Parameters.AddWithValue("@CreatedDate", val["CreatedDate"]);

                        int affectedRows = await command.ExecuteNonQueryAsync();

                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Create Fail " + ex.Message);
                    return false;
                }
            }
        }
        public async Task<bool> UpdateChucVu(ChucVuDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    Hashtable val = InitDataChucVu(model, true);

                    string query = "set dateformat dmy update ChucVu set Ten_CV = @Ten_CV, Id_PB = @Id_PB " +
                                   "where Id_CV = @Id_CV";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@Ten_CV", val["Ten_CV"]);
                        command.Parameters.AddWithValue("@Id_PB", val["Id_PB"]);
                        command.Parameters.AddWithValue("@Id_CV", model.Id_CV);

                        int affectedRows = await command.ExecuteNonQueryAsync();

                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Update fail " + ex.Message);
                    return false;
                }
            }
        }
        public async Task<bool> DeleteChucVu(int id)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    string query = "update ChucVu set DeletedDate = @DeletedDate where Id_CV = @Id";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@DeletedDate", DateTime.UtcNow);
                        command.Parameters.AddWithValue("@Id", id);

                        int affectedRows = await command.ExecuteNonQueryAsync();

                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Delete fail " + ex.Message);
                    return false;
                }
            }
        }
        public async Task<IEnumerable<ChucVuDTO>> SearchTenChucVu(string Ten_CV)
        {
            DataTable dt = new DataTable();
            string sql = $@"select Id_CV,Ten_CV,Ten_PB,cv.CreatedDate from ChucVu cv join 
                            PhongBan pb on cv.Id_PB = pb.Id_PB where Ten_CV like N'%{Ten_CV}%' and cv.DeletedDate is null";
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                var result = dt.AsEnumerable().Select(row => new ChucVuDTO
                {
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    Ten_PB = row["Ten_PB"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }
        }
        public async Task<ChucVuDTO> getChucVuID(int Id_CV)
        {
            DataTable dt = new DataTable();
            string sql = $@"select ChucVu.* from ChucVu where Id_CV = {Id_CV}";
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                var result = dt.AsEnumerable().Select(row => new ChucVuDTO
                {
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                    DeletedDate = (row["DeletedDate"] != DBNull.Value) ? ((DateTime)row["DeletedDate"]).ToString("dd/MM/yyyy") : " ",
                }).FirstOrDefault();
                return await Task.FromResult(result);
            }
        }
        public async Task<int> CheckId_PB_ChucVu(int Id_PB)
        {
            DataTable dt = new DataTable();
            string sql = $@"select count(*) AS total
                    from ChucVu 
                    WHERE Id_PB = {Id_PB} and DeletedDate is null";

            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                int totalCVs = Convert.ToInt32(dt.Rows[0]["total"]);

                return totalCVs;
            }
        }
        public async Task<IEnumerable<ChucVuDTO>> getAllChucVu_IdPB(int Id_PB)
        {
            DataTable dt = new DataTable();
            string sql = $@"select ChucVu.* from ChucVu where Id_PB = {Id_PB} and DeletedDate is null";
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                var result = dt.AsEnumerable().Select(row => new ChucVuDTO
                {
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                    DeletedDate = (row["DeletedDate"] != DBNull.Value) ? ((DateTime)row["DeletedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }
        }
    }
}
