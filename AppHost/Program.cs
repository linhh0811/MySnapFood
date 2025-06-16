
var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.Service_SnapFood_Api>("api");
builder.AddProject<Projects.Service_SnapFood_Client>("ui-client");
builder.AddProject<Projects.Service_SnapFood_Manage>("ui-manage");
builder.Build().Run();
