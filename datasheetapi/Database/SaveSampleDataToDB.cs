

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

        var contracts = DummyData.GetContracts();
        context.Contracts.AddRange(contracts);

        var containers = DummyData.GetContainers();
        context.Containers.AddRange(containers);

        var containerReviews = DummyData.GetContainerReviews();
        context.ContainerReviews.AddRange(containerReviews);

        DummyData.GetTagDatas();
        context.SaveChanges();
    }
}
