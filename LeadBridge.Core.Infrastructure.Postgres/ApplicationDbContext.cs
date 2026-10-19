namespace LeadBridge.Core.Infrastructure.Postgres;

using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Infrastructure.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions option)
        : base(option)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
    public DbSet<Reviews> Reviews => Set<Reviews>();
    public DbSet<BusinessType> BusinessTypes => Set<BusinessType>();
    public DbSet<Avatar> Avatars => Set<Avatar>();
    public DbSet<EmailConfirmation> EmailConfirmation => Set<EmailConfirmation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        SeedBusinessType(modelBuilder);
    }

    private static void SeedBusinessType(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessType>().HasData(
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                Name = "Великі СТО (мережеві)",
                Description = "Авторизовані сервісні центри та великі мережі технічного обслуговування.",
            },
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                Name = "Придорожні майстерні та шиномонтажі",
                Description = "Локальні точки швидкого ремонту та обслуговування.",
            },
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                Name = "Спеціалізовані вантажні сервіси",
                Description = "СТО, що спеціалізуються виключно на TIR та комерційному транспорті (TIR-сервіси).",
            },
            new
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                Name = "Логістичні компанії (Вантажоперевезення)",
                Description = "Компанії з власним парком фур та комерційних авто.",
            },
            new
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                Name = "Служби таксі та доставки",
                Description = "Компанії з великим парком легкових автомобілів (міська логістика).",
            },
            new
            {
                Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                Name = "Автобусні парки (Пасажирські перевезення)",
                Description = "Міжміські та міські пасажирські перевізники.",
            },
            new
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                Name = "Агрохолдинги",
                Description = "Великі підприємства з масивним парком важкої сільгосптехніки (трактори, комбайни).",
            },
            new
            {
                Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                Name = "Приватні фермерські господарства",
                Description = "Локальні фермери з невеликим парком техніки.",
            },
            new
            {
                Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                Name = "Будівельні підрядники",
                Description = "Компанії з важкою будівельною технікою (крани, бульдозери, самоскиди).",
            },
            new
            {
                Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                Name = "Прокат та оренда спецтехніки",
                Description = "Компанії, що здають техніку в оренду та самостійно її обслуговують.",
            },
            new
            {
                Id = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                Name = "Заводи (Важка промисловість)",
                Description = "Підприємства, що потребують індустріальних мастил для станків та конвеєрів.",
            },
            new
            {
                Id = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                Name = "Видобувна промисловість (Кар'єри, Шахти)",
                Description = "Підприємства з надважкою видобувною технікою.",
            },
            new
            {
                Id = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                Name = "Автомагазини та ринки",
                Description = "Оптові та дрібнооптові закупівлі для подальшого роздрібного перепродажу.",
            },
            new
            {
                Id = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                Name = "Локальні АЗС",
                Description = "Приватні автозаправні станції, що реалізують мастила.",
            },
            new
            {
                Id = Guid.Parse("70000000-0000-0000-0000-000000000001"),
                Name = "Державний сектор та Комунальні підприємства",
                Description = "Автодори, водоканали, прибирання сміття.",
            },
            new
            {
                Id = Guid.Parse("70000000-0000-0000-0000-000000000002"),
                Name = "Оборонний сектор та ЗСУ",
                Description = "Військові частини та обслуговування оборонної техніки.",
            });
    }
}