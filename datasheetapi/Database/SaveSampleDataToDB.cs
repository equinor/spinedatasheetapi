

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
        context.RevisionContainers.AddRange(revisionContainers);
        context.SaveChanges();

        context.Reviewers.Add(new Reviewer
        {
            Id = new Guid("f4504605-96ca-475d-bbb5-ac65f4c4aae2"),
            Project = projects[0]
        });

        DummyData.GetTagDatas();
        context.SaveChanges();
    }
}
