# CarbonEmissions
Net 8 RestFull Api to Implement Companies registers about its carbon emissions activity

Just be sure your Msql version is configured in program.cs in the baackend
this line 
options.UseMySql(builder.Configuration.GetConnectionString("localConnection"), new MySqlServerVersion(new Version(8, 0, 35)));

user - pulpo 
passw - 123