using Demo_API_QL_NS.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Demo_API_QL_NS.Repositories.TaiKhoan
{
    public class TaiKhoanRepository:ITaiKhoanRepository
    {
        private readonly string _connectionString;

        public TaiKhoanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<TaiKhoanDTO>> getAllTaiKhoan()
        {
            DataTable dt = new DataTable();
            string sql = $@"select TaiKhoan.* from TaiKhoan where DeletedDate is null";
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
                var result = dt.AsEnumerable().Select(row => new TaiKhoanDTO
                {

                    Ten_TK = row["Ten_TK"].ToString(),
                    Id_NV = Convert.ToInt32(row["Id_NV"].ToString()),
                    //Id_NQ = Convert.ToInt32(row["Id_NQ"].ToString()),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }

        }
        private Hashtable InitDataTaiKhoan(TaiKhoanDTO tk, bool isUpdate = false)
        {
            Hashtable val = new Hashtable();
            val.Add("Ten_TK", tk.Ten_TK);
            val.Add("MK_TK", HashPassword(tk.MK_TK));
            if (tk.Id_NV == 0)
                tk.Id_NV = null;
            if (tk.Id_NQ == 0)
                tk.Id_NQ = null;
            val.Add("Id_NV", tk.Id_NV);
            val.Add("Id_NQ", tk.Id_NQ);
            if (!isUpdate)
            {
                val.Add("CreatedDate", DateTime.UtcNow);
            }
            return val;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public async Task<bool> CreateTaiKhoan(TaiKhoanDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();


                    Hashtable val = InitDataTaiKhoan(model);

                    string query = "set dateformat dmy insert into TaiKhoan (Ten_TK, MK_TK,Id_NV,Id_NQ, CreatedDate) " +
                                   "values (@Ten_TK, @MK_TK,@Id_NV,@Id_NQ, @CreatedDate)";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {

                        command.Parameters.AddWithValue("@Ten_TK", val["Ten_TK"]);
                        command.Parameters.AddWithValue("@MK_TK", val["MK_TK"]);

                        command.Parameters.AddWithValue("@Id_NV", val["Id_NV"] ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Id_NQ", val["Id_NQ"] ?? DBNull.Value);
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
        public async Task<bool> UpdateTaiKhoan(TaiKhoanDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    Hashtable val = InitDataTaiKhoan(model, true);

                    string query = "set dateformat dmy update TaiKhoan set MK_TK = @MK_TK, Id_NV = @Id_NV, Id_NQ = @Id_NQ " +
                                   "where Ten_TK = @Ten_TK";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@MK_TK", val["MK_TK"]);
                        command.Parameters.AddWithValue("@Id_NV", val["Id_NV"]);
                        command.Parameters.AddWithValue("@Id_NQ", val["Id_NQ"]);
                        command.Parameters.AddWithValue("@Ten_TK", model.Ten_TK);

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
        public async Task<bool> DeleteTaiKhoan(TaiKhoanDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    string query = "update TaiKhoan set DeletedDate = @DeletedDate where Ten_TK = @Ten_TK";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@DeletedDate", DateTime.UtcNow);
                        command.Parameters.AddWithValue("@Ten_TK", model.Ten_TK);

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
        public async Task<int> CheckId_NQ_TaiKhoan(int Id_NQ)
        {
            DataTable dt = new DataTable();
            string sql = $@"select count(*) AS total
                    from TaiKhoan 
                    WHERE Id_NQ = {Id_NQ} and DeletedDate is null";

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

                int totalTKs = Convert.ToInt32(dt.Rows[0]["total"]);

                return totalTKs;
            }
        }
        public async Task<int> CheckId_NV_TaiKhoan(int Id_NV)
        {
            DataTable dt = new DataTable();
            string sql = $@"select count(*) AS total
                    from TaiKhoan 
                    WHERE Id_NV = {Id_NV} and DeletedDate is null";

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

                int totalTKs = Convert.ToInt32(dt.Rows[0]["total"]);

                return totalTKs;
            }
        }
        

        public async Task<TaiKhoanDTO> getIdTaiKhoan(int Id_TK)
        {
            DataTable dt = new DataTable();
            string sql = $@"select TaiKhoan.* from TaiKhoan where Id_TK = {Id_TK}";
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
                var result = dt.AsEnumerable().Select(row => new TaiKhoanDTO
                {
                    Ten_TK = row["Ten_TK"].ToString(),
                    MK_TK = row["MK_TK"].ToString(),
                    Id_NV = Convert.ToInt32(row["Id_NV"].ToString()),
                    //Id_NQ = Convert.ToInt32(row["Id_NQ"].ToString()),
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                    DeletedDate = (row["DeletedDate"] != DBNull.Value) ? ((DateTime)row["DeletedDate"]).ToString("dd/MM/yyyy") : " ",
                }).FirstOrDefault();
                return await Task.FromResult(result);
            }
        }
    }
}
