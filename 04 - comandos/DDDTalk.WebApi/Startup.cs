using DDDTalk.Dominio;
using DDDTalk.Dominio.Alunos;
using DDDTalk.Dominio.Aplicacao;
using DDDTalk.Dominio.Infra.Crosscutting;
using DDDTalk.Dominio.Infra.SqlServer.Dapper;
using DDDTalk.Dominio.Turmas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTalk.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new AppSettingsHelper(Configuration));
            services.AddScoped<IAlunosRepositorio, AlunosRepositorio>();
            services.AddScoped<ITurmasRepositorio, TurmasRepositorio>();
            services.AddScoped<INovoAlunoCommandHandler, GerenciadorComandosAluno>();
            services.AddScoped<IRealizarInscricaoCommandHandler, GerenciadorComandosAluno>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
