using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalogo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() 
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options) 
        {
            
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder.LogTo(Console.WriteLine);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=ProdutosDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamento
            modelBuilder.Entity<Produto>(p => 
            {
                p.ToTable("Produtos");

                p.HasKey(p => p.Id);

                p.Property(p => p.Marca).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Observacao).HasColumnType("varchar(200)").IsRequired(false);
                p.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
                p.Property(p => p.Vencimento).HasColumnType("date").IsRequired(false);
                p.Property(p => p.Fabricacao).HasColumnType("date").IsRequired(false);
                p.Property(p => p.Imagem).HasColumnType("image").IsRequired(false);
                p.Property(p => p.Lote).HasColumnType("varchar(10)").IsRequired(false);
                p.Property(p => p.Ativo).HasColumnType("bit").IsRequired();
                p.Property(p => p.Preco).HasColumnType("money").IsRequired();
            });

            modelBuilder.Entity<LogEvento>(le =>
            {
                le.ToTable("LogEventos");

                le.HasKey(le => le.Id);

                le.Property(le => le.Momento).HasColumnType("date").IsRequired();
                le.Property(l => l.EntidadeId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
                le.Property(l => l.UsuarioId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            });
            #endregion

            #region Seed
            #region Imagens em base64
            var imagemProdutoCamisetaManUtd = "PGh0bWwgbGFuZz0icHQtQlIiPjxoZWFkPjxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PVVURi04Ij48dGl0bGU+QXZpc28gZGUgcmVkaXJlY2lvbmFtZW50bzwvdGl0bGU+PHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiIHNyYz0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9GRDEyNkM0Mi1FQkZBLTRFMTItQjMwOS1CQjNGREQ3MjNBQzEvbWFpbi5qcz9hdHRyPThIUEVjNG9YNXE4V1oyV3BTVXQ0clhGVS1keHhiR2ZoZ1k4NmhyRDFVODVuanNNS2VDRG1XSnVlYlRNcUlkQmxtbHpCN3RFWGE4Q0ViN2dCeUF4YjNSZWtWYnQtM2lReG15TmR4VkdsZEtMYjVGUVpFYlVOaXIwaU5UeW9hejMwblBUTVMwLVR6NU14d1RFWXBaQ1lJdWs5cHI4UTBrSkNxZTdjN3dCRmxwdjUtWFNSbzB2aGFtRzdDV3FFdmtPVkZjdDk4bjRpZlRqMnkyc2FWSzdzcmYzWVh6cUxrazhHWk9iSXdscUJXdDl3ZHNqVjQ0bTlveklSMFppOTdGMzVBMklJU3VLckUtS3ZhQ0g1LTdpXzIxeW1aN0s1bkZFQzhNVmNDZ2w1OFh1a1BqTDE0bkN5aTlSTnpKd1FTQk1HUjhxRHc0YjFJQld5cnZCY1JiWVItdyIgY2hhcnNldD0iVVRGLTgiPjwvc2NyaXB0PjxsaW5rIHJlbD0ic3R5bGVzaGVldCIgY3Jvc3NvcmlnaW49ImFub255bW91cyIgaHJlZj0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9FM0U4OTM0Qy0yMzVBLTRCMEUtODI1QS0zNUEwODM4MUExOTEvYWJuL21haW4uY3NzP2F0dHI9YUhSMGNITTZMeTkzZDNjdVoyOXZaMnhsTG1OdmJTOTFjbXdfYzJFOWFTWjFjbXc5YUhSMGNITWxNMkVsTW1ZbE1tWjNkM2N1WVdScFpHRnpMbU52YlM1aWNpVXlabU5oYldsellTMHhMVzFoYm1Ob1pYTjBaWEl0ZFc1cGRHVmtMVEl3TFRJeEpUSm1SazAwTWpreUxtaDBiV3dtY0hOcFp6MUJUM1pXWVhjeVNtWm9YM0o0YVdOS05FZFVRMGhXZGtaa1gwSlNKblZ6ZEQweE5qSTNPVFF6TkRJeE9EazNNREF3Sm5OdmRYSmpaVDFwYldGblpYTW1ZMlE5ZG1abEpuWmxaRDB3UTBGdlVXcFNlSEZHZDI5VVEwMXBSM3BaU0hkclVFbERSbEZCUVVGQlFXUkJRVUZCUVVKQlNnIi8+PHN0eWxlPmJvZHksZGl2LGF7Zm9udC1mYW1pbHk6YXJpYWwsc2Fucy1zZXJpZn1ib2R5e2JhY2tncm91bmQtY29sb3I6I2ZmZjttYXJnaW4tdG9wOjNweH1kaXZ7Y29sb3I6IzAwMH1hOmxpbmt7Y29sb3I6IzRiMTFhOH1hOnZpc2l0ZWR7Y29sb3I6IzRiMTFhOH1hOmFjdGl2ZXtjb2xvcjojZWE0MzM1fWRpdi5teW1Hb3tib3JkZXItdG9wOjFweCBzb2xpZCAjZGFkY2UwO2JvcmRlci1ib3R0b206MXB4IHNvbGlkICNkYWRjZTA7YmFja2dyb3VuZDojZjhmOWZhO21hcmdpbi10b3A6MWVtO3dpZHRoOjEwMCV9ZGl2LmFYZ2FHYntwYWRkaW5nOjAuNWVtIDA7bWFyZ2luLWxlZnQ6MTBweH1kaXYuZlRrN3Zke21hcmdpbi1sZWZ0OjM1cHg7bWFyZ2luLXRvcDozNXB4fTwvc3R5bGU+PC9oZWFkPjxib2R5PjxkaXYgY2xhc3M9Im15bUdvIj48ZGl2IGNsYXNzPSJhWGdhR2IiPjxmb250IHN0eWxlPSJmb250LXNpemU6bGFyZ2VyIj48Yj5BdmlzbyBkZSByZWRpcmVjaW9uYW1lbnRvPC9iPjwvZm9udD48L2Rpdj48L2Rpdj48ZGl2IGNsYXNzPSJmVGs3dmQiPiZuYnNwO0EgcMOhZ2luYSBhbnRlcmlvciBlc3TDoSB0ZW50YW5kbyBsZXZhciB2b2PDqiBwYXJhIDxhIGhyZWY9Imh0dHBzOi8vd3d3LmFkaWRhcy5jb20uYnIvY2FtaXNhLTEtbWFuY2hlc3Rlci11bml0ZWQtMjAtMjEvRk00MjkyLmh0bWwiPmh0dHBzOi8vd3d3LmFkaWRhcy5jb20uYnIvY2FtaXNhLTEtbWFuY2hlc3Rlci11bml0ZWQtMjAtMjEvRk00MjkyLmh0bWw8L2E+Ljxicj48YnI+Jm5ic3A7U2Ugdm9jw6ogbsOjbyBxdWlzZXIgdmlzaXRhciBlc3NhIHDDoWdpbmEsIHBvZGVyw6EgPGEgaHJlZj0iIyIgaWQ9InRzdWlkMSI+dm9sdGFyIMOgIHDDoWdpbmEgYW50ZXJpb3I8L2E+LjxzY3JpcHQgbm9uY2U9IllRbC9OM2JQd2lHa3hKaGdJbTlsN3c9PSI+KGZ1bmN0aW9uKCl7dmFyIGlkPSd0c3VpZDEnOyhmdW5jdGlvbigpewpkb2N1bWVudC5nZXRFbGVtZW50QnlJZChpZCkub25jbGljaz1mdW5jdGlvbigpe3dpbmRvdy5oaXN0b3J5LmJhY2soKTtyZXR1cm4hMX07fSkuY2FsbCh0aGlzKTt9KSgpOyhmdW5jdGlvbigpe3ZhciBpZD0ndHN1aWQxJzt2YXIgY3Q9J29yaWdpbmxpbmsnO3ZhciBvaT0ndW5hdXRob3JpemVkcmVkaXJlY3QnOyhmdW5jdGlvbigpewpkb2N1bWVudC5nZXRFbGVtZW50QnlJZChpZCkub25tb3VzZWRvd249ZnVuY3Rpb24oKXt2YXIgYj1kb2N1bWVudCYmZG9jdW1lbnQucmVmZXJyZXIsYT13aW5kb3cmJndpbmRvdy5lbmNvZGVVUklDb21wb25lbnQ/ZW5jb2RlVVJJQ29tcG9uZW50OmVzY2FwZSxjPSIiO2ImJihjPWEoYikpOyhuZXcgSW1hZ2UpLnNyYz0iL3VybD9zYT1UJnVybD0iK2MrIiZvaT0iK2Eob2kpKyImY3Q9IithKGN0KTtyZXR1cm4hMX07fSkuY2FsbCh0aGlzKTt9KSgpOzwvc2NyaXB0Pjxicj48YnI+PGJyPjwvZGl2PjwvYm9keT48L2h0bWw+";
            var imagemprodutoJaquetaManUtd = "PGh0bWwgbGFuZz0icHQtQlIiPjxoZWFkPjxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PVVURi04Ij48dGl0bGU+QXZpc28gZGUgcmVkaXJlY2lvbmFtZW50bzwvdGl0bGU+PHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiIHNyYz0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9GRDEyNkM0Mi1FQkZBLTRFMTItQjMwOS1CQjNGREQ3MjNBQzEvbWFpbi5qcz9hdHRyPVVybzAwbUVUa0dfcWNPUW1uZVJqRUhPTTd4Wk5iOFJ1ZDFwaGZDVjdUWnBjaDJFRllIWEk3OWpTLUd6dThsRWN5Wjd1cUJmbXlCT1ppLW0tX1ZsSERGYjRzNWRiclIxeHREWWY3U3ZINWJ0NEhXMThBRURjOUpfQXZVcVoxam1idkZpQkpBUHBvemJQdGFNS0VrblBGMEp2ZUk1R0dXZDV5RXk3SFRTWkx4VTdGVEFvT2lBNTJ2eHVQT25MNl9OMkIzTWloSFhqREVuaFBpVG90V1ljcHI2NzMxSUdMTVpNT3p6Skdud1ZHV1BHUzg5RTZoQ1dnVzRudlFaekZvTE9fSFQ4S1dRckFwQzFObEx0WDZKNDZIejJDRkdhNE9LUlF3RnRqZEw2aUZSTFpjNldlajlHWW5mT3A0MGFieGYwYWRxejZhVENBTGREN2pMSFhTWHJWZVdmaU43T2txWWFBMzdER0lCM3N5NCIgY2hhcnNldD0iVVRGLTgiPjwvc2NyaXB0PjxsaW5rIHJlbD0ic3R5bGVzaGVldCIgY3Jvc3NvcmlnaW49ImFub255bW91cyIgaHJlZj0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9FM0U4OTM0Qy0yMzVBLTRCMEUtODI1QS0zNUEwODM4MUExOTEvYWJuL21haW4uY3NzP2F0dHI9YUhSMGNITTZMeTkzZDNjdVoyOXZaMnhsTG1OdmJTOTFjbXdfYzJFOWFTWjFjbXc5YUhSMGNITWxNMkVsTW1ZbE1tWjNkM2N1WVdScFpHRnpMbU52YlM1aWNpVXlabXBoY1hWbGRHRXRNeTF6ZEhKcGNHVnpMVzFoYm1Ob1pYTjBaWEl0ZFc1cGRHVmtKVEptUTFjM05qWTRMbWgwYld3bWNITnBaejFCVDNaV1lYY3lVelpFYjBaYWFHdFJhV2h0ZWtSemRuTlhhRmhvSm5WemREMHhOakkzT1RRek5qTTJNamN6TURBd0puTnZkWEpqWlQxcGJXRm5aWE1tWTJROWRtWmxKblpsWkQwd1EwRnZVV3BTZUhGR2QyOVVRMHhwTUhsUFZIZHJVRWxEUmxGQlFVRkJRV1JCUVVGQlFVSkJSQSIvPjxzdHlsZT5ib2R5LGRpdixhe2ZvbnQtZmFtaWx5OmFyaWFsLHNhbnMtc2VyaWZ9Ym9keXtiYWNrZ3JvdW5kLWNvbG9yOiNmZmY7bWFyZ2luLXRvcDozcHh9ZGl2e2NvbG9yOiMwMDB9YTpsaW5re2NvbG9yOiM0YjExYTh9YTp2aXNpdGVke2NvbG9yOiM0YjExYTh9YTphY3RpdmV7Y29sb3I6I2VhNDMzNX1kaXYubXltR297Ym9yZGVyLXRvcDoxcHggc29saWQgI2RhZGNlMDtib3JkZXItYm90dG9tOjFweCBzb2xpZCAjZGFkY2UwO2JhY2tncm91bmQ6I2Y4ZjlmYTttYXJnaW4tdG9wOjFlbTt3aWR0aDoxMDAlfWRpdi5hWGdhR2J7cGFkZGluZzowLjVlbSAwO21hcmdpbi1sZWZ0OjEwcHh9ZGl2LmZUazd2ZHttYXJnaW4tbGVmdDozNXB4O21hcmdpbi10b3A6MzVweH08L3N0eWxlPjwvaGVhZD48Ym9keT48ZGl2IGNsYXNzPSJteW1HbyI+PGRpdiBjbGFzcz0iYVhnYUdiIj48Zm9udCBzdHlsZT0iZm9udC1zaXplOmxhcmdlciI+PGI+QXZpc28gZGUgcmVkaXJlY2lvbmFtZW50bzwvYj48L2ZvbnQ+PC9kaXY+PC9kaXY+PGRpdiBjbGFzcz0iZlRrN3ZkIj4mbmJzcDtBIHDDoWdpbmEgYW50ZXJpb3IgZXN0w6EgdGVudGFuZG8gbGV2YXIgdm9jw6ogcGFyYSA8YSBocmVmPSJodHRwczovL3d3dy5hZGlkYXMuY29tLmJyL2phcXVldGEtMy1zdHJpcGVzLW1hbmNoZXN0ZXItdW5pdGVkL0NXNzY2OC5odG1sIj5odHRwczovL3d3dy5hZGlkYXMuY29tLmJyL2phcXVldGEtMy1zdHJpcGVzLW1hbmNoZXN0ZXItdW5pdGVkL0NXNzY2OC5odG1sPC9hPi48YnI+PGJyPiZuYnNwO1NlIHZvY8OqIG7Do28gcXVpc2VyIHZpc2l0YXIgZXNzYSBww6FnaW5hLCBwb2RlcsOhIDxhIGhyZWY9IiMiIGlkPSJ0c3VpZDEiPnZvbHRhciDDoCBww6FnaW5hIGFudGVyaW9yPC9hPi48c2NyaXB0IG5vbmNlPSJtMmtIRlhtRzk3MkRaMGRqVHVaa0ZBPT0iPihmdW5jdGlvbigpe3ZhciBpZD0ndHN1aWQxJzsoZnVuY3Rpb24oKXsKZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoaWQpLm9uY2xpY2s9ZnVuY3Rpb24oKXt3aW5kb3cuaGlzdG9yeS5iYWNrKCk7cmV0dXJuITF9O30pLmNhbGwodGhpcyk7fSkoKTsoZnVuY3Rpb24oKXt2YXIgaWQ9J3RzdWlkMSc7dmFyIGN0PSdvcmlnaW5saW5rJzt2YXIgb2k9J3VuYXV0aG9yaXplZHJlZGlyZWN0JzsoZnVuY3Rpb24oKXsKZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoaWQpLm9ubW91c2Vkb3duPWZ1bmN0aW9uKCl7dmFyIGI9ZG9jdW1lbnQmJmRvY3VtZW50LnJlZmVycmVyLGE9d2luZG93JiZ3aW5kb3cuZW5jb2RlVVJJQ29tcG9uZW50P2VuY29kZVVSSUNvbXBvbmVudDplc2NhcGUsYz0iIjtiJiYoYz1hKGIpKTsobmV3IEltYWdlKS5zcmM9Ii91cmw/c2E9VCZ1cmw9IitjKyImb2k9IithKG9pKSsiJmN0PSIrYShjdCk7cmV0dXJuITF9O30pLmNhbGwodGhpcyk7fSkoKTs8L3NjcmlwdD48YnI+PGJyPjxicj48L2Rpdj48L2JvZHk+PC9odG1sPg==";
            var imagemProdutoBoneManUtd = "PGh0bWwgbGFuZz0icHQtQlIiPjxoZWFkPjxtZXRhIGh0dHAtZXF1aXY9IkNvbnRlbnQtVHlwZSIgY29udGVudD0idGV4dC9odG1sOyBjaGFyc2V0PVVURi04Ij48dGl0bGU+QXZpc28gZGUgcmVkaXJlY2lvbmFtZW50bzwvdGl0bGU+PHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiIHNyYz0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9GRDEyNkM0Mi1FQkZBLTRFMTItQjMwOS1CQjNGREQ3MjNBQzEvbWFpbi5qcz9hdHRyPWZ1UXhWWXdTblI4S21zWWtlNWNuOHJXLUZ2Y3RGMGlXalktVU9JNXpfS0N3cWJYVEdlbDFZTXFiVzJzeVRWS0pDd2MtVE9Jb2M4N2poTlJhZmpNVkEteS1jM0NZS2FmRktJenJkNEh6R0RnNUJRTGFIc3F2UWRicGh2VnAxbTBzdVY0M3F3ZzB0ajdTaE5VUmsweHFTcmdZaGpJb3l1RmR2ai1LWHBaamZaak9BRXIzSHprX0RmdURERzlKZHZoUjk5T2k1djAwQ25UMVlJMkl1djVGaC1tZGVWYS1mS2dTandWVVZIM09sVE9xODdZN0FCdW9iY2xaYXdGWVNFOFl2ZUlWUzZsQWczOWNILUNWM2g1elE2WFEzT1hYd0pGVGxEZFhSRTJBemw0RlVpZ1ZrdHpYZFlpZmFwZkJYU3FOWFNWUUZrXy0zOWN1em1wNHpkNmk2QSIgY2hhcnNldD0iVVRGLTgiPjwvc2NyaXB0PjxsaW5rIHJlbD0ic3R5bGVzaGVldCIgY3Jvc3NvcmlnaW49ImFub255bW91cyIgaHJlZj0iaHR0cHM6Ly9tZS5raXMudjIuc2NyLmthc3BlcnNreS1sYWJzLmNvbS9FM0U4OTM0Qy0yMzVBLTRCMEUtODI1QS0zNUEwODM4MUExOTEvYWJuL21haW4uY3NzP2F0dHI9YUhSMGNITTZMeTkzZDNjdVoyOXZaMnhsTG1OdmJTOTFjbXdfYzJFOWFTWjFjbXc5YUhSMGNITWxNMkVsTW1ZbE1tWjNkM2N1Wm5WMFptRnVZWFJwWTNNdVkyOXRMbUp5SlRKbVltOXVaUzFoWkdsa1lYTXRiV0Z1WTJobGMzUmxjaTExYm1sMFpXUXRNM010ZG1WeWJXVnNhRzhtY0hOcFp6MUJUM1pXWVhjemFWUm5lbUZUTW1kbmR6UjFiMjVOUm1KNFVsSXRKblZ6ZEQweE5qSTNPVFF6TnpJMU9EazNNREF3Sm5OdmRYSmpaVDFwYldGblpYTW1ZMlE5ZG1abEpuWmxaRDB3UTBGdlVXcFNlSEZHZDI5VVEwcHBVamxaTTNoclVFbERSbEZCUVVGQlFXUkJRVUZCUVVKQlNnIi8+PHN0eWxlPmJvZHksZGl2LGF7Zm9udC1mYW1pbHk6YXJpYWwsc2Fucy1zZXJpZn1ib2R5e2JhY2tncm91bmQtY29sb3I6I2ZmZjttYXJnaW4tdG9wOjNweH1kaXZ7Y29sb3I6IzAwMH1hOmxpbmt7Y29sb3I6IzRiMTFhOH1hOnZpc2l0ZWR7Y29sb3I6IzRiMTFhOH1hOmFjdGl2ZXtjb2xvcjojZWE0MzM1fWRpdi5teW1Hb3tib3JkZXItdG9wOjFweCBzb2xpZCAjZGFkY2UwO2JvcmRlci1ib3R0b206MXB4IHNvbGlkICNkYWRjZTA7YmFja2dyb3VuZDojZjhmOWZhO21hcmdpbi10b3A6MWVtO3dpZHRoOjEwMCV9ZGl2LmFYZ2FHYntwYWRkaW5nOjAuNWVtIDA7bWFyZ2luLWxlZnQ6MTBweH1kaXYuZlRrN3Zke21hcmdpbi1sZWZ0OjM1cHg7bWFyZ2luLXRvcDozNXB4fTwvc3R5bGU+PC9oZWFkPjxib2R5PjxkaXYgY2xhc3M9Im15bUdvIj48ZGl2IGNsYXNzPSJhWGdhR2IiPjxmb250IHN0eWxlPSJmb250LXNpemU6bGFyZ2VyIj48Yj5BdmlzbyBkZSByZWRpcmVjaW9uYW1lbnRvPC9iPjwvZm9udD48L2Rpdj48L2Rpdj48ZGl2IGNsYXNzPSJmVGs3dmQiPiZuYnNwO0EgcMOhZ2luYSBhbnRlcmlvciBlc3TDoSB0ZW50YW5kbyBsZXZhciB2b2PDqiBwYXJhIDxhIGhyZWY9Imh0dHBzOi8vd3d3LmZ1dGZhbmF0aWNzLmNvbS5ici9ib25lLWFkaWRhcy1tYW5jaGVzdGVyLXVuaXRlZC0zcy12ZXJtZWxobyI+aHR0cHM6Ly93d3cuZnV0ZmFuYXRpY3MuY29tLmJyL2JvbmUtYWRpZGFzLW1hbmNoZXN0ZXItdW5pdGVkLTNzLXZlcm1lbGhvPC9hPi48YnI+PGJyPiZuYnNwO1NlIHZvY8OqIG7Do28gcXVpc2VyIHZpc2l0YXIgZXNzYSBww6FnaW5hLCBwb2RlcsOhIDxhIGhyZWY9IiMiIGlkPSJ0c3VpZDEiPnZvbHRhciDDoCBww6FnaW5hIGFudGVyaW9yPC9hPi48c2NyaXB0IG5vbmNlPSJibUtoWXY3aHVjUThFV3Q1THRxUWJnPT0iPihmdW5jdGlvbigpe3ZhciBpZD0ndHN1aWQxJzsoZnVuY3Rpb24oKXsKZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoaWQpLm9uY2xpY2s9ZnVuY3Rpb24oKXt3aW5kb3cuaGlzdG9yeS5iYWNrKCk7cmV0dXJuITF9O30pLmNhbGwodGhpcyk7fSkoKTsoZnVuY3Rpb24oKXt2YXIgaWQ9J3RzdWlkMSc7dmFyIGN0PSdvcmlnaW5saW5rJzt2YXIgb2k9J3VuYXV0aG9yaXplZHJlZGlyZWN0JzsoZnVuY3Rpb24oKXsKZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoaWQpLm9ubW91c2Vkb3duPWZ1bmN0aW9uKCl7dmFyIGI9ZG9jdW1lbnQmJmRvY3VtZW50LnJlZmVycmVyLGE9d2luZG93JiZ3aW5kb3cuZW5jb2RlVVJJQ29tcG9uZW50P2VuY29kZVVSSUNvbXBvbmVudDplc2NhcGUsYz0iIjtiJiYoYz1hKGIpKTsobmV3IEltYWdlKS5zcmM9Ii91cmw/c2E9VCZ1cmw9IitjKyImb2k9IithKG9pKSsiJmN0PSIrYShjdCk7cmV0dXJuITF9O30pLmNhbGwodGhpcyk7fSkoKTs8L3NjcmlwdD48YnI+PGJyPjxicj48L2Rpdj48L2JvZHk+PC9odG1sPg==";
            #endregion

            var produtoCamisetaManUtd = new Produto(
                marca: "Manchester United Football Club", 
                nome: "Camisete", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: imagemProdutoCamisetaManUtd, 
                observacao: null, 
                quantidade: 100, 
                preco: 200.50m
            );
            var produtoJaquetaManUtd = new Produto(
                marca: "Manchester United Football Club", 
                nome: "Jaqueta", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: imagemprodutoJaquetaManUtd, 
                observacao: null, 
                quantidade: 250, 
                preco: 300.50m
            );
            var produtoBoneManUtd =  new Produto(
                marca: "Manchester United Football Club", 
                nome: "Boné", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: imagemProdutoBoneManUtd, 
                observacao: null, 
                quantidade: 10, 
                preco: 80.50m
            );
            var produtoBermudaAdidas = new Produto(
                marca: "Adidas", 
                nome: "Bermuda", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoBermudaDcShoes = new Produto(
                marca: "Dc Shoes", 
                nome: "Bermuda", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoCamisetaDcShoes = new Produto(
                marca: "Dc Shoes", 
                nome: "Camiseta", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoBoneAdidas = new Produto(
                marca: "Adidas", 
                nome: "Bone", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoTenisAdidas = new Produto(
                marca: "Adidas", 
                nome: "Tenis", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoCamisaGreenBayPackers = new Produto(
                marca: "Green Bay Packers", 
                nome: "Camisa", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoBoneGreenBayPackers = new Produto(
                marca: "Green Bay Packers", 
                nome: "Bone", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoCanecaManUtd = new Produto(
                marca: "Manchester United Football Club", 
                nome: "Caneca", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );
            var produtoMeiaAdidas = new Produto(
                marca: "Adidas", 
                nome: "Meia", 
                lote: null, 
                fabricacao: DateTime.Now, 
                vencimento: null, 
                imagem: null, 
                observacao: null, 
                quantidade: 10, 
                preco: 20.50m
            );

            var usuarioId = Guid.NewGuid();

            modelBuilder.Entity<Produto>().HasData(
                produtoCamisetaManUtd,
                produtoJaquetaManUtd,
                produtoBoneManUtd,
                produtoBermudaAdidas,
                produtoBermudaDcShoes,
                produtoCamisetaDcShoes,
                produtoBoneAdidas,
                produtoTenisAdidas,
                produtoCamisaGreenBayPackers,
                produtoBoneGreenBayPackers,
                produtoCanecaManUtd,
                produtoMeiaAdidas
            );

            modelBuilder.Entity<LogEvento>().HasData(
                new LogEvento(entidadeId: produtoCamisetaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoJaquetaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBermudaAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBermudaDcShoes.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCamisetaDcShoes.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoTenisAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCamisaGreenBayPackers.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneGreenBayPackers.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCanecaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoMeiaAdidas.Id, usuarioId: usuarioId)
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
