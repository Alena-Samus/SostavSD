using Microsoft.AspNetCore.Identity;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Entities;

namespace SostavSD.Data
{
    public class DBInitializerWithUsers
    {
        private const string DefaultPassword = "12345678";

        public async static Task InitializeUsers(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SostavSDContext>();
            var _userManager = serviceProvider.GetService<UserManager<UserSostav>>();

            await AddRolesAsync(serviceProvider);
            await context.SaveChangesAsync();

            await AddUser("admin@test.com", Roles.AllRoles.ToArray(), _userManager, serviceProvider,"Admin");
            await AddUser("readonly@test.com", new[] { Roles.ReadOnly }, _userManager, serviceProvider,"ReadOnly");
            await AddUser("chief@test.com", new[] { Roles.Chief, }, _userManager, serviceProvider,"Chief");
            await AddUser("headofthegroup@test.com", new[] { Roles.HeadOfGroup, }, _userManager, serviceProvider, "HeadofTheGroup");
            await AddUser("estimator@test.com", new[] { Roles.Estimator, }, _userManager, serviceProvider, "Estimator");
            await AddUser("calculator@test.com", new[] { Roles.Calculator, }, _userManager, serviceProvider, "Calculator");
            await AddUser("secretary@test.com", new[] { Roles.Secretary, }, _userManager, serviceProvider, "Secretary");
            await AddUser("inspector@test.com", new[] { Roles.Inspector, }, _userManager, serviceProvider, "Inspector");

            await context.SaveChangesAsync();
            AddCompany(context);
        }

        private async static Task AddUser(string email, string[] roles, UserManager<UserSostav> userManager, IServiceProvider serviceProvider, string surname)
        {
            var user = Activator.CreateInstance<UserSostav>();

            user.UserName = email;
            user.Email = email;
            user.Surname= surname;
            var result = await userManager.CreateAsync(user, DefaultPassword);

            await AssignRoles(user.Email, roles, userManager);
        }

        private static async Task<IdentityResult> AssignRoles(string email, string[] roles, UserManager<UserSostav> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }

        private async static Task AddRolesAsync(IServiceProvider serviceProvider)
        {
            var roleMananger = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var roles = Roles.AllRoles;

            var existingRoles = roleMananger.Roles.ToList();


            foreach (string role in roles)
            {
                if (!existingRoles.Any(r => r.Name == role))
                {
                    await roleMananger.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static void AddCompany(SostavSDContext context)
        {
            // Look for any contract.
            if (context.company.Any())
            {
                return;   // DB has been seeded
            }

            var company = new Company[]
            {

                new Company {CompanyName="РУП \"Гомельэнерго\"",CompanyDetails="246050, г. Гомель, ул. Фрунзе, 9 " },
                new Company {CompanyName="РУП \"Минскэнерго\"",CompanyDetails="220033 г. Минск, ул. Аранская, 24" },
                new Company {CompanyName="РУП \"Гродноэнерго\"",CompanyDetails="230003, г. Гродно, проспект Космонавтов, 64" },
            };
            foreach (Company s in company)
            {
                context.company.Add(s);
            }
            context.SaveChanges();


            if (context.contract.Any())
            {
                return;   // DB has been seeded
            }
            var Contract = new Contract[]
            {

                new Contract{CompanyID = company.Single(i => i.CompanyName == "РУП \"Минскэнерго\"").CompanyID,ProjectName="Замена т/а ст.№1 типа ПТ-60-130/13, расположенного в главном корпусе филиала «Минская ТЭЦ-4» РУП «Минскэнерго»",Index="326",
                    Order="6-326",ContractNumber="062-88-22",ContractDate=DateTime.Parse("20.06.2022"),ContractDateEndOfWork=DateTime.Parse("11.01.2023"),City="Минск"},
                new Contract{CompanyID = company.Single(i => i.CompanyName =="РУП \"Гомельэнерго\"").CompanyID,ProjectName="Реконструкция здания котельной (инв. № 330/с-8008) расположенного по адресу: Мозырский район, Козенский сельсовет, 27/24",Index="123",
                    Order="6-123",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),ContractDateEndOfWork=DateTime.Parse("08.07.2023"),City="Мозырь" },
                new Contract{CompanyID = company.Single(i => i.CompanyName =="РУП \"Гродноэнерго\"").CompanyID,ProjectName="Гродненская ТЭЦ-2. Замена котлоагрегата БКЗ-320-140 ГМ  на котлоагрегат меньшей производительности",Index="427",
                    Order="6-427",ContractNumber="040-188-22",ContractDate=DateTime.Parse("24.07.2022"),ContractDateEndOfWork=DateTime.Parse("28.02.2023"),City="Гродно" },
                new Contract{CompanyID = company.Single(i => i.CompanyName == "РУП \"Минскэнерго\"").CompanyID, ProjectName="Реконструкция ВЛ-0,4 кВ от КТП-2,5,192,352,639 в н.п.Илья Вилейского района Минской области",Index="2116",
                    Order="6-2116",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),ContractDateEndOfWork=DateTime.Parse("20.12.2022"),City="Вилейка" },

            };
            foreach (Contract s in Contract)
            {
                context.contract.Add(s);
            }
            context.SaveChanges();
        }
    }
}

