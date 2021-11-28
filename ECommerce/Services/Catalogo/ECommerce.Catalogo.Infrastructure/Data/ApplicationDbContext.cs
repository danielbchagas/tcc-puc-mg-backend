﻿using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Produto> Produtos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamento
            modelBuilder.Entity<Produto>(p => 
            {
                p.ToTable("Produtos");

                p.HasKey(p => p.Id);

                p.Property(p => p.Descricao).HasColumnType("varchar(2000)").IsRequired();
                p.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
                p.Property(p => p.Imagem).HasColumnType("text").IsRequired(false);
                p.Property(p => p.Ativo).HasColumnType("bit").IsRequired();
                p.Property(p => p.Valor).HasColumnType("money").IsRequired();
                p.Property(p => p.DataCadastro).HasColumnType("datetime").IsRequired();
            });
            #endregion

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var rm = new ResourceManager(typeof(ImagensResource));

            var produtoCamisetaManUtd = new Produto(
                descricao: "Os Red Devils entram em campo e você fica na torcida devidamente uniformizado com a Camisa Manchester United Masculina da Adidas. Inspirada no uniforme de 1960, o manto titular da temporada 21/22 chega clean, num vermelho clássico do clube, com gola redonda e listras do jeito Adidas de torcer nos ombros. Semelhante ao modelo que os atletas usam nos jogos em Old Trafford, a peça é produzida em tecido leve e respirável que oferece caimento solto e confortável. Para te acompanhar na hora de bater uma bola com os amigos ou acompanhar as partidas do clube inglês, garanta já a sua Camisa Masculina do Manchester United e marque esse golaço.",
                nome: "Camiseta - Manchester United Football Club",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camisa_man_utd")),
                quantidade: 100,
                valor: 200.50m
            );
            var produtoJaquetaManUtd = new Produto(
                descricao: "O DNA do Manchester United e o estilo das Três Listras encontram-se nesta jaqueta de futebol. Apostando no vermelho, preto e branco, seu tecido de moletinho é macio em contato com a pele. O tecido AEROREADY que remove o suor mantém seu corpo seco, mesmo se aumentar a intensidade dos movimentos. O escudo bordado ajuda você a mostrar seu orgulho pelo clube em qualquer lugar. Este produto é feito com Primegreen, uma série de materiais reciclados de alta performance.",
                nome: "Jaqueta - Manchester United Football Club",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_man_utd")),
                quantidade: 250,
                valor: 300.50m
            );
            var produtoBoneManUtd = new Produto(
                descricao: "Turbine seu visual esportivo fora das quatro linhas usando o Boné Adidas do Manchester United. Produzido com material macio e resistente, o cap aba curva possui também proteção UV, sendo ideal para looks em dias mais quentes. O fecho strapback garante ajuste personalizado e confortável. Exiba sua torcida pelos Diabos Vermelhos com o Boné Manchester United da Adidas e marque esse golaço.  ",
                nome: "Boné - Manchester United Football Club",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_man_utd")),
                quantidade: 10,
                valor: 80.50m
            );
            var produtoBermudaAdidas = new Produto(
                descricao: "Com máximo conforto e liberdade de movimentos, conte com o Short Adidas Logo Masculino para te acompanhar nos treinos ou na corrida. É confeccionado em material macio ao toque da pele, garantindo frescor, ótimo caimento no corpo e bolso externo para guardar pequenos itens. Peça já o seu short Adidas e se mantenha sempre em movimento!",
                nome: "Bermuda - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_adidas")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoBermudaDcShoes = new Produto(
                descricao: "Uma homenagem a produtos de heavy metal, a camiseta de manga longa Screw Head Classic H apresenta uma versão de fogo do logotipo Classic H da HUF no lado esquerdo do peito, enquanto uma versão grande do logotipo é embelezada com uma caveira e parafusos cruzados impressos na parte de trás.Feito de algodão puro, a camiseta tem acabamento com chamas roxas em ambas as mangas. Camiseta de manga comprida 100% algodão pré-encolhido Serigrafia no lado esquerdo do peito, costas e ambas as mangas Etiqueta HUF tecida no decote interno Sobre a marca: HUF Seu fundador Keith Hufnagel cresceu andando de skate nas ruas arenosas no final dos anos 80, em Nova Iorque. Em 1992, Hufnagel mudou-se para San Francisco para prosseguir com a sua paixão pelo skate. Logo em seguida tornou-se profissional, onde teve a oportunidade rara e gratificante de viajar o mundo por meio de seu skate e da indústria que o apoiou. Trazido pela abordagem de “Do it Yourself”, que veio junto com o skate, Hufnagel viu uma conveniência de dar a volta a essa mesma comunidade que o havia ressuscitado, e abriu uma pequena loja em um bloco excêntrico no bairro de Tenderloin em San Francisco. Seu objetivo: reunir sob um mesmo teto as marcas mais respeitadas do skate e do streetwear. Ele nomeou a loja HUF, e rapidamente passou a ser reconhecido como o principal fornecedor de seu segmento.",
                nome: "Bermuda - Dc Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoCamisetaDcShoes = new Produto(
                descricao: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                nome: "Camiseta - Dc Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes")),
                quantidade: 10,
                valor: 75.50m
            );
            var produtoBoneAdidas = new Produto(
                descricao: "O Boné Adidas é o acessório perfeito para treinar ao ar livre! Seu visual esportivo garante ajuste perfeito à cabeça e proteção contra o sol, além de uma rápida absorção do suor.",
                nome: "Bone - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_adidas")),
                quantidade: 10,
                valor: 120.50m
            );
            var produtoTenisAdidas = new Produto(
                descricao: "Se você é iniciante na corrida e está buscando um tênis adequado para o seu treino, o novo Adidas Corerace é o que você estava procurando. Com um cabedal confeccionado com material têxtil, a parte de cima do calçado conta também com tramas em mesh que garantem circulação de ar e ótima respirabilidade entre uma passada e outra. A entressola em EVA e o solado de borracha de alta qualidade garantem amortecimento leve e ótima tração. Com um design moderno, o Tênis Adidas Masculino é opção para o treino na esteira, na rua ou, até mesmo, para usar de forma casual. Aproveite a oportunidade e garanta já o seu.",
                nome: "Tenis - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("tenis_adidas")),
                quantidade: 10,
                valor: 350.50m
            );
            var produtoCamisaGreenBayPackers = new Produto(
                descricao: "Desenvolvido por Designer Esportivo, a coleção apresenta um exclusivo e diferenciado modelo retroativo. Produto produzido em alta definição, com estampa digital e bordado industrial.O tecido é leve e com a tecnologia Truelife Dry, que transporta o suor para as camadas externas, mantendo a pele seca, proporcionado frescor e bem estar e Truelife UV com proteção solar.Tecido tecnológico de qualidade.TAMANHOS PADRÃO BRASILDimensões aproximadas do produto: Largura x Altura em cm.:P: 52 x 70 ¦ M: 54 x 72 ¦ G: 56 x 74 ¦ GG: 58 x 76 ¦ XG: 62 x 78INFORMAÇÕES DO PRODUTOTipo de Produto: Camisa gola VModelagem: NormalManga: com punhosComposição: Poliéster Garantido maior tempo de durabilidade das coresTipo de Tecido: Tecnológico Truelife Dry e Truelife UVEstampa: Logo Emborrachado Alto Relevo Direitos reservados:- Imagens ilustrativas, as cores podem variar conforme tonalidade de cada tela;- Produto exclusivo Rinno Force, não possui ligação com clubes ou entidades ainda que tenham semelhanças em cores ou elementos que compõe o produto.",
                nome: "Camisa - Green Bay Packers",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camisa_green_bay_packers")),
                quantidade: 10,
                valor: 400.50m
            );
            var produtoBoneGreenBayPackers = new Produto(
                descricao: "O Boné New Era 950 é o boné oficial da NFL 2018. Ele é o touchdown certo para elevar os traços urbanos de suas produções, com esportividade. Exiba sua paixão pelo futebol americano em seu visual e garanta personalidade.",
                nome: "Boné - Green Bay Packers",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_green_bay_packers")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoMeiaAdidas = new Produto(
                descricao: "Proteja seus pés com conforto usando a Meia Adidas. Fabricada com material respirável, é ideal para te acompanhar nos treinos diários.",
                nome: "Meia - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("meia_adidas")),
                quantidade: 10,
                valor: 20.50m
            );
            var produtoBermudaDcShoes2 = new Produto(
                descricao: "Uma homenagem a produtos de heavy metal, a camiseta de manga longa Screw Head Classic H apresenta uma versão de fogo do logotipo Classic H da HUF no lado esquerdo do peito, enquanto uma versão grande do logotipo é embelezada com uma caveira e parafusos cruzados impressos na parte de trás.Feito de algodão puro, a camiseta tem acabamento com chamas roxas em ambas as mangas. Camiseta de manga comprida 100% algodão pré-encolhido Serigrafia no lado esquerdo do peito, costas e ambas as mangas Etiqueta HUF tecida no decote interno Sobre a marca: HUF Seu fundador Keith Hufnagel cresceu andando de skate nas ruas arenosas no final dos anos 80, em Nova Iorque. Em 1992, Hufnagel mudou-se para San Francisco para prosseguir com a sua paixão pelo skate. Logo em seguida tornou-se profissional, onde teve a oportunidade rara e gratificante de viajar o mundo por meio de seu skate e da indústria que o apoiou. Trazido pela abordagem de “Do it Yourself”, que veio junto com o skate, Hufnagel viu uma conveniência de dar a volta a essa mesma comunidade que o havia ressuscitado, e abriu uma pequena loja em um bloco excêntrico no bairro de Tenderloin em San Francisco. Seu objetivo: reunir sob um mesmo teto as marcas mais respeitadas do skate e do streetwear. Ele nomeou a loja HUF, e rapidamente passou a ser reconhecido como o principal fornecedor de seu segmento.",
                nome: "Bermuda - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes_2")),
                quantidade: 15,
                valor: 200.50m
            );
            var produtoBoneDcShoes = new Produto(
                descricao: "Boné DC Shoes Five Panel é confeccionado em poliéster é super leve e confortável, possui modelagem 5 panel e aba reta, o fechamento com regulagem em strapback (fitinha) permite ajustálo melhor em sua cebeça. Composição: 100% Poliéster Fecho Strapback Aba Reta Logo DC em relevo.",
                nome: "Boné - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_dc_shoes")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoCamisetaDcShoes2 = new Produto(
                descricao: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                nome: "Camiseta - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_2")),
                quantidade: 10,
                valor: 80.00m
            );
            var produtoCamisetaDcShoes3 = new Produto(
                descricao: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                nome: "Camiseta - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_3")),
                quantidade: 10,
                valor: 90.50m
            );
            var produtoJaquetaDcShoes = new Produto(
                descricao: "JAQUETA DC SHOES DAGUP Esteja pronto para enfrentar o frio sem perder o estilo usando a Jaqueta Corta Vento DC Shoes Masculina. Fabricada com material leve, a peça é a escolha perfeita para os dias com temperatura mais baixa e chuvosos. O capuz com cordão ajustável e os bolsos nas laterais oferecem praticidade e conforto. Características Principais: Composição do material: 100% Poliéster. Garantia conta defeito de fabricação.",
                nome: "Jaqueta - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_dc_shoes")),
                quantidade: 10,
                valor: 200.00m
            );
            var produtoMeiaDcShoes = new Produto(
                descricao: "Composição:89% Algodão10% Poliamida1% Elastano",
                nome: "Meia - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("meia_dc_shoes")),
                quantidade: 10,
                valor: 20.50m
            );
            var produtoTenisDcShoes = new Produto(
                descricao: "Tênis DC Shoes Court Graffik TX  Black Grey White O Tênis DC Shoes Court Graffik TX tem o cabedal produzido em lona com detalhes em camurça, essa combinação de materiais garante maior durabilidade, solado 'Cup Sole' tecnologia que produz uma sola mais resistente exclusiva DC Shoes. Lingua em espuma almofadada de tecido leve para circulação de ar. Especificações do fabricante: Nome: Tênis DC Shoes Court Graffik TX  Black Grey White Código | SKU: DC024A-BGW Material: Lona e Camurça Solado: Borracha Vulcanizada.",
                nome: "Tênis - DC Shoes",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("tenis_dc_shoes")),
                quantidade: 10,
                valor: 250.50m
            );
            var produtoJaquetaGreenBayPackers = new Produto(
                descricao: "Composição:89% Algodão10% Poliamida1% Elastano",
                nome: "Meia - Adidas",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_green_bay_packers")),
                quantidade: 10,
                valor: 20.50m
            );

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
                produtoMeiaAdidas,
                produtoBermudaDcShoes2,
                produtoBoneDcShoes,
                produtoCamisetaDcShoes2,
                produtoCamisetaDcShoes3,
                produtoJaquetaDcShoes,
                produtoMeiaDcShoes,
                produtoTenisDcShoes,
                produtoJaquetaGreenBayPackers
            );
        }
    }
}
