

using datasheetapi.Repositories;

namespace api.Database;

public static class SaveSampleDataToDB
{

    public static void PopulateDb(DatabaseContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var projects = DummyData.GetProjects();
        context.Projects.AddRange(projects);
        context.SaveChanges();

        var contracts = DummyData.GetContracts();
        context.Contracts.AddRange(contracts);
        context.SaveChanges();

        var revisionContainers = DummyData.GetRevisionContainers();
        context.Containers.AddRange(revisionContainers);
        context.SaveChanges();

        DummyData.GetTagDatas();
        context.SaveChanges();
    }
}
