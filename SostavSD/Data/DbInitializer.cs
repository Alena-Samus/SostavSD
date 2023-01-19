﻿using SostavSD.Models;
using System;
using System.Linq;

namespace SostavSD.Data
{
    public static class DbInitializer
    {
        public static void Initialize (SostavSDContext context)
        {
            context.Database.EnsureCreated();

            // Look for any contract.
            if (context.Contract.Any())
            {
                return;   // DB has been seeded
            }

            var Contract = new Contract[]
            {

                new Contract{ProjectName="Замена т/а ст.№1 типа ПТ-60-130/13, расположенного в главном корпусе филиала «Минская ТЭЦ-4» РУП «Минскэнерго»",Index="326",
                    Order="6-326",ContractNumber="062-88-22",ContractDate=DateTime.Parse("20.06.2022"),ContractDateEndOfWork=DateTime.Parse("11.01.2023"),City="Минск" },
                new Contract{ProjectName="Реконструкция здания котельной (инв. № 330/с-8008) расположенного по адресу: Мозырский район, Козенский сельсовет, 27/24",Index="123",
                    Order="6-123",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),ContractDateEndOfWork=DateTime.Parse("08.07.2023"),City="Мозырь" },
                new Contract{ProjectName="Гродненская ТЭЦ-2. Замена котлоагрегата БКЗ-320-140 ГМ  на котлоагрегат меньшей производительности",Index="427",
                    Order="6-427",ContractNumber="040-188-22",ContractDate=DateTime.Parse("24.07.2022"),ContractDateEndOfWork=DateTime.Parse("28.02.2023"),City="Гродно" },
                new Contract{ProjectName="Реконструкция ВЛ-0,4 кВ от КТП-2,5,192,352,639 в н.п.Илья Вилейского района Минской области",Index="2116",
                    Order="6-2116",ContractNumber="066-107-22",ContractDate=DateTime.Parse("08.07.2022"),ContractDateEndOfWork=DateTime.Parse("20.12.2022"),City="Вилейка" },
            };
            foreach (Contract s in Contract)
            {
                context.Contract.Add(s);
            }
            context.SaveChanges();
        }
    }
}
