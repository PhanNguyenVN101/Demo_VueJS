using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Demo_API_QL_NS.Repositories.NhanVien;
using Demo_API_QL_NS.Services.NhanVien;
using System.Net.Http;
using ServiceStack.Configuration;
using Demo_API_QL_NS.Repositories.PhongBan;
using Demo_API_QL_NS.Services.PhongBan;
using Demo_API_QL_NS.Repositories.ChucVu;
using Demo_API_QL_NS.Services.ChucVu;


using Demo_API_QL_NS.Repositories.TaiKhoan;
using Demo_API_QL_NS.Services.TaiKhoan;

namespace Demo_API_QL_NS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public class MyHttpClient
        {
            private readonly HttpClient _httpClient;


            public async Task<string> GetDefaultUrlContentAsync()
            {
                var response = await _httpClient.GetAsync("");
                return await response.Content.ReadAsStringAsync();
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                
            });
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddOptions();
            services.AddCors(p => p.AddPolicy("MyCors", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            #region add Repository
            services.AddTransient<INhanVienRepository, NhanVienRepository>();
            services.AddTransient<IPhongBanRepository, PhongBanRepository>();
            services.AddTransient<ITaiKhoanRepository, TaiKhoanRepository>();
            services.AddTransient<IChucVuRepository, ChucVuRepository>();

            #endregion

            #region add Services
            services.AddTransient<INhanVienService, NhanVienService>();
            services.AddTransient<IPhongBanService, PhongBanService>();
            services.AddTransient<ITaiKhoanService, TaiKhoanService>();
            services.AddTransient<IChucVuService, ChucVuService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo_API_QL_NS v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyCors");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
