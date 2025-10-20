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

			await AddUser("admin@test.com", Roles.AllRoles.ToArray(), _userManager, serviceProvider, "Admin", "7");
			await AddUser("readonly@test.com", new[] { Roles.ReadOnly }, _userManager, serviceProvider, "ReadOnly", "7");
			await AddUser("chief@test.com", new[] { Roles.Chief, }, _userManager, serviceProvider, "Chief", "7");
			await AddUser("headofthegroup@test.com", new[] { Roles.HeadOfGroup, }, _userManager, serviceProvider, "HeadofTheGroup", "1");
			await AddUser("estimator@test.com", new[] { Roles.Estimator, }, _userManager, serviceProvider, "Estimator", "1");
			await AddUser("calculator@test.com", new[] { Roles.Calculator, }, _userManager, serviceProvider, "Calculator", "6");
			await AddUser("secretary@test.com", new[] { Roles.Secretary, }, _userManager, serviceProvider, "Secretary", "7");
			await AddUser("inspector@test.com", new[] { Roles.Inspector, }, _userManager, serviceProvider, "Inspector", "7");
			await AddUser("cpe1@test.com", new[] { Roles.ReadOnly, }, _userManager, serviceProvider, "ГИП1", "8");
			await AddUser("cpe2@test.com", new[] { Roles.ReadOnly, }, _userManager, serviceProvider, "ГИП2", "8");
			await AddUser("calc1@test.com", new[] { Roles.Calculator, }, _userManager, serviceProvider, "Расчетчик1", "6");
			await AddUser("calc2@test.com", new[] { Roles.Calculator, }, _userManager, serviceProvider, "Расчетчик2", "6");

			await context.SaveChangesAsync();

			AddBuildingView(context);
			AddBuildingZone(context);
			AddStatus(context);
			AddStage(context);
			AddSourceOfFinancing(context);
			AddCompany(context);
			AddProject(context);
			AddDrawing(context);
			AddEstimate(context);

        }

		private async static Task AddUser(string email, string[] roles, UserManager<UserSostav> userManager, IServiceProvider serviceProvider, string surname, string group)
		{
			var user = Activator.CreateInstance<UserSostav>();

			user.UserName = email;
			user.Email = email;
			user.Surname = surname;
			user.GroupName = group;
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

				new Contract{CompanyID = company.Single(i => i.CompanyName == "РУП \"Минскэнерго\"").CompanyID,Index="326",
					Order="6-326",ContractNumber="062-88-22",ContractDate=DateTime.Parse("20.06.2022"),City="Минск"},
				new Contract{CompanyID = company.Single(i => i.CompanyName =="РУП \"Гомельэнерго\"").CompanyID,Index="123",
					Order="6-123",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),City="Мозырь" },
				new Contract{CompanyID = company.Single(i => i.CompanyName =="РУП \"Гродноэнерго\"").CompanyID,Index="427",
					Order="6-427",ContractNumber="040-188-22",ContractDate=DateTime.Parse("24.07.2022"),City="Гродно" },
				new Contract{CompanyID = company.Single(i => i.CompanyName == "РУП \"Минскэнерго\"").CompanyID, Index="2116",
					Order="6-2116",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),City="Вилейка" },

			};
			foreach (Contract s in Contract)
			{
				context.contract.Add(s);
			}
			context.SaveChanges();
		}

		public static void AddBuildingZone(SostavSDContext context)
		{
			if (context.buildingZone.Any())
			{
				return;
			}

			var zone = new BuildingZone[]
			{
				new BuildingZone {BuildingZoneName = "1 - город"},
				new BuildingZone {BuildingZoneName = "2 - село"},
				new BuildingZone {BuildingZoneName = "3 - г.Минск"},
			};
			foreach (BuildingZone item in zone)
			{
				context.buildingZone.Add(item);
			}
			context.SaveChanges();
		}

		public static void AddSourceOfFinancing(SostavSDContext context)
		{
			if (context.sourceOfFinacing.Any())
			{
				return;
			}

			var sources = new SourceOfFinacing[]
			{
				new SourceOfFinacing {SourceName = "бюджет"},
				new SourceOfFinacing {SourceName = "собственные средства"},
			};
			foreach (SourceOfFinacing item in sources)
			{
				context.sourceOfFinacing.Add(item);
			}
			context.SaveChanges();
		}
		public static void AddBuildingView(SostavSDContext context)
		{
			if (context.buildingView.Any())
			{
				return;
			}
			else
			{
				var views = new BuildingView[]
				{
					new BuildingView {BuildingViewName = "новое строительство"},
					new BuildingView {BuildingViewName = "модернизация"},
					new BuildingView {BuildingViewName = "реконструкция"},
				};

				foreach (BuildingView view in views)
				{
					context.buildingView.Add(view);
				}
				context.SaveChanges();
			}


		}
		public static void AddStage(SostavSDContext context)
		{
			if (context.designStage.Any())
			{
				return;
			}
			else
			{
				var stages = new DesignStage[]
				{
					new DesignStage {StageName = "Строительный проект"},
					new DesignStage {StageName = "Архитектурный проект"},
					new DesignStage {StageName = "Предпроектная документация"},
				};
				foreach (DesignStage item in stages)
				{
					context.designStage.Add(item);
				}
				context.SaveChanges();
			}
		}
		public static void AddStatus(SostavSDContext context)
		{
			if (context.status.Any())
			{
				return;
			}
			else
			{
				var statuses = new Status[]
				{
					new Status {StatusName = "в работе", IsProject = true, IsEstimate = true},
					new Status {StatusName = "на расчете", IsEstimate = true},
					new Status {StatusName = "на проверке", IsEstimate = true},
                    new Status {StatusName = "на исправлении", IsEstimate = true},
                    new Status {StatusName = "на утверждении", IsEstimate = true},
                    new Status {StatusName = "на оформлении", IsEstimate = true},
                    new Status {StatusName = "выпущено", IsProject = true, IsEstimate = true},
                    new Status {StatusName = "без смет", IsEstimate = true},
                    new Status {StatusName = "замечания экспертизы", IsProject = true, IsEstimate = true},
                    new Status {StatusName = "удалить", IsEstimate = true},
                    new Status {StatusName = "в план", IsDrawing = true},
                    new Status {StatusName = "согласовано", IsDrawing = true},
                    new Status {StatusName = "утверждено", IsDrawing = true},
                    new Status {StatusName = "аннулирована", IsEstimate = true},
                    new Status {StatusName = "отработано", IsEstimate = true},
                    new Status {StatusName = "замечания заказчика", IsProject = true, IsDrawing = true, IsEstimate = true},
                    new Status {StatusName = "на оформлении (нормоконтроль)", IsDrawing = true},
                    new Status {StatusName = "на исправлении (нормоконтроль)", IsDrawing = true},
                    new Status {StatusName = "передан в ПОС", IsProject = true},
                    new Status {StatusName = "в экспертизе", IsProject = true},
                    new Status {StatusName = "приостановлен", IsProject = true, IsEstimate = true},
                    new Status {StatusName = "архивировано", IsDrawing = true},
                    new Status {StatusName = "не требует архивации", IsDrawing = true},
                    new Status {StatusName = "на архивацию", IsDrawing = true},
                    new Status {StatusName = "не выпускать", IsEstimate = true},
                    new Status {StatusName = "в архив", IsProject = true},
                    new Status {StatusName = "передано в ПИР", IsProject = true},
                };
				foreach (Status item in statuses)
				{
					context.status.Add(item);
				}
				context.SaveChanges();
			}
		}

        public static void AddDeppart(SostavSDContext context)
        {
            if (context.deppart.Any())
            {
                return;
            }
            else
            {
                var depparts = new Deppart[]
                {
                    new Deppart {GroupName = "Электрики",  GroupANU = "1"},
                    new Deppart {GroupName = "Строители",  GroupANU = "2"},
                    new Deppart {GroupName = "Сантехники",  GroupANU = "3"},
                    new Deppart {GroupName = "Технологи",  GroupANU = "4"},
                    new Deppart {GroupName = "Расчетная",  GroupANU = "6"},
                    new Deppart {GroupName = "Руководство",  GroupANU = "7"},
                    new Deppart {GroupName = "ГИП",  GroupANU = "8"},
                };
				foreach (Deppart item in depparts)
				{ 
					context.deppart.Add(item);
				}

                context.SaveChanges();
            }
        }
        public static void AddProject(SostavSDContext context)
		{
			if (context.project.Any())
			{
				return;
			}
			else
			{
				context.project.Add(new Project { BuildingNumber = "1", ContractId = 1, StageId = 1 });
                context.project.Add(new Project { BuildingNumber = "2", ContractId = 2, StageId = 2 });
                context.project.Add(new Project { BuildingNumber = "3", ContractId = 3, StageId =3 });
                context.project.Add(new Project { BuildingNumber = "192/1", ContractId = 4, StageId = 1 });
                context.project.Add(new Project { BuildingNumber = "5", ContractId = 1, StageId = 2});
                context.project.Add(new Project { BuildingNumber = "777", ContractId = 1, StageId = 3 });
            }

			context.SaveChanges();
		}

        public static void AddDrawing(SostavSDContext context)
        {
            if (context.drawing.Any())
            {
                return;
            }
            else
            {
                var drawings = new Drawing[]
                {
                    new Drawing {DrawingName = "11111-Р-10000-ТМ8", ProjectId = 1},
                    new Drawing {DrawingName = "050-1-КЖ10", ProjectId = 2},
                    new Drawing {DrawingName = "17100-2001-ЭМ25 изм.1", ProjectId = 6},
                };
                foreach (Drawing item in drawings)
                {
                    context.drawing.Add(item);
                }
                context.SaveChanges();
            }
        }
        public static void AddEstimate(SostavSDContext context)
        {
            if (context.estimate.Any())
            {
                return;
            }
            else
            {
                var estimates = new Estimate[]
                {
                    new Estimate {EstimateNumber="14.1", EstimateCode="797-14-13", EstimateName="строительные работы", Other =145879, Equipment=1250 },
                    new Estimate {EstimateNumber="3-73-4003Е-В2/Д9", EstimateCode="751-1-36", EstimateName="демонтаж оборудования и трубопроводов"},
                    new Estimate {EstimateNumber="32", EstimateCode="7341-3-32", EstimateName="прокладку трубопроводов теплоснабжения", Total = 3477},

                };
                foreach (Estimate item in estimates)
                {
                    context.estimate.Add(item);
                }
                context.SaveChanges();
            }
        }
    }
}