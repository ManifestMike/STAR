## Developer Machine Setup

### Code
The code is in a [git](https://git-scm.com/) repository.  The repository url is https://github.com/ManifestMike/STAR.git

### IDE Setup
- Visual Studio 2015 [Community Edition](https://www.visualstudio.com/) (or Professional, if you have it)

### Packages
- [NuGet](http://www.nuget.org) is used to deliver packages required by the server side code.  As long as your IDE is set to restore packages from the server (which is the default), all NuGet packages will be downloaded with your first build.
- [bower](http://bower.io) is used to manage client-side packages.  To use bower, you need to have [node.js](http://nodejs.org) installed, first.  Once node is installed, run `bower install` from the `trunk` directory.

### Database Setup

#### Database Engine
1. [Enable .NET Framework 3.5](https://msdn.microsoft.com/en-us/library/hh506443(v=vs.110).aspx)
1. Install [SQL Server Express with Tools](http://www.microsoft.com/en-us/server-cloud/products/sql-server-editions/sql-server-express.aspx)
	- Make sure that you add your account to the list of Administrators

#### Getting Schema Updates
1. In Visual Studio, open the Package Manager Console, and enter `update-database`.  This will run the migrations to bring your database up to the current version.


# Developer Guidelines
## Source Control Recommendations
### Update
It is strongly recommended that you get the latest changes from master before you start to work.  While working, frequently get the latest version merged into your branch.  This will minimize merge conflicts, which can be a pain to resolve.

You should also get in the habit of running `update-database` every time you get the latest version of the code.  You must think of the database schema as part of the commits - not as something separate.

### Commit
**DO NOT COMMIT BROKEN CODE.**


## Database Schema Changes
### On Update
As noted above, get in the habit of running `update-database` from the Package Manager Console every time you get the latest version of the code.  You must think of the database schema as part of the commits - not as something separate.

### On Commit
The database schema *is* part of the code - treat it like it.  Place changes to the schema *in the same commit* as the code changes that required that change in schema.  The entity framework code-first migrations make this task a little easier on you.

After you have gotten the latest version of the code, and all the tests pass, run `add-migration "reason for migration"` from the Package Manager Console.  Review the generated migration to ensure that it was done properly, and be sure to include the generated migration files with your code changes.

## MVC Practices
The Models in the MVC project are specific to the view.  They are NOT domain classes.  They may even be specific to the controller action.  See this [stackoverflow answer](http://stackoverflow.com/questions/7735635/real-example-of-tryupdatemodel-asp-net-mvc-3/7777059#7777059) for more information.