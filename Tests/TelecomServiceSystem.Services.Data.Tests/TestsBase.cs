namespace TelecomServiceSystem.Services.Data.Tests
{
    using System.Reflection;

    using TelecomServiceSystem.Services.Data.Tests.TestsModels;
    using TelecomServiceSystem.Services.Mapping;
    using TelecomServiceSystem.Services.Models.Bills;

    public class TestsBase
    {
        public TestsBase()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(BillModel).GetTypeInfo().Assembly,
                typeof(BillViewModel).GetTypeInfo().Assembly);
        }
    }
}
