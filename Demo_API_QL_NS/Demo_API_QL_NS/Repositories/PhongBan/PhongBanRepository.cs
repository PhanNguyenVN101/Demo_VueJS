using Demo_API_QL_NS.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Demo_API_QL_NS.Repositories.PhongBan
{
    public class PhongBanRepository:IPhongBanRepository
    {
        private readonly string _connectionString;

        public PhongBanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<PhongBanDTO>> getAllPhongBan()
        {
            DataTable dt = new DataTable();
            string sql = $@"select PhongBan.* from PhongBan where DeletedDate is null";
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
                var result = dt.AsEnumerable().Select(row => new PhongBanDTO
                {
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_PB = row["Ten_PB"].ToString(),
                    ViTri_PB = row["ViTri_PB"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }

        }
        private Hashtable InitDataPhongBan(PhongBanDTO nv, bool isUpdate = false)
        {
            Hashtable val = new Hashtable();
            val.Add("Ten_PB", nv.Ten_PB);
            val.Add("ViTri_PB", nv.ViTri_PB);
            if (!isUpdate)
            {
                val.Add("CreatedDate", DateTime.UtcNow);
            }
            return val;
        }
        public async Task<bool> CreatePhongBan(PhongBanDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();


                    Hashtable val = InitDataPhongBan(model);

                    string query = "set dateformat dmy insert into PhongBan (Ten_PB, ViTri_PB, CreatedDate) " +
                                   "values (@Ten_PB, @ViTri_PB, @CreatedDate)";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {

                        command.Parameters.AddWithValue("@Ten_PB", val["Ten_PB"]);
                        command.Parameters.AddWithValue("@ViTri_PB", val["ViTri_PB"]);
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
        public async Task<bool> UpdatePhongBan(PhongBanDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    Hashtable val = InitDataPhongBan(model, true);

                    string query = "set dateformat dmy update PhongBan set Ten_PB = @Ten_PB, ViTri_PB = @ViTri_PB " +
                                   "where Id_PB = @Id_PB";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@Ten_PB", val["Ten_PB"]);
                        command.Parameters.AddWithValue("@ViTri_PB", val["ViTri_PB"]);
                        command.Parameters.AddWithValue("@Id_PB", model.Id_PB);

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
        public async Task<bool> DeletePhongBan(int id)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    string query = "update PhongBan set DeletedDate = @DeletedDate where Id_PB = @Id";

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
        public async Task<IEnumerable<PhongBanDTO>> SearchTenPhongBan(string Ten_PB)
        {
            DataTable dt = new DataTable();
            string sql = $@"select PhongBan.* from PhongBan where Ten_PB like N'%{Ten_PB}%' and DeletedDate is null";
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
                var result = dt.AsEnumerable().Select(row => new PhongBanDTO
                {
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_PB = row["Ten_PB"].ToString(),
                    ViTri_PB = row["ViTri_PB"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }
        }
        public async Task<PhongBanDTO> getPhongBanID(int Id_PB)
        {
            DataTable dt = new DataTable();
            string sql = $@"select PhongBan.* from PhongBan where Id_PB = {Id_PB}";
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
                var result = dt.AsEnumerable().Select(row => new PhongBanDTO
                {
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_PB = row["Ten_PB"].ToString(),
                    ViTri_PB = row["ViTri_PB"].ToString(),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                    DeletedDate = (row["DeletedDate"] != DBNull.Value) ? ((DateTime)row["DeletedDate"]).ToString("dd/MM/yyyy") : " ",
                }).FirstOrDefault();
                return await Task.FromResult(result);
            }
        }
    }
}
