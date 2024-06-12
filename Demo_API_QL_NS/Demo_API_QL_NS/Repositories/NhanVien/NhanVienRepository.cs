using Demo_API_QL_NS.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Demo_API_QL_NS.Repositories.NhanVien
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly string _connectionString;

        public NhanVienRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<NhanVienDTO>> getAllNhanVien()
        {
            DataTable dt = new DataTable();
            string sql = $@" SELECT  Id_NV, HoTen_NV,GioiTinh_NV,DiaChi_NV,Email_NV,NgaySinh_NV,
                                Ten_PB,Ten_CV,nv.Id_PB,nv.Id_CV,nv.CreatedDate
                                FROM NhanVien nv JOIN PhongBan pb ON nv.Id_PB = pb.Id_PB JOIN  ChucVu cv ON nv.Id_CV = cv.Id_CV
                                WHERE nv.DeletedDate IS NULL";
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
                var result = dt.AsEnumerable().Select(row => new NhanVienDTO
                {
                    Id_NV = Convert.ToInt32(row["Id_NV"].ToString()),
                    HoTen_NV = row["HoTen_NV"].ToString(),
                    GioiTinh_NV = row["GioiTinh_NV"].ToString(),
                    DiaChi_NV = row["DiaChi_NV"].ToString(),
                    Email_NV = row["Email_NV"].ToString(),
                    Ten_PB = row["Ten_PB"].ToString(),
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    NgaySinh_NV = (row["NgaySinh_NV"] != DBNull.Value) ? ((DateTime)row["NgaySinh_NV"]).ToString("dd/MM/yyyy") : " ",
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }

        }
        private Hashtable InitDataNhanVien(NhanVienDTO nv,bool isUpdate = false)
        {
            Hashtable val = new Hashtable();
            val.Add("HoTen_NV", nv.HoTen_NV);
            val.Add("GioiTinh_NV", nv.GioiTinh_NV);
            val.Add("DiaChi_NV", nv.DiaChi_NV);
            val.Add("Email_NV", nv.Email_NV);
            val.Add("NgaySinh_NV", Convert.ToDateTime(nv.NgaySinh_NV));
            val.Add("Id_PB", nv.Id_PB);
            val.Add("Id_CV", nv.Id_CV);

            if (!isUpdate)
            {
                val.Add("CreatedDate", DateTime.UtcNow);
            }
            return val;
        }
        public async Task<bool> CreateNhanVien(NhanVienDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();


                    Hashtable val = InitDataNhanVien(model);

                    string query = " set dateformat dmy insert into NhanVien (HoTen_NV, GioiTinh_NV, DiaChi_NV, Email_NV, NgaySinh_NV, Id_PB, Id_CV, CreatedDate) " +
                                   "values (@HoTen, @GioiTinh, @DiaChi, @Email, @NgaySinh, @Id_PB, @Id_CV, @CreatedDate)";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {

                        command.Parameters.AddWithValue("@HoTen", val["HoTen_NV"]);
                        command.Parameters.AddWithValue("@GioiTinh", val["GioiTinh_NV"]);
                        command.Parameters.AddWithValue("@DiaChi", val["DiaChi_NV"]);
                        command.Parameters.AddWithValue("@Email", val["Email_NV"]);
                        command.Parameters.AddWithValue("@NgaySinh", val["NgaySinh_NV"]);
                        command.Parameters.AddWithValue("@CreatedDate", val["CreatedDate"]);
                        command.Parameters.AddWithValue("@Id_PB", val["Id_PB"]);
                        command.Parameters.AddWithValue("@Id_CV", val["Id_CV"]);
                        int affectedRows = await command.ExecuteNonQueryAsync();

                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Create Fail "+ex.Message);
                    return false;
                }
            }
        }
        public async Task<bool> UpdateNhanVien(NhanVienDTO model)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    Hashtable val = InitDataNhanVien(model,true);

                    string query = "set dateformat dmy update NhanVien set HoTen_NV = @HoTen, GioiTinh_NV = @GioiTinh, DiaChi_NV = @DiaChi, " +
                                   "Email_NV = @Email, NgaySinh_NV = @NgaySinh, Id_PB = @Id_PB, Id_CV = @Id_CV where Id_NV = @Id_NV";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@HoTen", val["HoTen_NV"]);
                        command.Parameters.AddWithValue("@GioiTinh", val["GioiTinh_NV"]);
                        command.Parameters.AddWithValue("@DiaChi", val["DiaChi_NV"]);
                        command.Parameters.AddWithValue("@Email", val["Email_NV"]);
                        command.Parameters.AddWithValue("@NgaySinh", val["NgaySinh_NV"]);
                        command.Parameters.AddWithValue("@Id_PB", val["Id_PB"]);
                        command.Parameters.AddWithValue("@Id_CV", val["Id_CV"]);
                        command.Parameters.AddWithValue("@Id_NV", model.Id_NV);

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
        public async Task<bool> DeleteNhanVien(int id)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();

                    string query = "update NhanVien set DeletedDate = @DeletedDate where Id_NV = @Id";

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
        public async Task<bool> DeleteNhanViens(List<int> ids)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                try
                {
                    await cnn.OpenAsync();
                    string query = $"UPDATE NhanVien SET DeletedDate = @DeletedDate WHERE Id_NV IN ({string.Join(",", ids)})";

                    using (SqlCommand command = new SqlCommand(query, cnn))
                    {
                        command.Parameters.AddWithValue("@DeletedDate", DateTime.UtcNow);

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



        public async Task<int> CheckId_PB_NhanVien(int Id_PB)
        {
            DataTable dt = new DataTable();
            string sql = $@"select count(*) AS total
                    from NhanVien 
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

                int totalEmployees = Convert.ToInt32(dt.Rows[0]["total"]);

                return totalEmployees;
            }
        }
        public async Task<int> CheckId_CV_NhanVien(int Id_CV)
        {
            DataTable dt = new DataTable();
            string sql = $@"select count(*) AS total
                    from NhanVien 
                    WHERE Id_CV = {Id_CV} and DeletedDate is null";

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

                int totalEmployees = Convert.ToInt32(dt.Rows[0]["total"]);

                return totalEmployees;
            }
        }
        public async Task<IEnumerable<NhanVienDTO>> SearchNhanVienHT(string HoTen)
        {
            DataTable dt = new DataTable();
            string sql = $@"select Id_NV,HoTen_NV,GioiTinh_NV,DiaChi_NV,Email_NV,NgaySinh_NV,Ten_PB,Ten_CV, nv.Id_PB, nv.CreatedDate
                              from NhanVien nv
                              join PhongBan pb on nv.Id_PB = pb.Id_PB
							  join ChucVu cv on nv.Id_CV = cv.Id_CV
                              where HoTen_NV like N'%{HoTen}%' and nv.DeletedDate is null";
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
                var result = dt.AsEnumerable().Select(row => new NhanVienDTO
                {
                    Id_NV = Convert.ToInt32(row["Id_NV"].ToString()),
                    HoTen_NV = row["HoTen_NV"].ToString(),
                    GioiTinh_NV = row["GioiTinh_NV"].ToString(),
                    DiaChi_NV = row["DiaChi_NV"].ToString(),
                    Email_NV = row["Email_NV"].ToString(),
                    Ten_PB = row["Ten_PB"].ToString(),
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Ten_CV = row["Ten_CV"].ToString(),
                    NgaySinh_NV = (row["NgaySinh_NV"] != DBNull.Value) ? ((DateTime)row["NgaySinh_NV"]).ToString("dd/MM/yyyy") : " ",
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                });
                return await Task.FromResult(result);
            }
        }
        public async Task<NhanVienDTO> getNhanVienID(int Id_NV)
        {
            DataTable dt = new DataTable();
            string sql = $@"select NhanVien.* from NhanVien where Id_NV = {Id_NV} ";
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
                var result = dt.AsEnumerable().Select(row => new NhanVienDTO
                {
                    Id_NV = Convert.ToInt32(row["Id_NV"].ToString()),
                    HoTen_NV = row["HoTen_NV"].ToString(),
                    GioiTinh_NV = row["GioiTinh_NV"].ToString(),
                    DiaChi_NV = row["DiaChi_NV"].ToString(),
                    Email_NV = row["Email_NV"].ToString(),
                    Id_PB = Convert.ToInt32(row["Id_PB"].ToString()),
                    Id_CV = Convert.ToInt32(row["Id_CV"].ToString()),
                    NgaySinh_NV = (row["NgaySinh_NV"] != DBNull.Value) ? ((DateTime)row["NgaySinh_NV"]).ToString("dd/MM/yyyy") : " ",
                    CreatedDate = (row["CreatedDate"] != DBNull.Value) ? ((DateTime)row["CreatedDate"]).ToString("dd/MM/yyyy") : " ",
                    DeletedDate = (row["DeletedDate"] != DBNull.Value) ? ((DateTime)row["DeletedDate"]).ToString("dd/MM/yyyy") : " ",
                }).FirstOrDefault();
                return await Task.FromResult(result);
            }
        }
    }
}
