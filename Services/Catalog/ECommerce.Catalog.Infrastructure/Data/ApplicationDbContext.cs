using ECommerce.Core.Contracts.Data;
using ECommerce.Core.Models.Catalog;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var rm = new ResourceManager(typeof(ImagensResource));
            
            modelBuilder.Entity<Product>().HasData(
                new Product(
                    description: "Os Red Devils entram em campo e você fica na torcida devidamente uniformizado com a Camisa Manchester United Masculina da Adidas. Inspirada no uniforme de 1960, o manto titular da temporada 21/22 chega clean, num vermelho clássico do clube, com gola redonda e listras do jeito Adidas de torcer nos ombros. Semelhante ao modelo que os atletas usam nos jogos em Old Trafford, a peça é produzida em tecido leve e respirável que oferece caimento solto e confortável. Para te acompanhar na hora de bater uma bola com os amigos ou acompanhar as partidas do clube inglês, garanta já a sua Camisa Masculina do Manchester United e marque esse golaço.",
                    name: "Camiseta - Manchester United Football Club",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camisa_man_utd")),
                    quantity: 100,
                    value: 200.50m
                ),
                new Product(
                    description: "O DNA do Manchester United e o estilo das Três Listras encontram-se nesta jaqueta de futebol. Apostando no vermelho, preto e branco, seu tecido de moletinho é macio em contato com a pele. O tecido AEROREADY que remove o suor mantém seu corpo seco, mesmo se aumentar a intensidade dos movimentos. O escudo bordado ajuda você a mostrar seu orgulho pelo clube em qualquer lugar. Este produto é feito com Primegreen, uma série de materiais reciclados de alta performance.",
                    name: "Jaqueta - Manchester United Football Club",
                    image: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_man_utd")),
                    quantity: 100,
                    value: 300.50m
                ),
                new Product(
                    description: "Turbine seu visual esportivo fora das quatro linhas usando o Boné Adidas do Manchester United. Produzido com material macio e resistente, o cap aba curva possui também proteção UV, sendo ideal para looks em dias mais quentes. O fecho strapback garante ajuste personalizado e confortável. Exiba sua torcida pelos Diabos Vermelhos com o Boné Manchester United da Adidas e marque esse golaço.  ",
                    name: "Boné - Manchester United Football Club",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bone_man_utd")),
                    quantity: 100,
                    value: 80.50m
                ),
                new Product(
                    description: "Com máximo conforto e liberdade de movimentos, conte com o Short Adidas Logo Masculino para te acompanhar nos treinos ou na corrida. É confeccionado em material macio ao toque da pele, garantindo frescor, ótimo caimento no corpo e bolso externo para guardar pequenos itens. Peça já o seu short Adidas e se mantenha sempre em movimento!",
                    name: "Bermuda - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bermuda_adidas")),
                    quantity: 100,
                    value: 150.50m
                ),
                new Product(
                    description: "Uma homenagem a produtos de heavy metal, a camiseta de manga longa Screw Head Classic H apresenta uma versão de fogo do logotipo Classic H da HUF no lado esquerdo do peito, enquanto uma versão grande do logotipo é embelezada com uma caveira e parafusos cruzados impressos na parte de trás.Feito de algodão puro, a camiseta tem acabamento com chamas roxas em ambas as mangas. Camiseta de manga comprida 100% algodão pré-encolhido Serigrafia no lado esquerdo do peito, costas e ambas as mangas Etiqueta HUF tecida no decote interno Sobre a marca: HUF Seu fundador Keith Hufnagel cresceu andando de skate nas ruas arenosas no final dos anos 80, em Nova Iorque. Em 1992, Hufnagel mudou-se para San Francisco para prosseguir com a sua paixão pelo skate. Logo em seguida tornou-se profissional, onde teve a oportunidade rara e gratificante de viajar o mundo por meio de seu skate e da indústria que o apoiou. Trazido pela abordagem de “Do it Yourself”, que veio junto com o skate, Hufnagel viu uma conveniência de dar a volta a essa mesma comunidade que o havia ressuscitado, e abriu uma pequena loja em um bloco excêntrico no bairro de Tenderloin em San Francisco. Seu objetivo: reunir sob um mesmo teto as marcas mais respeitadas do skate e do streetwear. Ele nomeou a loja HUF, e rapidamente passou a ser reconhecido como o principal fornecedor de seu segmento.",
                    name: "Bermuda - Dc Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes")),
                    quantity: 100,
                    value: 150.50m
                ),
                new Product(
                    description: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                    name: "Camiseta - Dc Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes")),
                    quantity: 100,
                    value: 75.50m
                ),
                new Product(
                    description: "O Boné Adidas é o acessório perfeito para treinar ao ar livre! Seu visual esportivo garante ajuste perfeito à cabeça e proteção contra o sol, além de uma rápida absorção do suor.",
                    name: "Bone - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bone_adidas")),
                    quantity: 100,
                    value: 120.50m
                ),
                new Product(
                    description: "Se você é iniciante na corrida e está buscando um tênis adequado para o seu treino, o novo Adidas Corerace é o que você estava procurando. Com um cabedal confeccionado com material têxtil, a parte de cima do calçado conta também com tramas em mesh que garantem circulação de ar e ótima respirabilidade entre uma passada e outra. A entressola em EVA e o solado de borracha de alta qualidade garantem amortecimento leve e ótima tração. Com um design moderno, o Tênis Adidas Masculino é opção para o treino na esteira, na rua ou, até mesmo, para usar de forma casual. Aproveite a oportunidade e garanta já o seu.",
                    name: "Tenis - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("tenis_adidas")),
                    quantity: 100,
                    value: 350.50m
                ),
                new Product(
                    description: "Desenvolvido por Designer Esportivo, a coleção apresenta um exclusivo e diferenciado modelo retroativo. Produto produzido em alta definição, com estampa digital e bordado industrial.O tecido é leve e com a tecnologia Truelife Dry, que transporta o suor para as camadas externas, mantendo a pele seca, proporcionado frescor e bem estar e Truelife UV com proteção solar.Tecido tecnológico de qualidade.TAMANHOS PADRÃO BRASILDimensões aproximadas do produto: Largura x Altura em cm.:P: 52 x 70 ¦ M: 54 x 72 ¦ G: 56 x 74 ¦ GG: 58 x 76 ¦ XG: 62 x 78INFORMAÇÕES DO PRODUTOTipo de Produto: Camisa gola VModelagem: NormalManga: com punhosComposição: Poliéster Garantido maior tempo de durabilidade das coresTipo de Tecido: Tecnológico Truelife Dry e Truelife UVEstampa: Logo Emborrachado Alto Relevo Direitos reservados:- Imagens ilustrativas, as cores podem variar conforme tonalidade de cada tela;- Produto exclusivo Rinno Force, não possui ligação com clubes ou entidades ainda que tenham semelhanças em cores ou elementos que compõe o produto.",
                    name: "Camisa - Green Bay Packers",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camisa_green_bay_packers")),
                    quantity: 100,
                    value: 400.50m
                ),
                new Product(
                    description: "O Boné New Era 950 é o boné oficial da NFL 2018. Ele é o touchdown certo para elevar os traços urbanos de suas produções, com esportividade. Exiba sua paixão pelo futebol americano em seu visual e garanta personalidade.",
                    name: "Boné - Green Bay Packers",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bone_green_bay_packers")),
                    quantity: 100,
                    value: 150.50m
                ),
                new Product(
                    description: "Proteja seus pés com conforto usando a Meia Adidas. Fabricada com material respirável, é ideal para te acompanhar nos treinos diários.",
                    name: "Meia - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("meia_adidas")),
                    quantity: 100,
                    value: 20.50m
                ),
                new Product(
                    description: "Uma homenagem a produtos de heavy metal, a camiseta de manga longa Screw Head Classic H apresenta uma versão de fogo do logotipo Classic H da HUF no lado esquerdo do peito, enquanto uma versão grande do logotipo é embelezada com uma caveira e parafusos cruzados impressos na parte de trás.Feito de algodão puro, a camiseta tem acabamento com chamas roxas em ambas as mangas. Camiseta de manga comprida 100% algodão pré-encolhido Serigrafia no lado esquerdo do peito, costas e ambas as mangas Etiqueta HUF tecida no decote interno Sobre a marca: HUF Seu fundador Keith Hufnagel cresceu andando de skate nas ruas arenosas no final dos anos 80, em Nova Iorque. Em 1992, Hufnagel mudou-se para San Francisco para prosseguir com a sua paixão pelo skate. Logo em seguida tornou-se profissional, onde teve a oportunidade rara e gratificante de viajar o mundo por meio de seu skate e da indústria que o apoiou. Trazido pela abordagem de “Do it Yourself”, que veio junto com o skate, Hufnagel viu uma conveniência de dar a volta a essa mesma comunidade que o havia ressuscitado, e abriu uma pequena loja em um bloco excêntrico no bairro de Tenderloin em San Francisco. Seu objetivo: reunir sob um mesmo teto as marcas mais respeitadas do skate e do streetwear. Ele nomeou a loja HUF, e rapidamente passou a ser reconhecido como o principal fornecedor de seu segmento.",
                    name: "Bermuda - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes_2")),
                    quantity: 15,
                    value: 200.50m
                ),
                new Product(
                    description: "Boné DC Shoes Five Panel é confeccionado em poliéster é super leve e confortável, possui modelagem 5 panel e aba reta, o fechamento com regulagem em strapback (fitinha) permite ajustálo melhor em sua cebeça. Composição: 100% Poliéster Fecho Strapback Aba Reta Logo DC em relevo.",
                    name: "Boné - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("bone_dc_shoes")),
                    quantity: 100,
                    value: 150.50m
                ),
                new Product(
                    description: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                    name: "Camiseta - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_2")),
                    quantity: 100,
                    value: 80.00m
                ),
                new Product(
                    description: "CAMISETA DC SHOES Camiseta DC Shoes em modelagem regular, malha penteada fio 30.1 e silk frontal. Características Principais: Composição: 100% Algodão. Garantia conta defeito de fabricação.",
                    name: "Camiseta - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_3")),
                    quantity: 100,
                    value: 90.50m
                ),
                new Product(
                    description: "JAQUETA DC SHOES DAGUP Esteja pronto para enfrentar o frio sem perder o estilo usando a Jaqueta Corta Vento DC Shoes Masculina. Fabricada com material leve, a peça é a escolha perfeita para os dias com temperatura mais baixa e chuvosos. O capuz com cordão ajustável e os bolsos nas laterais oferecem praticidade e conforto. Características Principais: Composição do material: 100% Poliéster. Garantia conta defeito de fabricação.",
                    name: "Jaqueta - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_dc_shoes")),
                    quantity: 100,
                    value: 200.00m
                ),
                new Product(
                    description: "Composição:89% Algodão10% Poliamida1% Elastano",
                    name: "Meia - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("meia_dc_shoes")),
                    quantity: 100,
                    value: 20.50m
                ),
                new Product(
                    description: "Tênis DC Shoes Court Graffik TX  Black Grey White O Tênis DC Shoes Court Graffik TX tem o cabedal produzido em lona com detalhes em camurça, essa combinação de materiais garante maior durabilidade, solado 'Cup Sole' tecnologia que produz uma sola mais resistente exclusiva DC Shoes. Lingua em espuma almofadada de tecido leve para circulação de ar. Especificações do fabricante: Name: Tênis DC Shoes Court Graffik TX  Black Grey White Código | SKU: DC024A-BGW Material: Lona e Camurça Solado: Borracha Vulcanizada.",
                    name: "Tênis - DC Shoes",
                    image: Convert.ToBase64String((byte[])rm.GetObject("tenis_dc_shoes")),
                    quantity: 100,
                    value: 250.50m
                ),
                new Product(
                    description: "Desenvolvido por Designer Esportivo, a coleção apresenta um exclusivo e diferenciado modelo retroativo. Produto produzido em alta definição, as etiquetas são produzidas em micro-bordado HD de extrema qualidade. Ocapuz é forradoe com cadarço diferenciado. O casaco Moletom é peluciado no lado interno, misturandoo conforto do algodão e a durabilidade do poliéster presente na composição. Estilo, conforto e durabilidade. INFORMAÇÕES DO PRODUTO: Tipo de Produto: Casaco Moletom Referência:Green BayModelagem:Médio (Caso preferir usar mais solto, pedir um tamanho acima do usado normalmente)Manga:RaglanComposição: Algodão ePoliéster, garantindo maior tempo de durabilidade das coresBolsos: Frontal, Estilo CanguruTipo de Tecido: Moletom PeluciadoEspessura: Grosso 3 cabosEstampa: Logo emborrachado em alto relevo Dimensões aproximadas do produto. Largura x Altura em cm:P: 58 x 69 | M: 59 x 70 | G:60 x 71 | GG: 62 x 72 | XG: 66 x 74 Observação: As medidas podem variar em 3% para mais, ou menos nas peças. Direitos reservados:- Imagens ilustrativas, as cores podem variar conforme tonalidade de cada tela;- Produto exclusivo Rinno Force, não possui ligação com clubes ou entidades ainda que tenham semelhanças em cores ou elementos que compõe o produto;- É expressamente proibido a reprodução, uso das imagens, ou venda deste produto sem a autorização legal da marca, sujeito a sansões e responsabilidade de responder em juízo o uso indevido e sem permissão.",
                    name: "Jaqueta - Green Bay Packers - Adidas",
                    image: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_green_bay_packers")),
                    quantity: 100,
                    value: 20.50m
                ),
                new Product(
                    description: "Os fãs de futebol inglês, em especial dos Blues, não podem deixar de garantir a nova Camisa Chelsea Nike Masculina. O manto titular da temporada 21/22 traz inspiração nos modelos usados pelo clube anos anos 60 e conta com ziguezagues e xadrez, relembrando o movimento artístico Optical Art. Semelhante a peça que os atletas usam nos jogos em Stamford Bridge, essa camisa de futebol é produzida com tecido leve e respirável, vestindo os boleiros com muito conforto e estilo.",
                    name: "Camiseta - Chelsea Football Club",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camisa_chelsea")),
                    quantity: 100,
                    value: 200.50m
                ),
                new Product(
                    description: "Aumente sua coleção de camisas de futebol garantindo a nova Camisa Masculina do Manchester City. O manto titular da temporada 21/22 é semelhante ao modelo que os atletas usam nos jogos no Etihad Stadium, trazendo o azul tradicional num tecido leve e respirável que oferece ótimo caimento, priorizando a liberdade de movimentos. O brasão do clube inglês bordado na altura do peito deixa evidente sua torcida. Garanta já a sua Camisa Manchester City Masculina da Puma e marque esse golaço.",
                    name: "Camiseta - Manchester City Football Club",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camisa_man_city")),
                    quantity: 100,
                    value: 200.50m
                ),
                new Product(
                    description: "Desenvolvido por Designer Esportivo, a coleção apresenta um exclusivo e diferenciado modelo retroativo. Produto produzido em alta definição, com estampa digital e bordado industrial.O tecido é leve e com a tecnologia Truelife Dry, que transporta o suor para as camadas externas, mantendo a pele seca, proporcionado frescor e bem estar e Truelife UV com proteção solar.Tecido tecnológico de qualidade.TAMANHOS PADRÃO BRASILDimensões aproximadas do produto: Largura x Altura em cm.:P: 52 x 70 ¦ M: 54 x 72 ¦ G: 56 x 74 ¦ GG: 58 x 76 ¦ XG: 62 x 78INFORMAÇÕES DO PRODUTOTipo de Produto: Camisa gola VModelagem: NormalManga: com punhosComposição: Poliéster Garantido maior tempo de durabilidade das coresTipo de Tecido: Tecnológico Truelife Dry e Truelife UVEstampa: Logo Emborrachado Alto Relevo Direitos reservados:- Imagens ilustrativas, as cores podem variar conforme tonalidade de cada tela;- Produto exclusivo Rinno Force, não possui ligação com clubes ou entidades ainda que tenham semelhanças em cores ou elementos que compõe o produto;- É expressamente proibido a reprodução, uso das imagens, ou venda deste produto sem a autorização legal da marca, sujeito a sansões e responsabilidade de responder em juízo o uso indevido e sem permissão.",
                    name: "Camiseta - Croácia",
                    image: Convert.ToBase64String((byte[])rm.GetObject("camisa_croacia")),
                    quantity: 100,
                    value: 150.00m
                )
            );
        }
    }
}
