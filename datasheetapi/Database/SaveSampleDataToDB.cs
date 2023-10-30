

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
        //context.SaveChanges();

        var contracts = DummyData.GetContracts();
        context.Contracts.AddRange(contracts);
        //context.SaveChanges();

        var containers = DummyData.GetContainers();
        context.Containers.AddRange(containers);
        //context.SaveChanges();

        var containerReviews = DummyData.GetContainerReviews();
        context.ContainerReviews.AddRange(containerReviews);
        //context.SaveChanges();

        DummyData.GetTagDatas();
        context.SaveChanges();
    }
}
