# DOMAIN
======================
The domain is about companies which have names and can have different base currencies. The definition of companies and changes to them are stored as events which must be processed. Various currencies will have different exchange rates and it must be possible to convert between amounts in different currencies using these exchange rates.

For companies some form of expenses (called "LineItems") are registered on different timestamps and using various currencies.

# PROJECTS
======================
The solution contains a project "TPBackend" with mostly interfaces and some data structures. The other project is "TPBackend.Tests" which contain XUnit tests that exercises the code in TPBackend.

# TASK
======================
The "TPBackend.Tests" project contains 16 XUnit tests which are currently all failing ("red"). The task is to make sure all (or as many as possible) of the 16 tests pass ("green") by implementing the interfaces in TPBackend. The exact sematics of the interfaces should be guessable by studying the unit tests.

Simply hardcoding answers to specific parameters in the interfaces to make the tests pass will of course not be acceptable. But any reasonable solution that might work in real life will be fine.

You may implement the interfaces in any order you like, but an order that starts with the most basic interfaces are probably preferable. For example:
1) IXmlLoader
2) IJsonLoader
3) IEcbXmlParser
4) IEventParser
5) ILineItemParser
6) IRepository
7) IRepositoryBuilder
8) IAccountHandler

When your solution is complete please send it (either zipping, dropbox url, by referencing a GitHub repository or equivalent) to: 
Ole Hyldahl Tolshave, CTO
oht@intercompany-software.com
